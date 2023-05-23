using CarShop.Models.Models;
using CarShop.Logic.Services;
using CarShop.Models.Entities;
using CarShop.Repository.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarShop.Test
{
    [TestFixture]
    public class CarLogicTests
    {
        #region Constants

        const int Brand1Id = 1;
        const int Brand2Id = 2;

        const string Brand1Name = "brand1Test";
        const string Brand2Name = "brand2Test";

        #endregion

        [TestCaseSource(nameof(GetBrandAveragesData))]
        public void BrandAveragesTest(List<Brand> brands, List<Car> cars, List<AverageModel> expectedAverages)
        {
            // Arrange
            var carRepo = new Mock<ICarRepository>();
            var brandRepo = new Mock<IBrandRepository>();

            carRepo.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());
            brandRepo.Setup(x => x.ReadAll()).Returns(brands.AsQueryable());

            var logic = new CarLogic(carRepo.Object, brandRepo.Object);

            // Act
            var result = logic.GetBrandAverages();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.EquivalentTo(expectedAverages));
        }

        [TestCaseSource(nameof(GetCarsForBrandFilteringData))]
        public void BrandFilteringTest(List<Car> cars, int brandId, int expectedCount)
        {
            // Arrange
            var carRepo = new Mock<ICarRepository>();
            //var brandRepo = new Mock<IBrandRepository>();

            carRepo.Setup(x => x.ReadAll()).Returns(cars.AsQueryable());

            var logic = new CarLogic(carRepo.Object, null);

            // Act
            var result = logic.ReadAllByBrandId(brandId);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(expectedCount));
            Assert.That(result.Select(x => x.BrandId), Is.All.EqualTo(brandId));
        }

        #region Utils

        static List<TestCaseData> GetCarsForBrandFilteringData()
        {
            var result = new List<TestCaseData>();

            // Empty elements
            result.Add(new TestCaseData(
                new List<Car>(),
                1,
                0
            ));

            // MultipleElement
            var car1 = new Car() { Id = 1, BrandId = Brand1Id, Model = "Test1", Price = 2500 };
            var car2 = new Car() { Id = 2, BrandId = Brand1Id, Model = "Test2", Price = 1400 };
            var car3 = new Car() { Id = 3, BrandId = Brand2Id, Model = "Test3", Price = 1000 };
            var car4 = new Car() { Id = 4, BrandId = Brand2Id, Model = "Test4", Price = 2000 };
            var car5 = new Car() { Id = 5, BrandId = Brand2Id, Model = "Test5", Price = 3001 };

            result.Add(new TestCaseData(
                new List<Car>()
                {
                    car1, car2, car3, car4, car5
                },
                Brand2Id,
                3
            ));

            return result;
        }

        static List<TestCaseData> GetBrandAveragesData()
        {
            var result = new List<TestCaseData>();

            // Empty elements
            result.Add(new TestCaseData(
                new List<Brand>(),
                new List<Car>(),
                new List<AverageModel>()
            ));

            // One brand withot car
            result.Add(new TestCaseData(
                new List<Brand>()
                { 
                    new Brand() { Id = Brand1Id, Name = Brand1Name }
                },
                new List<Car>(),
                new List<AverageModel>()
                {
                    new AverageModel() { BrandName = Brand1Name, Average = 0 }
                }
            ));

            // One brand with one car
            result.Add(new TestCaseData(
                new List<Brand>()
                {
                    new Brand() { Id = Brand1Id, Name = Brand1Name }
                },
                new List<Car>()
                {
                    new Car() { Id = 1, BrandId = Brand1Id, Model = "test1", Price = 1200 }
                },
                new List<AverageModel>()
                {
                    new AverageModel() { BrandName = Brand1Name, Average = 1200 }
                }
            ));

            // One brand without car (bad id)
            result.Add(new TestCaseData(
                new List<Brand>()
                {
                    new Brand() { Id = Brand1Id, Name = Brand1Name }
                },
                new List<Car>()
                {
                    new Car() { Id = 1, BrandId = Brand2Id, Model = "test1", Price = 1200 }
                },
                new List<AverageModel>()
                {
                    new AverageModel() { BrandName = Brand1Name, Average = 0 }
                }
            ));

            // Multiple brand with multiple car
            var car1 = new Car() { Id = 1, BrandId = Brand1Id, Model = "Test1", Price = 2500 };
            var car2 = new Car() { Id = 2, BrandId = Brand1Id, Model = "Test2", Price = 1400 };
            var car3 = new Car() { Id = 3, BrandId = Brand2Id, Model = "Test3", Price = 1000 };
            var car4 = new Car() { Id = 4, BrandId = Brand2Id, Model = "Test4", Price = 2000 };
            var car5 = new Car() { Id = 5, BrandId = Brand2Id, Model = "Test5", Price = 3001 };

            result.Add(new TestCaseData(
                new List<Brand>()
                {
                    new Brand() { Id = Brand1Id, Name = Brand1Name },
                    new Brand() { Id = Brand2Id, Name = Brand2Name },
                },
                new List<Car>()
                {
                    car1, car2, car3, car4, car5
                },
                new List<AverageModel>()
                {
                    new AverageModel() { BrandName = Brand1Name, Average = (car1.Price + car2.Price) / 2 },
                    new AverageModel() { BrandName = Brand2Name, Average = (car3.Price + car4.Price + car5.Price) / 3.0 }
                }
            ));

            return result;
        }

        #endregion
    }
}
