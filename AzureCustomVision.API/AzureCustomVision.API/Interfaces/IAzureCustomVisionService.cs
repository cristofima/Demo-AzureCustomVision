using AzureCustomVision.API.Requests;
using Microsoft.Azure.CognitiveServices.Vision.CustomVision.Prediction.Models;

namespace AzureCustomVision.API.Interfaces
{
    public interface IAzureCustomVisionService
    {
        Task<IList<PredictionModel>> ClassifyImage(FileRequest request);
        Task<IList<PredictionModel>> DetectObjects(FileRequest request);
    }
}
