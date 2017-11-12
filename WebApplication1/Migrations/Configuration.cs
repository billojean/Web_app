namespace WebApplication1.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using WebApplication1.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApplication1.DBA.DataContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApplication1.DBA.DataContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            var deparments = new List<Departments>
            {
               new Departments { DId = 1, Department = "A"},
               new Departments { DId = 2, Department = "B"},
               new Departments { DId = 3, Department = "C"}
            };
            deparments.ForEach(s => context.Departments.AddOrUpdate(d => d.Department, s));
            context.SaveChanges();

            var vehicles = new List<Vehicles>
            {
               new Vehicles { Id = "C-001", Kind = "Car"},
               new Vehicles { Id = "C-002", Kind = "Car"},
               new Vehicles { Id = "C-003", Kind = "Car"},
               new Vehicles { Id = "C-004", Kind = "Car"},
               new Vehicles { Id = "C-005", Kind = "Car"},
               new Vehicles { Id = "C-006", Kind = "Car"},
               new Vehicles { Id = "C-007", Kind = "Car"},
               new Vehicles { Id = "C-008", Kind = "Car"},
               new Vehicles { Id = "C-009", Kind = "Car"},
               new Vehicles { Id = "C-010", Kind = "Car"},
               new Vehicles { Id = "C-011", Kind = "Car"},
               new Vehicles { Id = "C-012", Kind = "Car"},
               new Vehicles { Id = "C-013", Kind = "Car"},
               new Vehicles { Id = "C-014", Kind = "Car"},
               new Vehicles { Id = "C-015", Kind = "Car"},
               new Vehicles { Id = "C-016", Kind = "Car"},
               new Vehicles { Id = "C-017", Kind = "Car"},
               new Vehicles { Id = "T-001", Kind = "Truck"},
               new Vehicles { Id = "T-002", Kind = "Truck"},
               new Vehicles { Id = "T-003", Kind = "Truck"},
               new Vehicles { Id = "T-004", Kind = "Truck"},
               new Vehicles { Id = "T-005", Kind = "Truck"},
               new Vehicles { Id = "T-006", Kind = "Truck"},
               new Vehicles { Id = "T-007", Kind = "Truck"},
               new Vehicles { Id = "T-008", Kind = "Truck"},
               new Vehicles { Id = "T-009", Kind = "Truck"},
               new Vehicles { Id = "T-010", Kind = "Truck"},
               new Vehicles { Id = "T-011", Kind = "Truck"},
               new Vehicles { Id = "T-012", Kind = "Truck"},
               new Vehicles { Id = "T-013", Kind = "Truck"},
               new Vehicles { Id = "T-014", Kind = "Truck"},
               new Vehicles { Id = "T-015", Kind = "Truck"},
               new Vehicles { Id = "T-016", Kind = "Truck"},
               new Vehicles { Id = "T-017", Kind = "Truck"}
            };
            vehicles.ForEach(s => context.Vehicles.AddOrUpdate(v=>v.Id,s));
            context.Vehicles.OrderBy(x => x.Id);
            context.SaveChanges();

            var laptops = new List<Laptops>
            {
                new Laptops {Id = "L-001-2014"},
                new Laptops {Id = "L-002-2014"},
                new Laptops {Id = "L-003-2014"},
                new Laptops {Id = "L-004-2014"},
                new Laptops {Id = "L-005-2014"},
                new Laptops {Id = "L-006-2014"},
                new Laptops {Id = "L-007-2014"},
                new Laptops {Id = "L-008-2014"},
                new Laptops {Id = "L-009-2014"},
                new Laptops {Id = "L-010-2014"},
                new Laptops {Id = "L-001-2015"},
                new Laptops {Id = "L-002-2015"},
                new Laptops {Id = "L-003-2015"},
                new Laptops {Id = "L-004-2015"},
                new Laptops {Id = "L-005-2015"},
                new Laptops {Id = "L-006-2015"},
                new Laptops {Id = "L-007-2015"},
                new Laptops {Id = "L-008-2015"},
                new Laptops {Id = "L-009-2015"},
                new Laptops {Id = "L-010-2015"},
                new Laptops {Id = "L-001-2016"},
                new Laptops {Id = "L-002-2016"},
                new Laptops {Id = "L-003-2016"},
                new Laptops {Id = "L-004-2016"},
                new Laptops {Id = "L-005-2016"},
                new Laptops {Id = "L-006-2016"},
                new Laptops {Id = "L-007-2016"},
                new Laptops {Id = "L-008-2016"},
                new Laptops {Id = "L-009-2016"},
                new Laptops {Id = "L-010-2016"}
            };
            laptops.ForEach(s => context.Laptops.AddOrUpdate(l=>l.Id,s));
            context.Spareparts.OrderBy(x => x.Id);
            context.SaveChanges();
            var spareparts = new List<SpareParts>
            {
                new SpareParts {Id = "S-001"},
                new SpareParts {Id = "S-002"},
                new SpareParts {Id = "S-003"},
                new SpareParts {Id = "S-004"},
                new SpareParts {Id = "S-005"},
                new SpareParts {Id = "S-006"},
                new SpareParts {Id = "S-007"},
                new SpareParts {Id = "S-008"},
                new SpareParts {Id = "S-009"},
                new SpareParts {Id = "S-010"},
                new SpareParts {Id = "S-011"},
                new SpareParts {Id = "S-012"},
                new SpareParts {Id = "S-013"},
                new SpareParts {Id = "S-014"},
                new SpareParts {Id = "S-015"},
                new SpareParts {Id = "S-016"},
                new SpareParts {Id = "S-017"},
                new SpareParts {Id = "S-018"},
                new SpareParts {Id = "S-019"},
                new SpareParts {Id = "S-020"},
                new SpareParts {Id = "S-021"},
                new SpareParts {Id = "S-022"},
                new SpareParts {Id = "S-023"},
                new SpareParts {Id = "S-024"},
                new SpareParts {Id = "S-025"}
            };
            spareparts.ForEach(s => context.Spareparts.AddOrUpdate(sp=>sp.Id,s));
            context.Spareparts.OrderBy(x => x.Id);
            context.SaveChanges();
        }
    }
}
