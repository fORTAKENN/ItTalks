using ItTalks.Web.ViewModels.Gallery;
using System;
using System.Collections.Generic;
using System.Text;

namespace ItTalks.Services.Data
{
  public interface IGalleryService
    {
        public ICollection<ImageViewModel> GetAll();

        public ImageDetailsViewModel GetImage(string id);

        public void AddImage(PhotoInputModel image, string UserId);


    }
}
