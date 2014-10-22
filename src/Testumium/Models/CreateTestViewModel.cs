using DataAnnotationsExtensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Testumium.Domain.Data;

namespace Testumium.Models
{
    public class CreateTestViewModel
    {
        public TestType TestType { get; set; }
        public string TestList { get; set; }
        public string Domain { get; set; }

        [Required]
        public string ContactName { get; set; }
        [Required, Email]
        public string Email { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required, DataAnnotationsExtensions.Url]
        public string WebsiteUrl { get; set; }
        public string Company { get; set; }
        public string Comments { get; set; }
        public int Minute { get; set; }
        public int Hour { get; set; }

        public IList<SelectListItem> Hours { get; set; }
        public IList<SelectListItem> Minutes { get; set; }

        public CreateTestViewModel()
        {
            this.Hours = new List<SelectListItem>();
            for (int i = 0; i < 24; i++)
            {
                this.Hours.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }
            this.Minutes = new List<SelectListItem>();
            for (int i = 0; i < 60; i++)
            {
                this.Minutes.Add(new SelectListItem() { Text = i.ToString(), Value = i.ToString() });
            }
        }
    }

    public class TestItemViewModel
    {
        public string Description { get; set; }
    }
}