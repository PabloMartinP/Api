using Microsoft.Owin.Security.OAuth;
using Netmefy.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Netmefy.Api.App_Start
{
    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {

            ////////////////////////////////////////////////////////
            string clientId = "";
            string clientSecret = "";

            context.TryGetFormCredentials(out clientId, out clientSecret);
            if (clientId != "Utn.Ba$")
                context.Rejected();
            else
                context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            LoginService ls = new LoginService();

            //bool ok1 = ls.login(context.UserName, context.Password);

            //UsuarioRepository ur = new UsuarioRepository();
            //var ok = ur.Validar(context.UserName, context.Password);
            //bool ok = context.UserName.ToLower().Equals("netmefy") && context.Password.ToLower().Equals("yfemten");
            //bool ok = context.UserName.ToLower().Equals("1234") || context.UserName.ToLower().Equals("5678");
            bool ok = ls.login(context.UserName, context.Password); 
            if (!ok)
            {
                context.SetError("Invalido", "El nombre de usuario o constraseña es incorrecto");
                return;
            }
            /*
            ok = context.UserName.ToLower().Equals("1234") && context.Password.ToLower().Equals("yfemten");
            if (!ok)
            {
                context.SetError("Invalido", "El nombre de usuario o constraseña es incorrecto");
                return;
            }*/


            /*
            using (AuthRepository _repo = new AuthRepository())
            {
                IdentityUser user = await _repo.FindUser(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }
            }*/

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim("role", "user"));

            context.Validated(identity);

        }
    }
}