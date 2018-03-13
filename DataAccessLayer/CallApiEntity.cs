using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace CompLocator.DataAccessLayer
{
	//This Class contains the method to call Google Places API
	public class CallApiEntity
	{
		#region Private Variables

		string _mResponseString = null;
		const string _mPlacesApi = "https://maps.googleapis.com/maps/api/place/textsearch/xml?";
		const string _mSearchQuery = "query=it+companies+in+";
		const string _mKey = "&key=AIzaSyA-nPuYcF-IFNIXUcZFmFa_GBjJ--mhV70";
		const string _nextPageToken = "&hasNextPage=true & nextPage()=true&pagetoken=";

		#endregion

		#region Public Methods

		/// <summary>
		/// GooglePlacesApi Calling
		/// </summary>
		/// <param name="location"></param>
		public string GetResponseViaGoogleApi(string location, string nextPageToken)
		{
			string url = string.Empty;
			if (string.IsNullOrEmpty(nextPageToken))
			{
				url = _mPlacesApi + _mSearchQuery + location + _mKey;
			}
			else
			{
				url = _mPlacesApi + _mSearchQuery + location + _mKey + _nextPageToken + nextPageToken;
			}

			var hittingApi = Task.Run(async () =>
	   {

		   HttpClient client = new HttpClient();
		   HttpResponseMessage response = await client.GetAsync(url);
		   _mResponseString = await response.Content.ReadAsStringAsync();
	   });
			hittingApi.Wait();
			return _mResponseString;
		}
		#endregion
	}
}
