using AzureCustomVision.API.Interfaces;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction;
using Microsoft.Extensions.Options;
using AzureCustomVision.API.Options;
using AzureCustomVision.API.Requests;
using AzureCustomVision.API.Utils;

namespace AzureCustomVision.API.Services
{
    public class AzureCustomVisionService : IAzureCustomVisionService
    {
        private readonly CustomVisionPredictionClient predictionApi;
        private readonly AzureVisionSettings settings;

        public AzureCustomVisionService(IOptions<AzureVisionSettings> options)
        {
            this.settings = options.Value;
            this.predictionApi = new CustomVisionPredictionClient(new ApiKeyServiceClientCredentials(this.settings.Classification.PredictionKey))
            {
                Endpoint = this.settings.Classification.Endpoint
            };
        }

        public async Task<IList<PredictionModel>> ClassifyImage(FileRequest request)
        {
            var memoryStream = await StreamUtil.ToMemoryStreamAsync(request.Image.OpenReadStream());
            var result = this.predictionApi.ClassifyImage(Guid.Parse(this.settings.Classification.ProjectId), this.settings.Classification.PublishedModelName, memoryStream);
            return result.Predictions;
        }

        public async Task<IList<PredictionModel>> DetectObjects(FileRequest request)
        {
            var memoryStream = await StreamUtil.ToMemoryStreamAsync(request.Image.OpenReadStream());
            var result = this.predictionApi.DetectImage(Guid.Parse(this.settings.Detection.ProjectId), this.settings.Detection.PublishedModelName, memoryStream);
            return result.Predictions;
        }
    }
}
