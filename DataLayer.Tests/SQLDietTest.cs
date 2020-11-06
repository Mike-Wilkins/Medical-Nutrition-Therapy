using DataLayer.Models;
using DataLayer.Repositories;
using DataLayer.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DataLayer.Tests
{
    public class SQLDietTest
    {
        List<DietType> DietTypeInMemoryDb()
        {
            List<DietType> dietTypeList = new List<DietType>()
                {
                    new DietType
                    {
                        Id = 1,
                        Name = "Bland Diet"
                    },
                    new DietType
                    {
                        Id = 2,
                        Name = "Carb-counting Diet"
                    }
                };

            return dietTypeList;
        }

        [Fact]
        public async void SQLDietRepository_Should_Add_DietType()
        {
            IDietRepository sut = GetInMemoryDietRepository();
            List<DietType> dietType = DietTypeInMemoryDb();

            DietType savedDietType = await sut.Add(dietType[0]);
            DietType savedDietType2 = await sut.Add(dietType[1]);

            var test = await sut.GetAllDiets();

            Assert.Equal(2, test.Count());
            Assert.Equal(1, savedDietType.Id);
            Assert.Equal("Bland Diet", savedDietType.Name);
            Assert.Equal(2, savedDietType2.Id);
            Assert.Equal("Carb-counting Diet", savedDietType2.Name);

        }

        [Fact]
        public async void SQLDietRepository_Should_Get_DietType_By_Id()
        {
            IDietRepository sut = GetInMemoryDietRepository();
            List<DietType> dietType = DietTypeInMemoryDb();

            DietType savedDietType = await sut.Add(dietType[0]);

            var test = await sut.GetDiet(savedDietType.Id);
            Assert.Single(await sut.GetAllDiets());
            Assert.Equal(1, test.Id);
            Assert.Equal("Bland Diet", test.Name);
        }




        private IDietRepository GetInMemoryDietRepository()
        {
            DbContextOptions<ApplicationDbContext> options;
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseInMemoryDatabase(databaseName: "DietTypeDatabase");


            options = builder.Options;
            ApplicationDbContext dietTypeDataContext = new ApplicationDbContext(options);
            dietTypeDataContext.Database.EnsureDeleted();
            dietTypeDataContext.Database.EnsureCreated();

            return new SQLDietRepository(dietTypeDataContext);
        }
    }

}
