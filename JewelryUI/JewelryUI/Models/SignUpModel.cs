using JewelryDB.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace JewelryUI.Models
{
    public class SignUpModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="This field is required")]
        [StringLength(10)]
        public string LoginId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [StringLength(10)]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Cellno { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string StreetAdress { get; set; }
        [Required(ErrorMessage = "This field is required")]

        public string Email { get; set; }
        public string ImageURL { get; set; }
        public DateTime DOB { get; set; }
        public City City { set; get; }
        public RoleModel Role { set; get; }
        public string securityQuestion { set; get; }
        public string SecurityAnswer { set; get; }

    }
}