﻿using Microsoft.EntityFrameworkCore;
using MinimalApi2.Models;

namespace MinimalApi2.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("ConnectionString");
        }
        public DbSet<Book> Books => Set<Book>();
    }
}
