using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using Amazon.Lambda.Core;
using Amazon.Lambda.APIGatewayEvents;
using Microsoft.Extensions.DependencyInjection;
using Engenharia_Software.Presentation.ResponseModel;
using Engenharia_Software.Presentation.RequestModel;
using Engenharia_Software.Domain.Infra.IDP;
using Engenharia_Software.Domain.Services;
using Engenharia_Software.Domain.Entities;
using System.Linq;

namespace Engenharia_Software.Presentation
{
    public class Functions : BaseFunction
    {
        public Functions()
        {
       
        }
        
        public APIGatewayProxyResponse Test(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                var user = GetUser(request);
                return Ok(new UserResponse(user));                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public APIGatewayProxyResponse GenerateOauthToken(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                UserRequest user = null;
                if(request == null || string.IsNullOrEmpty(request.Body) || (user = JsonConverter.DeserializeObject<UserRequest>(request.Body)) == null)
                    return BadRequest("Parameter User is not configured.");

                if(! user.IsValid())
                    return BadRequest("Username and Password is not configured.");

                var iidp = ServiceProvider.GetService<IIdentityProvider>();
                var token = iidp.GenerateToken(user.ToModel());

                return Ok(new { Token = token });                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        public APIGatewayProxyResponse Search(APIGatewayProxyRequest request, ILambdaContext context)
        {
            try
            {
                string q = null;
                if (request == null || request.QueryStringParameters == null || !request.QueryStringParameters.ContainsKey("q"))
                    return BadRequest("Parameter 'q' is not configured.");
                
                q = request.QueryStringParameters["q"];
                var searchService = ServiceProvider.GetService<ISearchService>();
                ICollection<Search> results = searchService.Search(q, GetUser(request));
                var resultsResponse = results.Select(x => new SearchResponse(x));

                return Ok(resultsResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
