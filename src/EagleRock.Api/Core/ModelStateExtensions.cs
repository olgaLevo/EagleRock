using EagleRock.Api.Data;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EagleRock.Api.Core
{
    public static class ModelStateExtensions
    {
        public static ModelStateDictionary AddValidationResultErrors(this ModelStateDictionary modelState, ErrorsList errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(error.Key, error.Value);
            }

            return modelState;
        }
    }
}
