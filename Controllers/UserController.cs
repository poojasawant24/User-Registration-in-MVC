using MVC_Form.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVC_Form.Repository;

namespace MVC_Form.Controllers
{
    public class UserController : Controller
    {
        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Submit(UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();
                        using (SqlCommand cmd = new SqlCommand("SaveUserDetailsSP", connection))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@FirstName", userDetails.FirstName);
                            cmd.Parameters.AddWithValue("@LastName", userDetails.LastName);
                            cmd.Parameters.AddWithValue("@Gender", userDetails.Gender);
                            cmd.Parameters.AddWithValue("@DateOfBirth", userDetails.DateOfBirth);
                            cmd.Parameters.AddWithValue("@Country", userDetails.Country);
                            cmd.Parameters.AddWithValue("@Languages", string.Join(",", userDetails.Languages));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    return RedirectToAction("Success");
                }
                catch (Exception ex)
                {
                    ViewBag.ErrorMessage = "Error occurred while saving data: " + ex.Message;
                    return View("CreateUser", userDetails);
                }
            }
            return View("CreateUser", userDetails);
        }

        public ActionResult Clear()
        {
            return RedirectToAction("CreateUser");
        }

        public ActionResult Success()
        {
            return View();
        }

        public ActionResult EditUserDetails(int id)
        {
            UserRepository userRepository = new UserRepository();
            UserDetails user = userRepository.GetUserById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(UserDetails userDetails)
        {
            if (ModelState.IsValid)
            {
                UserRepository userRepository = new UserRepository();
                userRepository.UpdateUserDetails(userDetails);
                return RedirectToAction("GetAllUsers", "UserDasboard");
            }

            return View(userDetails);
        }

        public ActionResult Delete(int id)
        {
            UserRepository userRepository = new UserRepository();
            var user = userRepository.DeleteUserById(id);

            return RedirectToAction("GetAllUsers", "UserDasboard");
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            return RedirectToAction("GetAllUsers", "UserDashboard");
        }



    }
}