using System.Web.Mvc;
using WarmTransfer.Web.Domain;

namespace WarmTransfer.Web.Controllers
{
    public class TokenController : Controller
    {
        [HttpPost]
        public ActionResult Generate(string agentId)
        {
            var result = new
            {
                token = CapabilityGenerator.Generate(agentId),
                agentId
            };
            
            return Json(result);
        }
    }
}