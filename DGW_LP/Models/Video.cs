using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DGW_LP.Models
{
    
    public class Video
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Src { get; set; }
    }
}