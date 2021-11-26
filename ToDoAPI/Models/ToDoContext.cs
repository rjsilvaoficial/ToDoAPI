using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoAPI.Models
{
    public class ToDoContext : DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base (options) { }

        public DbSet<ToDoItem> ToDoItens { get; set; } = null!;
    }
}
