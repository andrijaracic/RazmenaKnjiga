using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace RazmenaKnjiga.Helpers
{
    public class FileUploadOperation : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var fileParams = context.MethodInfo.GetParameters()
                .Where(p => p.ParameterType == typeof(IFormFile));

            if (fileParams.Any())
            {
                operation.RequestBody = new OpenApiRequestBody
                {
                    Content =
                    {
                        ["multipart/form-data"] = new OpenApiMediaType
                        {
                            Schema = new OpenApiSchema
                            {
                                Type = "object",
                                Properties = fileParams.ToDictionary(
                                    p => p.Name,
                                    p => new OpenApiSchema { Type = "string", Format = "binary" }
                                ),
                                Required = fileParams.Select(p => p.Name).ToHashSet()
                            }
                        }
                    }
                };
            }
        }
    }
}
