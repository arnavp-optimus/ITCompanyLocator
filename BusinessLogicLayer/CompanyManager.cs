using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using CompLocator.DataAccessLayer;

namespace CompLocator.BusinessLogicLayer
{
	public class CompanyManager
	{
		const string name = "name";

		/// <summary>
		/// Method for XMLParsing
		/// </summary>
		/// <param name="responseString"></param>
		private CompanyDetails XmlParse(string responseString)
		{
			XmlDocument responseDocument = new XmlDocument();

			responseDocument.LoadXml(responseString);

			XmlNodeList nameList = responseDocument.GetElementsByTagName(name);
			XmlNodeList addressList = responseDocument.GetElementsByTagName("formatted_address");
			var companyDetails = new CompanyDetails
			{
				Names = new List<string>(),
				Addresses = new List<string>()
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

		/// <summary>
		/// 
		/// </summary>
		/// <param name="location"></param>
		/// <returns></returns>
		public CompanyDetails GetCompanies(string location)
		{
			//1. Location verification(empty,null, special character) result
			if (location.Equals(null))
			{
				return null;
			}

			//2. Data acess method call
			CallApiEntity callApiObject = new CallApiEntity();
			string response = callApiObject.GetResponseViaGoogleApi(location);
			return XmlParse(response);
		}


	}
}
