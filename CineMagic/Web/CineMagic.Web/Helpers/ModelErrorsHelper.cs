namespace CineMagic.Web.Helpers
{
    using System;
    using System.Linq;

    using Microsoft.AspNetCore.Mvc.ModelBinding;

    public class ModelErrorsHelper
    {
        public static string GetModelErorrs(ModelStateDictionary modelState)
        {
            return string.Join(Environment.NewLine, modelState.Values
                .SelectMany(x => x.Errors)
                .Select(x => x.ErrorMessage));
        }
    }
}
