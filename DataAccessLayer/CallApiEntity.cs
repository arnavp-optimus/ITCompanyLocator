using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;


namespace CompLocator.DataAccessLayer
{
	//
    public class CallApiEntity
    {
        string url = string.Empty;
	    HttpClient client = new HttpClient();
		string responseString = null;
		 
		/// <summary>
		/// GooglePlacesApi Calling
		/// </summary>
		/// <param name="location"></param>
		public string GetResponseViaGoogleApi(string location)
		{
			var myTask = Task.Run (async () =>
			{
				url = "https://maps.googleapis.com/maps/api/place/textsearch/xml?query=it+companies+in+"+location+"&key=AIzaSyC_SL4Eig9Gc49GlcLl7wqWMp0KYqA6-k0";
				HttpResponseMessage response = await client.GetAsync(url);
				responseString = await response.Content.ReadAsStringAsync();
			});
			myTask.Wait();
			return responseString;
		}
	}
}
