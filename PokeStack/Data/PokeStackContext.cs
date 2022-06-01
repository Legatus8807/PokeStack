#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PokeStack.Models;

namespace PokeStack.Data
{
    public class PokeStackContext : DbContext
    {
        public PokeStackContext (DbContextOptions<PokeStackContext> options)
            : base(options)
        {
        }

        public DbSet<PokeStack.Models.PokeModel> PokeModel { get; set; }
    }
}
