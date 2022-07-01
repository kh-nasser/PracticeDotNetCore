namespace WebRepository
{
    public class Cookies
    {

        /// <summary>  
        /// Get the cookie  
        /// </summary>  
        /// <param name="key">Key </param>  
        /// <param name="httpRequest">HttpRequest </param>  
        /// <returns>string value</returns>  
        public string Get(HttpRequest httpRequest,string key)
        {
            return httpRequest.Cookies[key];
        }
        /// <summary>  
        /// set the cookie  
        /// </summary>  
        /// <param name="key">key (unique indentifier)</param>  
        /// <param name="value">value to store in cookie object</param>  
        /// <param name="expireTime">expiration time</param>  
        /// <param name="httpResponse">HttpResponse</param>  
        public void Set(HttpResponse httpResponse,string key, string value, int? expireTime)
        {
            CookieOptions option = new CookieOptions();
            if (expireTime.HasValue)
                option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTime.Now.AddMilliseconds(10);
            httpResponse.Cookies.Append(key, value, option);
        }
        /// <summary>  
        /// Delete the key  
        /// </summary>  
        /// <param name="key">Key</param>  
        /// <param name="httpResponse">httpResponse</param>  
        public void Remove(HttpResponse httpResponse, string key)
        {
            httpResponse.Cookies.Delete(key);
        }
    }
}
