using System.Web.Mvc;
using Twilio.TwiML.Mvc;

namespace WarmTransfer.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}