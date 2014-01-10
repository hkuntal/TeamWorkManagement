﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcCheckBoxListSampleApp.Model;
using MvcCheckBoxListSampleApp.ViewModels;

namespace MvcCheckBoxListSampleApp.Controllers {
	public class HomeController : Controller {
	  public HomeController() {
	    ViewBag.AuthorUrl = "http://www.codeproject.com/Members/Mikhail-T";
	    ViewBag.CodePrejectUrl = "http://www.codeproject.com/Articles/292050/MVC3-Html-CheckBoxList-custom-extension";
	    ViewBag.GitHubZipSourceUrl = "https://github.com/mikhail-tsennykh/MVC3-Html.CheckBoxList-custom-extension/zipball/master";
	    ViewBag.GitHubUrl = "https://github.com/mikhail-tsennykh/MVC3-Html.CheckBoxList-custom-extension";
	    ViewBag.LicenseUrl = "http://www.codeproject.com/info/cpol10.aspx";
	  }

    public ActionResult Index(string[] cities, PostedCities postedCities) {
      ViewBag.HideMenu = true;
      return View(GetCitiesModel(cities, postedCities));
    }

		public ActionResult Examples(string[] cities, PostedCities postedCities) {
		  return View(GetCitiesModel(cities, postedCities));
		}

    public ActionResult Docs() {
      return View();
    }
    
    private CitiesViewModel GetCitiesModel(string[] cities, PostedCities postedCities) {
			// setup properties
			var model = new CitiesViewModel();
			var selectedCities = new List<City>();
			var postedCityIDs = new string[0];
			if (postedCities == null) postedCities = new PostedCities();

			// if an array of posted city ids exists and is not empty,
			// save selected ids
			if (cities != null && cities.Any()) {
				postedCityIDs = cities;
				postedCities.CityIDs = cities;
			}
			// if a view model array of posted city ids exists and is not empty,
			// save selected ids
			if (postedCities.CityIDs != null && postedCities.CityIDs.Any()) {
				postedCityIDs = postedCities.CityIDs;
				model.WasPosted = true;
			}
			// if there are any selected ids saved, create a list of cities
			if (postedCityIDs.Any())
				selectedCities = CityRepository.GetAll()
					.Where(x => postedCityIDs.Any(s => x.Id.ToString().Equals(s))).ToList();

			// setup a view model
			model.AvailableCities = CityRepository.GetAll();
			model.SelectedCities = selectedCities;
			model.PostedCities = postedCities;

      return model;
    }

	}
}