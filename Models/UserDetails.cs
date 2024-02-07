using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_Form.Models
{
    public class UserDetails
    {
        public UserDetails()
        {
            Languages = new List<string>();
        }
        public Int64 UserId { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Description = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; }

        [Display(Description = "First Name")]
        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Date of Birth is required")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        [Required(ErrorMessage = "At least one language is required")]
        public List<string> Languages { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        public enum CountryEnum
        {
            India,
            USA,
            UK
        }
    }
}