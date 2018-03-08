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
		// GET: SearchLocation
		public ActionResult EnterLocation()
		{
			return View();
		}

		[HttpGet]
		public ActionResult GetCompanies(String location)
		{

			CompanyManager companymanager = new CompanyManager(); // creating object of CompanyManager Class which is in BusinessLogicLayer
			var companyEntities = companymanager.GetCompanies(location);

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

			return View(selectedCompanies);
		}
	}
}