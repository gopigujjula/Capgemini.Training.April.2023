using Gymster.Project.Models;
using Sitecore.ContentSearch.Linq;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.ContentSearch;
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
    public class BookController : Controller
    {
        public ActionResult Booklist()
        {
            var searchText = Request.QueryString["text"];

            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);//sitecore/content/Capgemini/Home
            var bookTemplateId = new Sitecore.Data.ID("{C77CBF36-E660-4C1A-AB94-85F172CA180A}");
            SearchResults<SearchResultItem> results;

            using (var context = ContentSearchManager.GetIndex((SitecoreIndexableItem)homeItem).CreateSearchContext())
            {
                IQueryable<SearchResultItem> query = context.GetQueryable<SearchResultItem>();
                query = query.Where(f => f.TemplateId == bookTemplateId && f.Paths.Contains(homeItem.ID)
                                        && f.Language == Sitecore.Context.Language.Name);
                if (!string.IsNullOrEmpty(searchText))
                {
                    query = query.Where(f => f.Content.Contains(searchText));
                }
                query = query.OrderByDescending(f => f.CreatedDate)
                  .Page(0, 10);
                results = query.GetResults();
            }

            List<SearchResultItem> resultItems = results?.Hits.Select(f => f.Document).ToList();

            return View("~/Views/Gymster/Components/Booklist.cshtml", resultItems);
        }
    }
}