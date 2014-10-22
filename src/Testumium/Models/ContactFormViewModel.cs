using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Testumium.Models
{
    public class ContactFormViewModel
    {
        [Required]
        public string ContactName { get; set; }
        [Required, Email]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        public string Company { get; set; }
        [Required]
        public string Comments { get; set; }
    }
}