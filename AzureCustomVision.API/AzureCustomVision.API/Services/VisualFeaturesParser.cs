using Azure.AI.Vision.ImageAnalysis;
using AzureCustomVision.API.Interfaces;

namespace AzureCustomVision.API.Services
{
    public class VisualFeaturesParser : IVisualFeaturesParser
    {
        public VisualFeatures ParseFeatures(string features)
        {
            if (string.IsNullOrWhiteSpace(features)){
                return VisualFeatures.Caption;
            }

            VisualFeatures selectedFeatures = VisualFeatures.None;

            var featuresList = features.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (var feature in featuresList)
            {
                if (Enum.TryParse(feature, true, out VisualFeatures parsedFeature))
                {
                    selectedFeatures |= parsedFeature;
                }
            }

            return selectedFeatures;
        }
    }
}
