using Azure.AI.Vision.ImageAnalysis;
using Azure;
using AzureCustomVision.API.Interfaces;
using AzureCustomVision.API.Options;
using Microsoft.Extensions.Options;
using AzureCustomVision.API.Requests;
using AzureCustomVision.API.Utils;

namespace AzureCustomVision.API.Services
{
    public class AzureVisionService : IAzureVisionService
    {
        private readonly ImageAnalysisClient imageAnalysisClient;

        public AzureVisionService(IOptions<AzureVisionSettings> options)
        {
            var settings = options.Value;

            this.imageAnalysisClient = new ImageAnalysisClient(
                new Uri(settings.Endpoint),
                new AzureKeyCredential(settings.ApiKey));
        }

        public async Task<ImageAnalysisResult> AnalizeImage(FileRequest request)
        {
            var memoryStream = await StreamUtil.ToMemoryStreamAsync(request.Image.OpenReadStream());
            return imageAnalysisClient.Analyze(
                BinaryData.FromStream(memoryStream),
                VisualFeatures.Caption |
                VisualFeatures.Read
                );
        }
    }
}
