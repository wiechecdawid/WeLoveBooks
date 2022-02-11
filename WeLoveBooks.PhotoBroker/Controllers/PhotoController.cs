using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WeLoveBooks.DataAccess.Models;
using WeLoveBooks.PhotoBroker.Dtos;
using WeLoveBooks.PhotoBroker.Interfaces;
using WeLoveBooks.PhotoBroker.Models;

namespace WeLoveBooks.PhotoBroker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoService _photoService;

        public PhotoController(IPhotoService photoService)
        {
            _photoService = photoService;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromQuery]int type,
            [FromQuery]string id,
            [FromForm]IFormFile file,
            CancellationToken token)
        {
            var addPhotoDto = new AddPhotoDto
            {
                PhotoType = (Dtos.PhotoType)type,
                ForeignKeyId = id,
                File = file
            };

            Photo photo;

            try
            {
                photo = await _photoService.AddPhoto(addPhotoDto, token);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

            return Ok(photo);
        }

        [HttpDelete]
        public async Task<ActionResult<OperationResult>> Delete(string id)
        {
            return await _photoService.DeletePhoto(id);
        }
    }
}
