using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompLocator.BusinessLogicLayer;
using CompLocatorUI.Models;

namespace CompLocatorUI.Controllers
{
	public class SearchLocationController : Controller
	{
		
		public ActionResult EnterLocation()
		{
			return View();
		}

		[HttpPost]
		public ActionResult EnterLocation(string textbox)
		{

			CompanyManager companymanager = new CompanyManager(); // creating object of CompanyManager Class which is in BusinessLogicLayer
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

	}
}