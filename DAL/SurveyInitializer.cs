using dashboard.Models;
using System;
using System.Collections.Generic;

namespace dashboard.DAL
{
    public class SurveyInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<SurveyContext>
    {
        protected override void Seed(SurveyContext context)
        {
            var owners = new List<Owner>
            {
                new Owner{ID=123999,Name="Kim"},
                new Owner{ID=312314,Name="Idris"},
                new Owner{ID=809712,Name="Bas"},
            };

            owners.ForEach(s => context.Owners.Add(s));
            context.SaveChanges();

            var surveys = new List<Survey>
            {
                new Survey{ID=123456, Name="Cijferlijsten", Status=Status.Afgerond, StartDate=DateTime.Parse("2021-09-01 23:12"), EndDate=DateTime.Parse("2021-12-01 23:10"), OwnerID=123999},
                new Survey{ID=901230, Name="Niveau docenten", Status=Status.Gepland, StartDate=DateTime.Parse("2021-12-10 10:10"), EndDate=DateTime.Parse("2040-12-11 10:10"), OwnerID=312314},
                new Survey{ID=973242, Name="Openingstijden", Status=Status.Lopend, StartDate=DateTime.Parse("2021-05-23 12:10"), EndDate=DateTime.Parse("2021-12-30 09:10"), OwnerID=809712}
            };
            surveys.ForEach(s => context.Surveys.Add(s));
            context.SaveChanges();
        }
    }
}