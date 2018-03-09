using System.Collections.Generic;
using System.Xml;
using CompLocator.DataAccessLayer;

namespace CompLocator.BusinessLogicLayer
{
	//This Class contains method for XMLParsing
	public class CompanyManager
	{
		#region Private Variables

		private const string _mCname = "name";
		private const string _mCaddress = "formatted_address";
		private string _nextPageToken = "next_page_token";
		private string _token = string.Empty;

		#endregion

		#region Private Methods
		/// <summary>
		/// Method for XMLParsing
		/// </summary>
		/// <param name="responseString"></param>
		private CompanyDetails XmlParse(string responseString)
		{
			XmlDocument responseDocument = new XmlDocument();

			responseDocument.LoadXml(responseString);

			XmlNodeList nameList = responseDocument.GetElementsByTagName(_mCname);
			XmlNodeList addressList = responseDocument.GetElementsByTagName(_mCaddress);
			XmlNodeList pageToken = responseDocument.GetElementsByTagName(_nextPageToken);
			if (pageToken.Count != 0)
			{
				_token = pageToken[pageToken.Count - 1].InnerText;
			}
			else
			{
				_token = null;
			}

			var companyDetails = new CompanyDetails
			{
				Names = new List<string>(),
				Addresses = new List<string>(),
			};

			foreach (XmlNode CompName in nameList)
			{
				companyDetails.Names.Add(CompName.InnerText);
			}

			foreach (XmlNode CompAddress in addressList)
			{
				companyDetails.Addresses.Add(CompAddress.InnerText);
			}
			return companyDetails;

		}
		#endregion

		#region Public Methods
		/// <summary>
		/// Method for validation and Calling GetResponseViaGoogleApi
		/// </summary>
		/// <param name="location"></param>
		/// <returns>response</returns>
		public CompanyDetails GetCompanies(string location)
		{
			var companies = new CompanyDetails
			{
				Names = new List<string>(),
				Addresses = new List<string>(),
			};
			if (location.Equals(null))
			{
				return null;
			}

			CallApiEntity callApiObject = new CallApiEntity();
			while (_token != null)
			{
				string response = callApiObject.GetResponseViaGoogleApi(location, _token);
				var resultCompanies = XmlParse(response);
				companies.Names.AddRange(resultCompanies.Names);
				companies.Addresses.AddRange(resultCompanies.Addresses);
			}
			return companies;
		}
		#endregion


	}
}
