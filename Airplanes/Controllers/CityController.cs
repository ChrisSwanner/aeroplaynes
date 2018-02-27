
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Airplanes.Models;

namespace Airplanes.Controllers
{
  public class CityController : Controller
  {
    [Route("/")]
    public ActionResult Index()
    {
      List<City> allCities = City.GetAll();
      return View("Index", allCities);
    }

    [HttpGet("/cities/new")]
    public ActionResult CreateCityForm()
    {
      return View();
    }

    [HttpPost("/cities")]
    public ActionResult CreateCity()
    {
      City newCity = new City(Request.Form["city-name1"], Request.Form["city-name2"]);
      newCity.Save();
      return RedirectToAction("Index");
    }

    [HttpGet("/cities/{id}")]
    public ActionResult CityDetail(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      City selectedCity = City.Find(id);
      List<Flight> allFlights = selectedCity.GetAllFlights();
      model.Add("cities", selectedCity);
      model.Add("flight", allFlights);
      return View("Detail", model);
    }

    [HttpPost("/cities/{id}/flights")]
    public ActionResult CreateFlight(int id)
    {
      Dictionary<string, object> model = new Dictionary<string, object>();
      City foundCity = City.Find(id);
      List<Flight> cityFlights = foundCity.GetAllFlights();
      Flight newFlight = new Flight(Request.Form["dep-time"], Request.Form["arr-time"], Request.Form["dep-date"], Request.Form["arr-date"]);
      newFlight.SetCityId(foundCity.GetId());
      cityFlights.Add(newFlight);
      newFlight.Save();
      model.Add("flight", cityFlights);
      model.Add("cities", foundCity);
      return View("Detail", model);

    }

    [HttpGet("/cities/{id}/update")]
    public ActionResult Detail()
    {
      return View();
    }
  }
}
