using Microsoft.EntityFrameworkCore;
using MuranoTest.Data.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace MuranoTest.Data.Db
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<SearchResult> SearchResults { get; set; }
    }
}
