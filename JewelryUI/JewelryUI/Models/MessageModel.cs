using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace JewelryUI.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        [Required (ErrorMessage ="Name  is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Cell no  is required")]
        public string Cellno { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Message is required")]
        public string Message { get; set; }
        
    }
}