using Azure.AI.Vision.ImageAnalysis;

namespace AzureCustomVision.API.Interfaces
{
    public interface IVisualFeaturesParser
    {
        /// <summary>
        /// Parse the visual features to use in the Azure AI Vision service. If no features are given, it returns Caption by default.
        /// </summary>
        VisualFeatures ParseFeatures(string features);
    }
}
