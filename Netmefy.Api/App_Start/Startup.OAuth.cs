using System;
using System.Configuration;
//using Netmefy.Core;
//using Netmefy.Identity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using Netmefy.Api;
using Netmefy.Api.App_Start;

namespace Netmefy
{
    public partial class Startup
    {
        public void ConfigureOAuth(IAppBuilder app)
        {
            //var issuer = ConfigurationManager.AppSettings["issuer"];
            //var secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["secret"]);
            /*string issuer = "localhost";
            var secret = "bigote";
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(secret);
            var bigote = System.Convert.ToBase64String(plainTextBytes);
			*/

            /*app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                AllowedAudiences = new[] { "Any" },
                IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                {
                    new SymmetricKeyIssuerSecurityTokenProvider(issuer, bigote)
                }
            });*/
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());


            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth2/token"),
                //AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),                
                Provider = new SimpleAuthorizationServerProvider(),
                //Provider = new CustomOAuthProvider(),
                //AccessTokenFormat = new CustomJwtFormat(issuer)
                //AccessTokenFormat = new CustomJwtFormat()
            });


        }
    }
}
