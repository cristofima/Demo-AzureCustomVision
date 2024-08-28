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

        /// <summary>
        /// Analyze multiple visual features in the given image
        /// </summary>
        /// <remarks>
        /// Available features to analyze: 
        /// - Tags
        /// - Caption
        /// - DenseCaptions
        /// - Objects
        /// - Read
        /// - SmartCrops
        /// - People
        /// </remarks>
        /// <param name="features">Visual features (separated by comma) to analize. Ex: Caption, Tags, Objects</param>
        [HttpPost]
        public async Task<IActionResult> Analyze([FromQuery] string features, [FromForm] FileRequest request)
        {
            try
            {
                var result = await this.azureVisionService.AnalyzeImage(request, features);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
