using ItTalks.Data;
using ItTalks.Data.Models.Gallery;
using ItTalks.Web.ViewModels.Gallery;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ItTalks.Services.Data
{
    public class GalleryService : IGalleryService
    {
        private readonly ApplicationDbContext db;
        private readonly IHostingEnvironment hostingEnvironment;

        public GalleryService(ApplicationDbContext db, IHostingEnvironment hostingEnvironment)
        {
            this.db = db;
            this.hostingEnvironment = hostingEnvironment;
        }



        public void AddImage(PhotoInputModel image, string UserId)
        {
            string fileName = this.UploadFile(image);
            var user = this.db.Users.FirstOrDefault(u => u.Id == UserId);
            var photo = new Image()
            {
                Name = image.Name,
                ImageUrl = fileName,
                Owner = user,
                OwnerId = user.Id,

            };

            user.Images.Add(photo);
            this.db.Images.Add(photo);
            this.db.SaveChanges();
        }

        public ICollection<ImageViewModel> GetAll()
        {
            return this.db.Images.Select(i => new ImageViewModel() { 
            ImageId = i.ImageId,
            ImageUrl = i.ImageUrl,
            Name = i.Name

            }).ToList();
        }

        public ImageDetailsViewModel GetImage(string id)
        {
            var image = this.db.Images.FirstOrDefault(i => i.ImageId == id);
            var model = new ImageDetailsViewModel()
            {
                ImageId = image.ImageId,
                Name = image.Name,
                ImageUrl = image.ImageUrl

            };

            return model;
        }


        private string UploadFile(PhotoInputModel input)
        {
            string fileName = null;
            if (input.Photo != null)
            {
                string uploadDir = Path.Combine(this.hostingEnvironment.WebRootPath, "GalleryImages");
                fileName = Guid.NewGuid().ToString() + "-" + input.Photo.FileName;
                string filePath = Path.Combine(uploadDir, fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    input.Photo.CopyTo(fileStream);
                }
            }

            return fileName;
        }


    }
}
