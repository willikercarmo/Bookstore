﻿using BulkyBook.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BulkyBook.Web.Data
{
    public class ApplicationDbContext : DbContext // Installed EntityFrameworkCore package
    {
        // Constructor 
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // Set table names in the database
        public DbSet<Category> Categories { get; set; }
    }
}