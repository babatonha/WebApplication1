﻿using System;  
using System.Collections.Generic;  
using System.Data;  
using System.Data.SqlClient;  
using System.Linq;  
using System.Threading.Tasks;  

namespace WebApplication1.Models
{
    public class EmployeeDataAccessLayer
    {
        //I think there is a way to hide the password in the string #still to find out
        string connectionString = "Server=ADMINRG-1VV6VFI\\SQLEXPRESS; database=StaffDirectory; User=sa; Password=123456";
        //To View all employees details    
        public IEnumerable<Employee> GetAllEmployees() // this is Procedure created in the database
        {
            List<Employee> lstemployee = new List<Employee>();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spGetAllEmployees", con);
                cmd.CommandType = CommandType.StoredProcedure;
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    Employee employee = new Employee();
                    employee.ID = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.City = rdr["City"].ToString();
                    employee.Contact = rdr["Contact"].ToString();
                    employee.Email = rdr["Email"].ToString();
                    lstemployee.Add(employee);
                }
                con.Close();
            }
            return lstemployee;
        }
        //To Add new employee record    
        public void AddEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spAddEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@Contact", employee.Contact);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        //To Update the records of a particluar employee  
        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spUpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", employee.ID);
                cmd.Parameters.AddWithValue("@Name", employee.Name);
                cmd.Parameters.AddWithValue("@Gender", employee.Gender);
                cmd.Parameters.AddWithValue("@Department", employee.Department);
                cmd.Parameters.AddWithValue("@City", employee.City);
                cmd.Parameters.AddWithValue("@Contact", employee.Contact);
                cmd.Parameters.AddWithValue("@Email", employee.Email);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
        //Get the details of a particular employee  
        public Employee GetEmployeeData(int? id)
        {
            Employee employee = new Employee();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                string sqlQuery = "SELECT * FROM tblEmployee WHERE EmployeeID= " + id;
                SqlCommand cmd = new SqlCommand(sqlQuery, con);
                con.Open();
                SqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    employee.ID = Convert.ToInt32(rdr["EmployeeID"]);
                    employee.Name = rdr["Name"].ToString();
                    employee.Gender = rdr["Gender"].ToString();
                    employee.Department = rdr["Department"].ToString();
                    employee.City = rdr["City"].ToString();
                    employee.Contact = rdr["Contact"].ToString();
                    employee.Email = rdr["Email"].ToString();
                }
            }
            return employee;
        }
        //To Delete the record on a particular employee  
        public void DeleteEmployee(int? id)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("spDeleteEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@EmpId", id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}