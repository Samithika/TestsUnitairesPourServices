using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using TestsUnitairesPourServices.Data;
using TestsUnitairesPourServices.Models;
using TestsUnitairesPourServices.Services;

namespace TestProject1.Services
{
    [TestClass]
    public class CatsServiceTests
    {
        private ApplicationDBContext _db;
        private const int CHAT_AVEC_MAISON_ID = 1;
        private const int CHAT_SANS_MAISON_ID = 2;

        [TestInitialize]
        public void Init()
        {
            string dbName = "CatsService" + Guid.NewGuid().ToString();
            DbContextOptions<ApplicationDBContext> options = new DbContextOptionsBuilder<ApplicationDBContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .UseLazyLoadingProxies(true) // Active le lazy loading
            .Options;

            _db = new ApplicationDBContext(options);
            using ApplicationDBContext db = new ApplicationDBContext(options);

            // Ajoute des données au test
            // Deux maisons
            List<House> houses = new List<House>();
            House house1 = new House()
            {
                Address = "123 street",
                OwnerName = "bob",
                Cats = new List<Cat>()
            };

            House house2 = new House()
            {
                Address = "456 street",
                OwnerName = "alice",
                Cats = new List<Cat>()
            };

            houses.Add(house1);
            houses.Add(house2);

            // un chat avec maison
            Cat houseCat = new Cat()
            {
                Id = CHAT_AVEC_MAISON_ID,
                Name = "Mochi",
                Age = 3
            };

            // Un chat sans maison
            Cat wildCat = new Cat()
            {
                Id = CHAT_SANS_MAISON_ID,
                Name = "Salem",
                Age = 5
            };

            List<Cat> cats = new List<Cat>
            {
                houseCat,
                wildCat
            };

            _db.AddRange(houses);
            _db.AddRange(cats);
            _db.SaveChanges();
        }

        // Teste la méthode move avec succès
        [TestMethod]
        public void MoveTestSuccess()
        {
            CatsService catsService = new CatsService(_db);

        }

        // Teste la méthode move avec un chat dont l'ID est inconnu
        [TestMethod]
        public void MoveTestUnknownCat()
        {
            CatsService catsService = new CatsService(_db);

        }

        // Cleanup après chaque test
        [TestCleanup]
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
