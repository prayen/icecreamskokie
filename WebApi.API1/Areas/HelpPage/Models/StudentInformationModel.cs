﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApi.API1.Areas.HelpPage.Models
{
    public class StudentInformationModel
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
    }
}