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
        private readonly IVisualFeaturesParser featuresParser;

        public AzureVisionService(IOptions<AzureVisionSettings> options, IVisualFeaturesParser featuresParser)
        {
            var settings = options.Value;
            this.featuresParser = featuresParser;

            this.imageAnalysisClient = new ImageAnalysisClient(
                new Uri(settings.Endpoint),
                new AzureKeyCredential(settings.ApiKey));          
        }

        public async Task<ImageAnalysisResult> AnalyzeImage(FileRequest request, string visualFeatures)
        {
            var memoryStream = await StreamUtil.ToMemoryStreamAsync(request.Image.OpenReadStream());
            var selectedFeatures = this.featuresParser.ParseFeatures(visualFeatures);

            return imageAnalysisClient.Analyze(
                BinaryData.FromStream(memoryStream),
                selectedFeatures
                );
        }
    }
}
