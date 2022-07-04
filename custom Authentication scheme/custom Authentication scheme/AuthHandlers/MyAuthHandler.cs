using custom_Authentication_scheme.Authentication.Data;
using custom_Authentication_scheme.AuthHandlers.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace custom_Authentication_scheme.Authentication
{
    public class MyAuthHandler : AuthenticationHandler<MyAuthSchemeOptions>
    {
        public MyAuthHandler(
            IOptionsMonitor<MyAuthSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            TokenModel model;

            // validation comes in here
            if (!Request.Headers.ContainsKey(HeaderNames.Authorization))
            {
                return Task.FromResult(AuthenticateResult.Fail("Header Not Found."));
            }

            var header = Request.Headers[HeaderNames.Authorization].ToString();
            var tokenMatch = Regex.Match(header, AuthSchemeConstants.MyToken);

            if (tokenMatch.Success)
            {
                // the token is captured in this group
                // as declared in the Regex
                var token = tokenMatch.Groups["token"].Value;

                try
                {
                    model = new TokenBuilder().Decode(token);
                }
                catch (System.Exception ex)
                {
                    Console.WriteLine("Exception Occured while Deserializing: " + ex);
                    return Task.FromResult(AuthenticateResult.Fail("TokenParseException"));
                }

                // success branch
                // generate authTicket
                // authenticate the request
                if (model != null)
                {
                    // create claims array from the model
                    var claims = new[] {
                    new Claim(ClaimTypes.NameIdentifier, model.UserId.ToString()),
                    new Claim(ClaimTypes.Email, model.EmailAddress),
                    new Claim(ClaimTypes.Name, model.Name) };

                    // generate claimsIdentity on the name of the class
                    var claimsIdentity = new ClaimsIdentity(claims,
                                nameof(MyAuthHandler));

                    // generate AuthenticationTicket from the Identity
                    // and current authentication scheme
                    var ticket = new AuthenticationTicket(
                        new ClaimsPrincipal(claimsIdentity), this.Scheme.Name);

                    // pass on the ticket to the middleware
                    return Task.FromResult(AuthenticateResult.Success(ticket));
                }
            }

            // failure branch
            // return failure
            // with an optional message
            return Task.FromResult(AuthenticateResult.Fail("Model is Empty"));
        }

    }
}
