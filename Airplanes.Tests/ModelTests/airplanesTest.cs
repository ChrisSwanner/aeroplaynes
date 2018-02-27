using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using MySql.Data.MySqlClient;
using Airplanes.Models;

namespace Airplanes.Tests
{
  [TestClass]
  public class FlightTests : IDisposable
  {
    public void Dispose()
    {
      Flight.DeleteAll();
      City.DeleteAll();
    }

    public FlightTests()
    {
      DBConfiguration.ConnectionString = "server=localhost;user id=root;password=root;port=8889;database=airplanes_test;";
    }

    [TestMethod]
    public void GetFormattedDate_FetchDate_Date()
    {
      Flight testFlight = new Flight("13:04", "20:13", "2008-03-02", "2008-03-02");
      DateTime controlDep = new DateTime(2008, 03, 02, 13, 04, 00);
      DateTime controlArr = new DateTime(2008, 03, 02, 20, 13, 00);

      testFlight.SetDepDate();
      testFlight.SetArrDate();
      DateTime depResult = testFlight.GetDep();
      DateTime arrResult = testFlight.GetArr();

      Assert.AreEqual(depResult, controlDep);
      Assert.AreEqual(arrResult, controlArr);
    }
  }
}
