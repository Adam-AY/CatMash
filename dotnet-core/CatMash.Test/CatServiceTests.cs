using CatMash.Api;
using CatMash.Api.Models;

namespace CatMash.Test
{

    public class CatServiceTests
    {
        [Fact]
        public void GetWinners_Should_Return_Top3_When_All_Scores_Are_Unique()
        {
            // Arrange
            Cat cat1 = new Cat { Id = "A", Url = "cat1.png" };
            Cat cat2 = new Cat { Id = "B1", Url = "cat2.png", Score = 1230 };
            Cat cat3 = new Cat { Id = "B2", Url = "cat3.png", Score = 1450 };
            Cat cat4 = new Cat { Id = "D", Url = "cat4.png", Score = 2300 };

            var service = new CatService();

            service.Cats.Add(cat1);
            service.Cats.Add(cat2);
            service.Cats.Add(cat3);
            service.Cats.Add(cat4);

            // Act
            var result = service.GetWinners();

            // Assert
            Assert.Equal(3, result.Count);
            Assert.Equal(new[] { "D", "B2", "B1" }, result.Select(c => c.Id));
        }

        [Fact]
        public void GetWinners_Should_Ignore_Duplicated_Scores()
        {
            // Arrange
            Cat cat1 = new Cat { Id = "A", Url = "cat1.png", Score = 1300 };
            Cat cat2 = new Cat { Id = "B1", Url = "cat2.png", Score = 1300 };
            Cat cat3 = new Cat { Id = "B2", Url = "cat3.png", Score = 1450 };
            Cat cat4 = new Cat { Id = "D", Url = "cat4.png", Score = 1250 };

            var service = new CatService();

            service.Cats.Add(cat1);
            service.Cats.Add(cat2);
            service.Cats.Add(cat3);
            service.Cats.Add(cat4);

            // Act
            var result = service.GetWinners();

            // Assert
            Assert.DoesNotContain(result, c => c.Score == 1300);
            Assert.Equal(new[] { "B2", "D" }, result.Select(c => c.Id));
        }

        [Fact]
        public void GetWinners_Should_Return_Empty_When_All_Scores_Are_Duplicated()
        {
            // Arrange
            Cat cat1 = new Cat { Id = "A", Url = "cat1.png", Score = 1300 };
            Cat cat2 = new Cat { Id = "B1", Url = "cat2.png", Score = 1300 };
            Cat cat3 = new Cat { Id = "B2", Url = "cat3.png", Score = 1300 };
            Cat cat4 = new Cat { Id = "D", Url = "cat4.png", Score = 1300 };

            var service = new CatService();

            service.Cats.Add(cat1);
            service.Cats.Add(cat2);
            service.Cats.Add(cat3);
            service.Cats.Add(cat4);

            // Act
            var result = service.GetWinners();

            // Assert
            Assert.Empty(result);
        }

        [Fact]
        public void GetWinners_Should_Ignore_Default_Score_1200()
        {
            // Arrange
            Cat cat1 = new Cat { Id = "A", Url = "cat1.png", Score = 1400 };
            Cat cat2 = new Cat { Id = "B1", Url = "cat2.png", Score = 1200 };
            Cat cat3 = new Cat { Id = "B2", Url = "cat3.png", Score = 1100 };
            Cat cat4 = new Cat { Id = "D", Url = "cat4.png", Score = 1300 };

            var service = new CatService();

            service.Cats.Add(cat1);
            service.Cats.Add(cat2);
            service.Cats.Add(cat3);
            service.Cats.Add(cat4);

            // Act
            var result = service.GetWinners();

            // Assert
            Assert.DoesNotContain(result, c => c.Score == 1200);
            Assert.Equal(new[] { "A", "D" }, result.Select(c => c.Id));
        }
    }
}
