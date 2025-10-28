using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.ErrorModels;

namespace E_Commerce.Factories
{
    public static class APIResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext Context)
        {
              // InvalidModelStateResponseFactory =>  function to specialize the response when there's Validation Error. 
            
                var Errors = Context.ModelState.Where(M => M.Value.Errors.Any())  //بنختار الفيلدز بس اللي فيها errors .
                                    .Select(M => new ValidationErrors()
                                    {
                                        field = M.Key,
                                        Errors = M.Value.Errors.Select(E => E.ErrorMessage)
                                    });
                var Response = new ValidationErrorToReturn()
                {
                    ValidationErrors = Errors
                };
                return new BadRequestObjectResult(Response);
            
        }
    }
}
