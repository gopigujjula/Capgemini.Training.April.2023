using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gymster.Project.Controllers
{
    public class HeroController : Controller
    {
        public ActionResult Index()
        {
            var dataSourceItem = RenderingContext.Current.Rendering.Item;
            return View("~/Views/Gymster/Components/HeroControllerExample.cshtml", dataSourceItem);
        }
    }
}