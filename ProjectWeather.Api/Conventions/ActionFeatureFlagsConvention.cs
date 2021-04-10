using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ProjectWeather.Api.Conventions
{
    public class ActionFeatureFlagsConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            if (action.Controller.ControllerName == "Second")
            {
                action.ApiExplorer.IsVisible = false;
            }
        }
    }
}
