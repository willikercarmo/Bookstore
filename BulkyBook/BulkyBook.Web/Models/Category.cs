﻿using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Web.Models
{
    public class Category
    {
        // Data Annotations
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}