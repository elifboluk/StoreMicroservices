using FreeProduct.Services.PhotoStock.DTOs;
using FreeProduct.Shared.ControllerBases;
using FreeProduct.Shared.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeProduct.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            // CancellationToken alma amacımız fotoğraf süresinin kısa olması durumunda(20 sn) eğer endpointi çağıran yer işlemi sonlandırırsa burada da sonlanması için.
            // Kullanıcı fotoğrafı seçti ve fotoğraf kaydoluyor diyelim fakat bu sürede tarayıcıyı kapatırsa CancellationToken otomatik olarak tetiklenecek ve fotoğraf eklemeyi de iptal edecek.
            // async başlayan bir işlemi sadece hata fırlatarak sonlandırabiliriz. CancellationToken da hata fırlatarak sonlandırıyor.

            if (photo != null && photo.Length > 0)
            {
                // Get path.
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName); // Add photo.

                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken);

                // example: http://www.photostock.api.com/photos/asd.jpg
                var returnPath = "photos/" + photo.FileName;

                PhotoDto photoDto = new() { Url = returnPath }; // Get type.

                return CreateActionResultInstance(Response<PhotoDto>.Success(photoDto, 200));
            }
            return CreateActionResultInstance(Response<PhotoDto>.Fail("Fotoğraf bulunamadı.", 400));
        }
                

        [HttpDelete]
        public IActionResult PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if (!System.IO.File.Exists(path)) // Path var mı yok mu?
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("Fotoğraf bulunamadı.", 404));
            }

            System.IO.File.Delete(path);

            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
    }
}
