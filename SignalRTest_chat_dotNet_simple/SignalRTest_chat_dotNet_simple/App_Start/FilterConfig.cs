using System.Web;
using System.Web.Mvc;

namespace SignalRTest_chat_dotNet_simple
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
