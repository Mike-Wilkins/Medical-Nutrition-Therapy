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

        [Fact]
        public async void SQLDietRepository_Should_Delete_DietType_By_ID()
        {
            IDietRepository sut = GetInMemoryDietRepository();
            List<DietType> dietType = DietTypeInMemoryDb();

            DietType savedDietType1 = await sut.Add(dietType[0]);
            DietType savedDietType2 = await sut.Add(dietType[1]);

            var deleteDietType = await sut.Delete(savedDietType1.Id);
            Assert.Single(await sut.GetAllDiets());
            var remainingDietType = await sut.GetDiet(savedDietType2.Id);
            Assert.Equal(2, remainingDietType.Id);
            Assert.Equal("Carb-counting Diet", remainingDietType.Name);
        }

        [Fact]
        public async void SQLDietRepository_Should_Update_DietType_By_Id()
        {
            IDietRepository sut = GetInMemoryDietRepository();
            List<DietType> dietType = DietTypeInMemoryDb();

            DietType savedDietType = await sut.Add(dietType[0]);

            Assert.Equal(1, savedDietType.Id);
            Assert.Equal("Bland Diet", savedDietType.Name);

            savedDietType.Name = "Bland Diet EDITED";

            var updateDietType = sut.Update(savedDietType);
            var getUpdateDietType = await sut.GetDiet(updateDietType.Id);

            Assert.Equal(1, getUpdateDietType.Id);
            Assert.Equal("Bland Diet EDITED", getUpdateDietType.Name);
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
