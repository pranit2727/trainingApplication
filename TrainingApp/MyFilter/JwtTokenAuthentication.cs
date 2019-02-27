using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace TrainingApp.MyFilter
{
    public class JwtTokenAuthentication : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (!IsUserAuthorized(actionContext))
            {
                ShowAuthenticationError(actionContext);
                return;
            }
            base.OnAuthorization(actionContext);
        }

        public bool IsUserAuthorized(HttpActionContext actionContext)
        {
            try
            {
                var idToken = actionContext.Request.Headers.Authorization.Parameter;
                if (idToken != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var jwtToken = tokenHandler.ReadToken(idToken) as JwtSecurityToken;
                    var symmetricKey = Encoding.ASCII.GetBytes("SuperKeySnnhddhdhdhdhdhdhdhdhdhdhddhhdhd");
                    var validationParameters = new TokenValidationParameters()
                    {
                        RequireExpirationTime = true,
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        IssuerSigningKey = new SymmetricSecurityKey(symmetricKey)
                    };
                    // var simplePrinciple = GetPrincipal(idToken);
                    SecurityToken securityToken;
                    var principal = tokenHandler.ValidateToken(idToken, validationParameters, out securityToken);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch(Exception )
            {
                return false;
            }
        }

        public static void ShowAuthenticationError(HttpActionContext filterContext)
        {
            filterContext.Response = filterContext.Request.CreateResponse(HttpStatusCode.Unauthorized, "Unable to access, Please login again");
        }

    }
}