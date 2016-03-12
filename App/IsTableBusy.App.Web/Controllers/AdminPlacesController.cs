using IsTableBusy.App.Web.ViewModels;
using System.Web.Mvc;

namespace IsTableBusy.App.Web.Controllers
{
    public partial class AdminPlacesController : Controller
    {
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }
    }
}