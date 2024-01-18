using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace MvcAppOpt
{
    public class DecimalModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext context)
        {
            var result = context.ValueProvider.GetValue("bet");
            var dec = double.Parse(result.FirstValue!, CultureInfo.InvariantCulture);
            context.Result = ModelBindingResult.Success(dec);
            return Task.CompletedTask;
        }

        private DecimalModelBinder() { }

        public static DecimalModelBinder Instance { get; } = new();
    }
}
