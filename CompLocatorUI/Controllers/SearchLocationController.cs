using System.Collections.Generic;
using System.Web.Mvc;
using CompLocator.BusinessLogicLayer;
using CompLocatorUI.Models;

namespace CompLocatorUI.Controllers
{
	//Class to take user input and return the list of companies to View
	public class SearchLocationController : Controller
	{
		public ActionResult EnterLocation()
		{
			return View();
		}

		#region Public Methods
		/// <summary>
		/// Method that returns list of the Name and Adresses of ITCompanies of a particular location
		/// </summary>
		/// <param name="textbox"></param>
		/// <returns>View</returns>
		[HttpPost]
		public ActionResult EnterLocation(string textbox)
		{

			CompanyManager companymanager = new CompanyManager();
			var companyEntities = companymanager.GetCompanies(textbox);

			//convert to companyModel list
			var selectedCompanies = new List<CompanyModel>();
			for (int index = 0; index < companyEntities.Names.Count; index++)
			{
				var company = new CompanyModel
				{
					Name = companyEntities.Names[index],
					Address = companyEntities.Addresses[index]
				};
				selectedCompanies.Add(company);
			}
            return View("DisplayOutput",selectedCompanies);
		}
		#endregion

	}
}