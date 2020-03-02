using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ADSM.Models
{
    public class User_Details
    {
        [Key]
        public int Reg_Id { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage =" First Name is required")]
        public string First_Name { get; set; }
        [Display(Name = "Last Name")]
        [Required(ErrorMessage = " Last Name is required")]
        public string Last_Name { get; set; }
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd-MMM-yy}")]
        [Required(ErrorMessage = " Date of Birth is required")]
        public DateTime DOB  { get; set; }
        [Required]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail is not valid")]
        public string Email  { get; set; }
        [Required(ErrorMessage = " Address is required")]
        public string Address { get; set; }
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        [RegularExpression(@"^(\d{10})$", ErrorMessage = "Please enter correct number")]
        public string Contact { get; set; }
        [Required(ErrorMessage = "Please select your gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = " Password is required")]
        public string Password { get; set; }
        [Display(Name = "Confirm Password")]
        [Compare("Password", ErrorMessage = "Password do not match")]
        public string Confirm_Password { get; set; }
        public string LastUpdatedBy { get; set; }
        public DateTime LastUpdatedOn { get; set; }

    }
}