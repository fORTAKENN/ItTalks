namespace ItTalks.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ItTalks.Data;
    using ItTalks.Data.Models;
    using ItTalks.Data.Models.Forum;
    using ItTalks.Data.Models.Gallery;
    using ItTalks.Web.ViewModels.Forum;
    using ItTalks.Web.ViewModels.Gallery;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Hosting.Internal;
    using Microsoft.EntityFrameworkCore;

    using Xunit;

    public class GalleryServiceTests
    {
        [Fact]
        public void AddPhotoShouldThrowArgumentNullExceptionIfNotGivenInput()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("Imagetestadd");
            var db = new ApplicationDbContext(options.Options);

            var hosting = new HostingEnvironment();
            var service = new GalleryService(db, hosting);

            Assert.Throws<ArgumentNullException>(() => service.AddImage(null, "userid"));
        }

        [Fact]
        public void GetAllShouldReturnCollectionOfImageViewModelOfAllPhotosInDatase()
        {
            var user = new ApplicationUser()
            {
                Email = "george.arbk@test.com",
            };

            var name = "Name";

            var image1 = new Image()
            {
                Name = name,
                Owner = user,
                OwnerId = user.Id,
            };

            var image2 = new Image()
            {
                Name = name,
                Owner = user,
                OwnerId = user.Id,
            };

            var photos = new List<Image>();
            photos.Add(image1);
            photos.Add(image2);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("IKmageGetAll");
            var db = new ApplicationDbContext(options.Options);

            db.Users.Add(user);
            db.Images.AddRange(photos);
            db.SaveChanges();

            int expectedCount = photos.Count();

            var hosting = new HostingEnvironment();
            var service = new GalleryService(db, hosting);

            var result = service.GetAll();

            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count());
            Assert.IsType<ImageViewModel>(result.First());
        }

        [Fact]
        public void GetPersonalPhotosShouldReturnCollectionOfAllPhotosMadeByTheUser()
        {
            var user = new ApplicationUser()
            {
                Email = "george.arbk@test.com",
            };
            var name = "Name";

            var image1 = new Image()
            {
                Name = name,
                Owner = user,
                OwnerId = user.Id,
            };

            var image2 = new Image()
            {
                Name = name,
                Owner = user,
                OwnerId = user.Id,
            };

            var image3 = new Image()
            {
                Name = name,
                OwnerId = "fakeuserid",
            };

            var photos = new List<Image>();
            photos.Add(image1);
            photos.Add(image2);
            photos.Add(image3);

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("TestGetPersonalPhotos");
            var db = new ApplicationDbContext(options.Options);

            db.Users.Add(user);
            db.Images.AddRange(photos);
            db.SaveChanges();

            int expectedCount = 2;

            var hosting = new HostingEnvironment();
            var service = new GalleryService(db, hosting);

            var result = service.GetPersonalPhotos(user.Id);

            Assert.NotNull(result);
            Assert.Equal(expectedCount, result.Count());
            Assert.IsType<ImageViewModel>(result.First());
        }

        [Fact]
        public void GetImageShouldReturnTheImageWithTheGivenId()
        {
            var user = new ApplicationUser()
            {
                Email = "george.arbk@test.com",
            };
            var name = "Name";

            var image1 = new Image()
            {
                Name = name,
                Owner = user,
                OwnerId = user.Id,
            };

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
    .UseInMemoryDatabase("TestGetPersonalPhotosOne");
            var db = new ApplicationDbContext(options.Options);

            db.Users.Add(user);
            db.Images.Add(image1);
            db.SaveChanges();

            var hosting = new HostingEnvironment();
            var service = new GalleryService(db, hosting);

            var result = service.GetImage(image1.ImageId);

            Assert.NotNull(result);
            Assert.IsType<ImageDetailsViewModel>(result);
            Assert.Equal(name, result.Name);
        }

        [Fact]
        public void GetPostShouldThrowArgumentNullExceptionIfIdIsInvalid()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("GetPHotoInvalid");
            var db = new ApplicationDbContext(options.Options);


            var hosting = new HostingEnvironment();
            var service = new GalleryService(db, hosting);


            Assert.Throws<ArgumentNullException>(() => service.GetImage("fake_image_id"));
        }

    }
}
