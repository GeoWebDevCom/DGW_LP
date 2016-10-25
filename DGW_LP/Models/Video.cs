using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace DGW_LP.Models
{
    
    public class Video
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Src { get; set; }
        public string Description { get; set; }
        public string AuthorName { get; set; }
        public DateTime createdDate { get; set; }
    }

    public class Comments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime createdDate { get; set; }

        public ApplicationUser ApplicationUser { get; set; }
        public Video Video { get; set; }

    }


}