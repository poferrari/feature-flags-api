using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.FeatureManagement;
using Microsoft.FeatureManagement.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace ProjectWeather.Api.Conventions
{
    public class ActionFeatureFlagsConvention : IActionModelConvention
    {
        private readonly IFeatureManager _featureManager;

        public ActionFeatureFlagsConvention(IFeatureManager featureManager)
        {
            _featureManager = featureManager;
        }

        public void Apply(ActionModel action)
        {
            var isVisible = CheckFeatureFlagsAttribute(action.Controller.Attributes);

            if (isVisible)
            {
                isVisible = CheckFeatureFlagsAttribute(action.Attributes);
            }

            action.ApiExplorer.IsVisible = isVisible;
        }

        private bool CheckFeatureFlagsAttribute(IReadOnlyList<object> attributes)
        {
            var isVisible = true;
            if (attributes.Any(att => att.GetType() == typeof(FeatureGateAttribute)))
            {
                var featureGates = attributes.Where(att => att.GetType() == typeof(FeatureGateAttribute))
                    .Cast<FeatureGateAttribute>()
                    .ToList();

                isVisible = CheckFeaturesAreVisible(featureGates);
            }
            return isVisible;
        }

        private bool CheckFeaturesAreVisible(List<FeatureGateAttribute> featureGates)
            => !featureGates.Any(featureGate => featureGate.Features.Any(feature => !_featureManager.IsEnabledAsync(feature).Result));
    }
}
