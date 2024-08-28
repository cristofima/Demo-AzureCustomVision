using Azure.AI.Vision.ImageAnalysis;
using AzureCustomVision.API.Requests;

namespace AzureCustomVision.API.Interfaces
{
    public interface IAzureVisionService
    {
        Task<ImageAnalysisResult> AnalyzeImage(FileRequest request, string visualFeatures);
    }
}
