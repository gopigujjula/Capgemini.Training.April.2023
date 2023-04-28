using Gymster.Project.Models;
using Sitecore.Data.Fields;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
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
            var parameter = RenderingContext.Current.Rendering.Parameters["Slide Size"];
            int.TryParse(parameter, out int count);
            var carouselItem = RenderingContext.Current.Rendering.Item;
            var slidesField = (MultilistField)carouselItem?.Fields["Slides"];
            var slides = slidesField?.GetItems();            

            return View("~/Views/Gymster/Components/Carousel.cshtml", slides.Take(count).ToList());
        }

        public ActionResult CarouselModelExample()
        {
            var datasource = RenderingContext.Current.Rendering?.Item;            
            var slideCountParameter = RenderingContext.Current.Rendering.Parameters["Slide Size"];
            int.TryParse(slideCountParameter, out int result);

            MultilistField slidesField = datasource?.Fields["Slides"];
            var slideItems = slidesField?.GetItems();//carousel slide items
            int slideCount = result == 0 ? slideItems.Count() : result;
            int counter = 0;
            List<SlideModel> slides = new List<SlideModel>();            
            foreach (var slideItem in slideItems?.Take(slideCount))
            {
                slides.Add(new SlideModel
                {
                    //@Html.Sitecore().Field("Title",slideItem)
                    SlideTitle = new MvcHtmlString(FieldRenderer.Render(slideItem, "Title")),
                    SlideSubtitle = slideItem.Fields["Subtitle"]?.Value,//new MvcHtmlString(FieldRenderer.Render(slideItem, "Subtitle")),
                    //@Html.Sitecore().Field("Image", slideItem, new { @class = "w-100" })
                    SlideImage = new MvcHtmlString(FieldRenderer.Render(slideItem, "Image", "class=w-100")),
                    PrimaryLink = new MvcHtmlString(FieldRenderer.Render(slideItem, "Primary Link", "class=btn btn-primary py-md-3 px-md-5 me-3")),
                    SecondaryLink = new MvcHtmlString(FieldRenderer.Render(slideItem, "Secondary Link", "class=btn btn-light py-md-3 px-md-5")),
                    IsActive = counter == 0 ? true : false
                });
                counter++;
            }

            return View("~/Views/Gymster/Components/CarouselModelExample.cshtml",
                new CarouselModel
                {
                    Slides = slides
                });
        }
    }
}