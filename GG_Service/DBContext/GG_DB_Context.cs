using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GG_Service.Models;

namespace GG_Service.DBContext
{
    public class GG_DB_Context : DbContext
    {
        public DbSet<GG_Orders> GGOrders { get; set; }

       protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           _ = optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GGMarketPlace;Integrated Security=True");
       }
    }
}
