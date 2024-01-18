using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MvcAppOpt
{
    public class DecimalModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder? GetBinder(ModelBinderProviderContext context)
        {
            return context.Metadata.ModelType == typeof(double) ? DecimalModelBinder.Instance : null;
        }

        private DecimalModelBinderProvider() { }

        public static DecimalModelBinderProvider Instance { get; } = new();
    }
}
