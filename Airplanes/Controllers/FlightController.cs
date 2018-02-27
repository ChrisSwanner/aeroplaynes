using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Airplanes.Models;

namespace Airplanes.Controllers
{
  public class FlightController : Controller
  {
    [HttpGet("/cities/{cityID}/flights/new")]
    public ActionResult CreateFlightForm(int cityId)
    {
      City foundCity = City.Find(cityId);
      return View(foundCity);
    }

    [HttpGet("/flights/{id}/update")]
    public ActionResult Detail(int id)
    {

      Flight flight = Flight.Find(id);
      return View(flight);
    }

    [HttpPost("/cities/{id}/update")]
    public ActionResult UpdateFlight(int id)
    {

      Flight thisFlight = Flight.Find(id);
      thisFlight.Edit(Request.Form["status-select"]);
      return View("CityDetail");
    }
  }
}
