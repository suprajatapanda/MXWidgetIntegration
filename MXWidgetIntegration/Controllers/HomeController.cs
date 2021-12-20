using MXWidgetIntegration.Core.Model;
using MXWidgetIntegration.Core.Services;
using System.Web.Mvc;

namespace MXWidgetIntegration.Controllers
{
    public class HomeController : Controller
    {
        private MxPlatformApi userApi = new MxPlatformApi();
        public string Index()
        {
            var response = userApi.CreateUser(new UserCreateRequestBody() 
            { 
                User = new UserCreateRequest() 
                { 
                    Email = "asdasdasad@gmail.com", 
                    Id = "12333radas15", 
                    IsDisabled = false, 
                    Metadata = "fistname2" 
                } 
            });
            var userguid = response.User.Guid;
            var widgeturl = userApi.RequestWidgetURL(userguid, new WidgetRequestBody()
            {
                WidgetUrl = new WidgetRequest()
                {
                    WidgetType = "connect_widget"
                }
            });
            return widgeturl.WidgetUrl.Url;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}