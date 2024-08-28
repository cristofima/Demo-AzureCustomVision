using AzureCustomVision.API.Interfaces;
using AzureCustomVision.API.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AzureCustomVision.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ObjectDetectionController : ControllerBase
    {
        private readonly IAzureCustomVisionService azureCustomVisionService;

        public ObjectDetectionController(IAzureCustomVisionService azureCustomVisionService)
        {
            this.azureCustomVisionService = azureCustomVisionService;
        }

        [HttpPost]
        [Route("CarPlate")]
        public async Task<IActionResult> CarPlate([FromForm] FileRequest request)
        {
            try
            {
                var result = await this.azureCustomVisionService.DetectObjects(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
