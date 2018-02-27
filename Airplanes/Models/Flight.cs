using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System;

namespace Airplanes.Models
{
  public class Flight
  {
    private int _id;
    private int _citiesId;
    private string _rawDepTime;
    private string _rawArrTime;
    private string _rawDepDate;
    private string _rawArrDate;
    private DateTime _formattedDep;
    private DateTime _formattedArr;
    private string _status;

    public Flight (string rawDepTime, string rawArrTime, string rawDepDate, string rawArrDate, int Id = 0, int citiesId = 0)
    {
      _status = "On Time";
      _rawDepTime = rawDepTime;
      _rawArrTime = rawArrTime;
      _rawDepDate = rawDepDate;
      _rawArrDate = rawArrDate;
      _id = Id;
      _citiesId = citiesId;
      _formattedDep = new DateTime();
      _formattedArr = new DateTime();
    }

    public int GetCitiesId()
    {
      return _citiesId;
    }
    public int GetId()
    {
      return _id;
    }

    public string GetStatus()
    {
      return _status;
    }
    public void SetStatus(string newStatus)
    {
      _status = newStatus;
    }
    public string GetRawDepTime()
    {
      return _rawDepTime;
    }
    public string GetRawArrTime()
    {
      return _rawArrTime;
    }
    public string GetRawDepDate()
    {
      return _rawDepDate;
    }
    public string GetRawArrDate()
    {
      return _rawArrDate;
    }

    public void SetCityId(int id)
    {
      _citiesId = id;
    }

    public DateTime GetArr()
    {
      return _formattedArr;
    }

    public DateTime GetDep()
    {
      return _formattedDep;
    }


    public void SetArrDate()
    {
      string[] dateArray = _rawArrDate.Split('-');
      List<int> intDateList = new List<int>{};
      foreach (string num in dateArray)
      {
        intDateList.Add(Int32.Parse(num));
      }

      string[] timeArray = _rawArrTime.Split(':');
      Console.WriteLine(timeArray);
      List<int> intTimeList = new List<int>{};
      foreach (string num in timeArray)
      {
        intTimeList.Add(Int32.Parse(num));
      }

      _formattedArr = new DateTime(intDateList[0], intDateList[1], intDateList[2], intTimeList[0], intTimeList[1], 00);
    }

    public void SetDepDate()
    {
      string[] dateArray = _rawDepDate.Split('-');
      List<int> intDateList = new List<int>{};
      foreach (string num in dateArray)
      {
        intDateList.Add(Int32.Parse(num));
      }

      string[] timeArray = _rawDepTime.Split(':');
      List<int> intTimeList = new List<int>{};
      foreach (string num in timeArray)
      {
        intTimeList.Add(Int32.Parse(num));
      }

      _formattedDep = new DateTime(intDateList[0], intDateList[1], intDateList[2], intTimeList[0], intTimeList[1], 00);
    }

    public static void DeleteAll()
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"DELETE FROM flights;";

      cmd.ExecuteNonQuery();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public void Save()
    {
      this.SetArrDate();
      this.SetDepDate();
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"INSERT INTO `flights` (`cities_id`, `rawDepTime`, `rawArrTime`, `rawDepDate`, `rawArrDate`, `formattedDep`, `formattedArr`, `status`) VALUES (@CitiesId, @RawDepTime, @RawArrTime, @RawDepDate, @RawArrDate, @FormattedDep, @FormattedArr, @Status);";

      MySqlParameter citiesId = new MySqlParameter();
      citiesId.ParameterName = "@CitiesId";
      citiesId.Value = this._citiesId;


      MySqlParameter rawDepTime = new MySqlParameter();
      rawDepTime.ParameterName = "@RawDepTime";
      rawDepTime.Value = this._rawDepTime;

      MySqlParameter rawArrTime = new MySqlParameter();
      rawArrTime.ParameterName = "@RawArrTime";
      rawArrTime.Value = this._rawArrTime;

      MySqlParameter rawDepDate = new MySqlParameter();
      rawDepDate.ParameterName = "@RawDepDate";
      rawDepDate.Value = this._rawDepDate;

      MySqlParameter rawArrDate = new MySqlParameter();
      rawArrDate.ParameterName = "@RawArrDate";
      rawArrDate.Value = this._rawArrDate;

      MySqlParameter formattedDep = new MySqlParameter();
      formattedDep.ParameterName = "@FormattedDep";
      formattedDep.Value = this._formattedDep;

      MySqlParameter formattedArr = new MySqlParameter();
      formattedArr.ParameterName = "@FormattedArr";
      formattedArr.Value = this._formattedArr;

      MySqlParameter status = new MySqlParameter();
      status.ParameterName = "@Status";
      status.Value = this._status;

      cmd.Parameters.Add(citiesId);
      cmd.Parameters.Add(rawDepTime);
      cmd.Parameters.Add(rawArrTime);
      cmd.Parameters.Add(rawDepDate);
      cmd.Parameters.Add(rawArrDate);
      cmd.Parameters.Add(formattedDep);
      cmd.Parameters.Add(formattedArr);
      cmd.Parameters.Add(status);

      cmd.ExecuteNonQuery();
      _id = (int) cmd.LastInsertedId;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }

    public static Flight Find(int id)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();

      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"SELECT * FROM `flights` WHERE id = @thisId;";

      MySqlParameter thisId = new MySqlParameter();
      thisId.ParameterName = "@thisId";
      thisId.Value = id;
      cmd.Parameters.Add(thisId);

      var rdr = cmd.ExecuteReader() as MySqlDataReader;
      int flightId = 0;
      int citiesId = 0;
      string rawDepDate = "";
      string rawArrDate = "";
      string rawDepTime = "";
      string rawArrTime = "";

      while (rdr.Read())
      {
        flightId = rdr.GetInt32(0);
        citiesId = rdr.GetInt32(1);
        rawDepDate = rdr.GetString(4);
        rawArrDate = rdr.GetString(5);
        rawDepTime = rdr.GetString(2);
        rawArrTime = rdr.GetString(3);
      }

      Flight foundFlight = new Flight(rawDepTime, rawArrTime, rawDepDate, rawArrDate, id, citiesId);
      foundFlight.SetDepDate();
      foundFlight.SetArrDate();

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }

      return foundFlight;

    }
    public void Edit(string newStatus)
    {
      MySqlConnection conn = DB.Connection();
      conn.Open();
      var cmd = conn.CreateCommand() as MySqlCommand;
      cmd.CommandText = @"UPDATE flights SET status = @newStatus WHERE id = @searchId;";

      MySqlParameter searchId = new MySqlParameter();
      searchId.ParameterName = "@searchId";
      searchId.Value = _id;
      cmd.Parameters.Add(searchId);

      MySqlParameter status = new MySqlParameter();
      status.ParameterName = "@newStatus";
      status.Value = newStatus;
      cmd.Parameters.Add(status);

      cmd.ExecuteNonQuery();
      _status = newStatus;

      conn.Close();
      if (conn != null)
      {
        conn.Dispose();
      }
    }
  }
}
