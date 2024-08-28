using AzureCustomVision.API.Interfaces;
using AzureCustomVision.API.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AzureCustomVision.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClassificationController : ControllerBase
    {
        private readonly IAzureCustomVisionService azureCustomVisionService;

        public ClassificationController(IAzureCustomVisionService azureCustomVisionService)
        {
            this.azureCustomVisionService = azureCustomVisionService;
        }

        [HttpPost]
        [Route("PlantDiseases")]
        public async Task<IActionResult> PlantDiseases([FromForm] FileRequest request)
        {
            try
            {
                var result = await this.azureCustomVisionService.ClassifyImage(request);
                return Ok(result);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
