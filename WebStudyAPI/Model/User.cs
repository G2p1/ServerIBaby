using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace WebStudyAPI.Model
{
    public class User
    {


        public int ID { get; set; }
        public string Username { get; set; }
        public string Usr_password { get; set; }

        public string Usr_role { get; set; }
        public DateTime? Created_at { get; set; }
        public DateTime? Sub_start_math_month { get; set;}
        public DateTime? Sub_start_math_reading_month { get; set; }
        public DateTime? Sub_start_reading_month { get; set; }
        public DateTime? Sub_start_ready_wright_month { get; set; }
    }
}
