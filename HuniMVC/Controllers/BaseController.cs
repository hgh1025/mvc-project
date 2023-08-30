using Azure.Core;
using HuniMVC.ActionResults;
using HuniMVC.Helpers;
using Microsoft.AspNetCore.Mvc;

using System.Globalization;


namespace RealWeb.RealPMS.WebSite.Infrastructure
{
    public abstract class BaseController : Controller
    {


        protected StandardJsonResult JsonValidationError()
        {
            var result = new StandardJsonResult();

            foreach (var validationError in ModelState.Values.SelectMany(v => v.Errors))
            {
                result.AddError(validationError.ErrorMessage);
            }
            return result;
        }

        protected StandardJsonResult JsonError(string errorMessage)
        {
            var result = new StandardJsonResult();

            result.AddError(errorMessage);

            return result;
        }

        protected StandardJsonResult<T> JsonSuccess<T>(T data)
        {
            return new StandardJsonResult<T> { Value = data };
        }

        public async Task<IActionResult> SomeAction()
        {
            string cultureName = null;

            // Attempt to read the culture cookie from Request
            string cultureCookie = Request.Cookies["LOCALEKEY"];
            if (cultureCookie != null)
                cultureName = cultureCookie;
            else
                cultureName = Request.Headers["Accept-Language"].FirstOrDefault() ?? null;

            // Validate culture name
            cultureName = CultureHelper.GetImplementedCulture(cultureName); // Assuming you have this method

            // Modify current thread's cultures            
            System.Globalization.CultureInfo cultureInfo = new System.Globalization.CultureInfo(cultureName);
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;

            return View(); // Or other action logic
        }
    }
}