using IsTableBusy.App.Web.ViewModels;
using IsTableBusy.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IsTableBusy.App.Web.Controllers
{
    public partial class PlacesController : Controller
    {
        [HttpGet]
        public virtual ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Route("places/{placeName}/tables")]
        public virtual ActionResult Tables(string placeName)
        {
            var model = new TablesViewModel() { PlaceName = placeName };
            return View(Views.Tables, model);
        }
    }
}