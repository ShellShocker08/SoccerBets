﻿using Microsoft.EntityFrameworkCore;
using Soccer.Web.Data.Entities;

namespace Soccer.Web.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        // Objetos que se crean en la BD
        public DbSet<TeamEntity> Teams { get; set; }
    }
}