﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace WebApplication1
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public List<String> GetAll()
        {
            string connString = "server=localhost;uid=admin;pwd=admin1234;database=StudentGradesDB";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = (connString);
            conn.Open();
            string query = "SELECT * FROM StudentGradesDB.Grades;";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            List<String> list = new List<String>();
            while (reader.Read())
            {
                list.Add("StudentId=" + reader.GetString(0) + "||" +
                    " CourseId=" + reader.GetString(1) + "||" +
                    " CourseName=" + reader.GetString(2) + "||" +
                    " Grade=" + reader.GetString(3));

            }
            return list;
        }

        [WebMethod]
        public DataTable GetDataTable()
        {
            DataTable dt = new DataTable("grades_tb ");
            dt.Columns.Add("StudentID", typeof(string));
            dt.Columns.Add("CourseID", typeof(string));
            dt.Columns.Add("CourseName", typeof(string));
            dt.Columns.Add("Grade", typeof(string));

            string connString = "server=localhost;uid=admin;pwd=admin1234;database=StudentGradesDB";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = (connString);
            conn.Open();
            string query = "SELECT * FROM StudentGradesDB.Grades;";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dt.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3));
            }

            return dt;
        }

        [WebMethod]
        public DataTable GetAverage()
        {
            DataTable dt = new DataTable("grades_tb ");
            dt.Columns.Add("StudentID", typeof(string));
            dt.Columns.Add("StudentName", typeof(string));
            dt.Columns.Add("AverageGrade", typeof(string));

            string connString = "server=localhost;uid=admin;pwd=admin1234;database=StudentGradesDB";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = (connString);
            conn.Open();
            string query = "SELECT s.Id, s.Name, AVG(g.Grade) as Average_Grade\r\nFROM Student s\r\nJOIN Grades g ON s.Id = g.Student_Id\r\nGROUP BY s.Id, s.Name;";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dt.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2));
            }

            return dt;
        }




        [WebMethod]
        public DataTable GetTop5()
        {
            DataTable dt = new DataTable("grades_tb ");
            dt.Columns.Add("StudentID", typeof(string));
            dt.Columns.Add("StudentName", typeof(string));
            dt.Columns.Add("AverageGrade", typeof(string));

            string connString = "server=localhost;uid=admin;pwd=admin1234;database=StudentGradesDB";
            MySqlConnection conn = new MySqlConnection();
            conn.ConnectionString = (connString);
            conn.Open();
            string query = "SELECT s.Id, s.Name, AVG(g.Grade) as Average_Grade\r\nFROM Student s\r\nJOIN Grades g ON s.Id = g.Student_Id\r\nGROUP BY s.Id, s.Name\r\nORDER BY Average_Grade DESC\r\nLIMIT 5;";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                dt.Rows.Add(reader.GetString(0), reader.GetString(1), reader.GetString(2));
            }

            return dt;
        }



    }
}

