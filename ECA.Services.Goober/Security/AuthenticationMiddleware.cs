using ECA.Services.Goober.Config;
using Microsoft.AspNetCore.Http;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System;
using Microsoft.AspNetCore.Authentication;
using ECA.Services.Goober.Library;

namespace ECA.Services.Goober.Security
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IJsonConfiguration _config;
        public AuthenticationMiddleware(RequestDelegate next, IJsonConfiguration config)
        {
            _next = next;
            _config = config;
        }
        
        public async Task Invoke(HttpContext context)
        {
            var bearer = "bearer ";
            string authHeader = context.Request.Headers["Authorization"];

            if (!string.IsNullOrEmpty(authHeader) && authHeader.ToLower().StartsWith(bearer))
            {
                var tokenString = authHeader.Substring(bearer.Length).Trim();
                var tokenHandler = new JwtSecurityTokenHandler();
                var keyByteArray = _config.JwtSecretKey.FromBase64Url();
                var signingKey = new SymmetricSecurityKey(keyByteArray);

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ClockSkew = TimeSpan.FromMinutes(1),
                    ValidateAudience = false,
                    IssuerSigningKey = signingKey,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _config.JwtIssuer,
                    ValidateIssuer = true,
                    RequireExpirationTime = true,
                    ValidateLifetime = _config.EnforceTokenLife
                };

                try
                {
                    var principal = tokenHandler.ValidateToken(tokenString, tokenValidationParameters, out var rawValidatedToken);
                    var ticket = new AuthenticationTicket(principal, "JwtBearer");
                    context.User = AuthenticateResult.Success(ticket).Principal;
                }
                catch (SecurityTokenExpiredException)
                {
                    //Let the request through to the AllowAnonymousFilter to determine if it is to an open endpoint
                }
                catch (SecurityTokenInvalidIssuerException)
                {
                    //Let the request through to the AllowAnonymousFilter to determine if it is to an open endpoint
                }
            }
            await _next.Invoke(context);
        }
    }
}
