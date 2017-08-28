using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using week7.Repository.Entity;

namespace week7.Repository
{
    public class Employeecontext : DbContext
    {


        public Employeecontext() : base("name=MyConnectionString")
        {

        }
        public DbSet<Emp2> Employees { get; set; }
        public DbSet<State> StateList { get; set; }
        public DbSet<City> CityList { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public virtual void Commit()
        {
            base.SaveChanges();
        }
    }
}
