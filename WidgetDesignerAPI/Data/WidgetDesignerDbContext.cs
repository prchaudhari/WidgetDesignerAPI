
﻿using Microsoft.EntityFrameworkCore;
using WidgetDesignerAPI.Models;

namespace WidgetDesignerAPI.API.Data
{
    public class WidgetDesignerAPIDbContext : DbContext
    {
        public WidgetDesignerAPIDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Widgets> Widgets { get; set; }
    }
}