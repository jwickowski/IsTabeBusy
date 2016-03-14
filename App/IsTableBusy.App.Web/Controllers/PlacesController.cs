using IsTableBusy.App.Web.ViewModels;
using System.Web.Mvc;

namespace IsTableBusy.App.Web.Controllers
{
    public partial class PlacesController : Controller
    {
        [HttpGet]
        [Route("places/{placeName}")]
        public virtual ActionResult Tables(string placeName)
        {
            var model = new TablesViewModel() { PlaceName = placeName };
            return View(Views.Tables, model);
        }
    }
}