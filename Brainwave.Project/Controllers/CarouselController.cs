using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gymster.Project.Controllers
{
    public class CarouselController : Controller
    {
        public ActionResult Index()
        {
            var carouselItem = RenderingContext.Current.Rendering.Item;
            var slidesField = (MultilistField)carouselItem?.Fields["Slides"];
            var slides = slidesField?.GetItems();

            return View("~/Views/Gymster/Components/Carousel.cshtml", slides);
        }
    }
}