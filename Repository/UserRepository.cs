using MVC_Form.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Net.Http.Headers;

namespace MVC_Form.Repository
{
    public class UserRepository
    {
        private readonly string connectionString;

        public UserRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString; ;
        }
        public IEnumerable<UserDetails> GetAllUsers()
        {
            List<UserDetails> users = new List<UserDetails>();

            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetAllUsersSP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                UserDetails user = new UserDetails
                                {
                                    UserId = Convert.ToInt64(reader["UserId"]),
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]),
                                    Languages = reader["Languages"].ToString().Split(',').ToList(),
                                    Country = reader["Country"].ToString()
                                };

                                users.Add(user);
                            }
                        }
                    }
                }

            }
            catch (Exception)
            {
                throw;
            }
            return users;
        }

        public UserDetails GetUserById(int userId)
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("GetUserById_SP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", userId);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                UserDetails user = new UserDetails
                                {
                                    UserId = (Int32)reader["UserId"],
                                    FirstName = reader["FirstName"].ToString(),
                                    LastName = reader["LastName"].ToString(),
                                    Gender = reader["Gender"].ToString(),
                                    DateOfBirth = (DateTime)reader["DateOfBirth"],
                                    Languages = ((string)reader["Languages"]).Split(',').ToList(),
                                    Country = reader["Country"].ToString()
                                };

                                return user;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }

            return null;
        }

        public void UpdateUserDetails(UserDetails userDetails)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand cmd = new SqlCommand("UpdateUserDetailsSP", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@UserId", userDetails.UserId);
                        cmd.Parameters.AddWithValue("@FirstName", userDetails.FirstName);
                        cmd.Parameters.AddWithValue("@LastName", userDetails.LastName);
                        cmd.Parameters.AddWithValue("@Gender", userDetails.Gender);
                        cmd.Parameters.AddWithValue("@DateOfBirth", userDetails.DateOfBirth);
                        cmd.Parameters.AddWithValue("@Country", userDetails.Country);
                        cmd.Parameters.AddWithValue("@Languages", string.Join(",", userDetails.Languages));
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public int DeleteUserById(int id)
        {
            try
            {
                int RowsAffected;
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand("DeleteUserSP", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@UserId", id);
                        RowsAffected = command.ExecuteNonQuery();
                        return RowsAffected;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}