﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;

namespace Capstone.DAO
{
    public class BrewerySQLDAO : IBreweryDAO
    {
        //FIELDS
        //create connectionString field for the DAO
        private readonly string connectionString;

        //CONSTRUCTOR
        //constructor pulls in a connection string and assigns it to field
        public BrewerySQLDAO( string dbConnectionstring)
        {
            connectionString = dbConnectionstring;
        }

        //METHODS
        //I want to get every Brewery in my database
        public List<Brewery> GetAllBreweries()
        {

            List<Brewery> allBreweries = new List<Brewery>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM brewery", conn);

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Brewery brewery = CreateBreweryFromReader(reader);
                    allBreweries.Add(brewery);
                }

            }
            return allBreweries;
        }

        //I want to get a brewery based on the brewery ID
        public Brewery GetBreweryByBreweryID(int breweryID)
        {
            Brewery brewery = new Brewery();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT * FROM brewery WHERE id = @BREWERYID", conn);
                cmd.Parameters.AddWithValue("@BREWERYID", breweryID);

                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    brewery = CreateBreweryFromReader(reader);
                }
            }
            return brewery;
        }

        //I need a method that creates a Brewery from the data returned by the SQL Data Reader
        private Brewery CreateBreweryFromReader(SqlDataReader reader)
        {
            Brewery brewery = new Brewery();
            brewery.BreweryId = Convert.ToInt32(reader["id"]);
            brewery.Name = Convert.ToString(reader["name"]);
            brewery.PhoneNumber = Convert.ToString(reader["phone"]);
            brewery.About = Convert.ToString(reader["about"]);
            brewery.WebSite = Convert.ToString(reader["website"]);
            brewery.Address = Convert.ToString(reader["address"]);
            brewery.Image = Convert.ToString(reader["image"]);
            brewery.Hours = Convert.ToString(reader["hours"]);
            brewery.City = Convert.ToString(reader["city"]);
            brewery.State = Convert.ToString(reader["state"]);
            brewery.ZipCode = Convert.ToString(reader["zip"]);

            return brewery;
        }
    }
}
