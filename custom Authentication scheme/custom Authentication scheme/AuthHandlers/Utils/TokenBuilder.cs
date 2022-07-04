using custom_Authentication_scheme.Authentication.Data;
using Newtonsoft.Json;
using System.Text;

namespace custom_Authentication_scheme.AuthHandlers.Utils
{
    public class TokenBuilder
    {
        public TokenModel Decode(string token) {
            byte[] fromBase64String = Convert.FromBase64String(token);
            var parsedToken = Encoding.UTF8.GetString(fromBase64String);

            // deserialize the JSON string obtained from the byte array
            var model = JsonConvert.DeserializeObject<TokenModel>(parsedToken);
            return model;
        }

        public string Encode(TokenModel model)
        {
            var json = JsonConvert.SerializeObject(model);
            var byteArr = Encoding.UTF8.GetBytes(json);
            string encodedStr = Convert.ToBase64String(byteArr);
            return encodedStr;
        }
    }
}
