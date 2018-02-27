using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;

namespace Airplanes.Models
{
  public class City
  {
    private string _depName;
    private string _arrName;
    private int _id;

    public City(string depName, string arrName, int Id = 0)
    {
      _depName = depName;
      _arrName = arrName;
      _id = Id;
    }

    public int GetId()
    {
      return _id;
    }

    public string GetDepName()
    {
      return _depName;
    }
    public string GetArrName()
    {
      return _arrName;
    }

    public static List<City> GetAll()
    {
      List<City> allCities = new List<City> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      MySqlCommand cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM cities;";
      MySqlDataReader rdr = cmd.ExecuteReader() as MySqlDataReader;
      while (rdr.Read())
      {
        int cityId = rdr.GetInt32(0);
        string depName = rdr.GetString(1);
        string arrName = rdr.GetString(2);
        City newCity = new City(depName, arrName, cityId);
        allCities.Add(newCity);
      }

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
      return allCities;
    }

    public override bool Equals(System.Object otherCity)
    {
      if (!(otherCity is City))
      {
        return false;
      }
      else
      {
        City newCity = (City) otherCity;
        return this.GetId().Equals(newCity.GetId());
      }
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM cities;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `cities` (`depName`, `arrName`, `id`) VALUES (@DepName, @ArrName, @CityId);";

      MySqlParameter depName = new MySqlParameter();
      depName.ParameterName = "@DepName";
      depName.Value = this._depName;

      MySqlParameter arrName = new MySqlParameter();
      arrName.ParameterName = "@ArrName";
      arrName.Value = this._arrName;

      MySqlParameter id = new MySqlParameter();
      id.ParameterName = "@CityId";
      id.Value = this._id;

      cmd.Parameters.Add(depName);
      cmd.Parameters.Add(arrName);
      cmd.Parameters.Add(id);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public List<Flight> GetAllFlights()
    {
      List<Flight> allCityFlights = new List<Flight> {};
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `flights` WHERE `cities_id` = @cities_id;";

      MySqlParameter citiesId = new MySqlParameter();
      citiesId.ParameterName = "@cities_id";
      citiesId.Value = this._id;
      cmd.Parameters.Add(citiesId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      while(rdr.Read())
      {
        int id = rdr.GetInt32(0);
        int cityId = rdr.GetInt32(1);
        string rawDepDate = rdr.GetString(4);
        string rawArrDate = rdr.GetString(5);
        string rawDepTime = rdr.GetString(2);
        string rawArrTime = rdr.GetString(3);

        Flight newFlight = new Flight(rawDepTime, rawArrTime, rawDepDate, rawArrDate, id, cityId);
        newFlight.SetArrDate();
        newFlight.SetDepDate();
        allCityFlights.Add(newFlight);
      }
      return allCityFlights;
    }


    public static City Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * from `cities` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int cityId = 0;
      string depName = "";
      string arrName = "";

      while (rdr.Read())
      {
        cityId = rdr.GetInt32(0);
        depName = rdr.GetString(1);
        arrName = rdr.GetString(2);
      }

      City foundCity = new City(depName, arrName, cityId);

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundCity;
    }
  }
}
