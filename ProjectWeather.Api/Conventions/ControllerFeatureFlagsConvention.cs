using Microsoft.AspNetCore.Mvc.ApplicationModels;

namespace ProjectWeather.Api.Conventions
{
    public class ControllerFeatureFlagsConvention : IControllerModelConvention
    {
        public void Apply(ControllerModel controller)
        {
            if (controller.ControllerName == "Second")
            {
                controller.ApiExplorer.IsVisible = false;
            }
        }
    }
}
