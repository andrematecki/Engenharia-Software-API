using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using Engenharia_Software.Domain.Entities;
using Engenharia_Software.Domain.Infra.IDP;
using Microsoft.Extensions.Configuration;
using System;

namespace Engenharia_Software.Infra.IDP
{
    public class IdentityProvider : IIdentityProvider
    {
        private string _clientId;
        private string _userPoolId;
        private RegionEndpoint _region;
        private string _accessKey;
        private string _secretKey;

        public IdentityProvider(IConfiguration config)
        {
            
            _clientId = config.GetValue<string>("AWS:Cognito:ClientId");
            _userPoolId = config.GetValue<string>("AWS:Cognito:UserPoolId");
            _region = RegionEndpoint.GetBySystemName(config.GetValue<string>("AWS:Cognito:Region"));
            _accessKey = config.GetValue<string>("AWS:ProgrammaticUser:AccessKey");
            _secretKey = config.GetValue<string>("AWS:ProgrammaticUser:SecretKey");
        }

        public string GenerateToken(User user)
        {
            var cognito = new AmazonCognitoIdentityProviderClient(new BasicAWSCredentials(_accessKey, _secretKey), _region);

            var req = new AdminInitiateAuthRequest
            {
                UserPoolId = _userPoolId,
                ClientId = _clientId,
                AuthFlow = AuthFlowType.ADMIN_NO_SRP_AUTH
            };

            req.AuthParameters.Add("USERNAME", user.Username);
            req.AuthParameters.Add("PASSWORD", user.Password);

            var response = cognito.AdminInitiateAuthAsync(req).Result;

            if (response != null && response.AuthenticationResult != null && !string.IsNullOrEmpty(response.AuthenticationResult.IdToken))
                return response.AuthenticationResult.IdToken;

            throw new Exception("Erro to call AmazonCognitoIdentityProviderClient.AdminInitiateAuthAsync.");
        }
    }
}
