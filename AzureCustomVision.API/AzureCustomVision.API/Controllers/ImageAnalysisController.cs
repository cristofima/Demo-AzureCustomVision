using AzureCustomVision.API.Interfaces;
using AzureCustomVision.API.Requests;
using Microsoft.AspNetCore.Mvc;

namespace AzureCustomVision.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageAnalysisController : ControllerBase
    {
        private readonly IAzureVisionService azureVisionService;

        public ImageAnalysisController(IAzureVisionService azureVisionService)
        {
            this.azureVisionService = azureVisionService;
        }

        [HttpPost]
        public async Task<IActionResult> Analize([FromForm] FileRequest request)
        {
            try
            {
                var result = await this.azureVisionService.AnalizeImage(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
