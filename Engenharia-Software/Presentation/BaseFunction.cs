using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Engenharia_Software.CrossCutting;
using Engenharia_Software.Domain.CrossCutting;
using Engenharia_Software.Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Net;


// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace Engenharia_Software.Presentation
{
    public abstract class BaseFunction
    {
        protected ServiceProvider ServiceProvider { get; set; }
        protected IJsonConverter JsonConverter { get; set; }

        public BaseFunction()
        {
            ServiceProvider = new ServiceCollectionConfiguration().Configure();
            JsonConverter = ServiceProvider.GetService<IJsonConverter>();
        }

        protected APIGatewayProxyResponse BadRequest(string message)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.BadRequest,
                Body = message,
                Headers = new Dictionary<string, string> { { "Content-Type", "text/plain" } }
            };
        }

        protected APIGatewayProxyResponse Ok(object value)
        {
            return new APIGatewayProxyResponse
            {
                StatusCode = (int)HttpStatusCode.OK,
                Body = JsonConverter.SerializeObject(value),
                Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } }
            };
        }

        protected User GetUser(APIGatewayProxyRequest request)
        {
            if (request == null || request.RequestContext == null || request.RequestContext.Authorizer == null || request.RequestContext.Authorizer.Claims == null)
                throw new Exception("Parameter 'Request' not configured. It is not possible to get 'Claims' settings.");

            var claims = request.RequestContext.Authorizer.Claims;

            return new User { Email = claims.GetValueOrDefault("email"), Username = claims.GetValueOrDefault("cognito:username") };
            
        }
    }
}
