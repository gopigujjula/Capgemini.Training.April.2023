using Gymster.Project.Models;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Links;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Gymster.Project.Controllers
{
    public class HeaderController : Controller
    {
        public ActionResult Index()
        {
            List<Navigation> navigations = new List<Navigation>();
            HeaderModel model = new HeaderModel();

            var homeItem = Sitecore.Context.Database.GetItem(Sitecore.Context.Site.StartPath);
            
            navigations.Add(new Navigation
            {
                NavigationTitle = homeItem.Fields["Navigation Title"]?.Value,
                NavigationUrl = LinkManager.GetItemUrl(homeItem),
                IsActiveLink = homeItem.ID == PageContext.Current.Item.ID
            });

            foreach (Item childItem in homeItem.Children)
            {
                var showInNavField = (CheckboxField)childItem.Fields["Show In Navigation"];
                if (showInNavField != null && showInNavField.Checked)
                {
                    navigations.Add(new Navigation
                    {
                        NavigationTitle = childItem.Fields["Navigation Title"]?.Value ?? childItem.Name,
                        NavigationUrl = LinkManager.GetItemUrl(childItem),
                        IsActiveLink = childItem.ID == PageContext.Current.Item.ID,
                        Children = GetNavigations(childItem.Children.ToList())
                    });
                }
            }
            model.Navigations = navigations;
            return View("~/Views/Gymster/Components/HeaderNavigation.cshtml", model);
        }

        private List<Navigation> GetNavigations(List<Item> navigationChildren)
        {
            List<Navigation> navigations = new List<Navigation>();
            HeaderModel model = new HeaderModel();

            foreach (Item childItem in navigationChildren)
            {
                navigations.Add(new Navigation
                {
                    NavigationTitle = childItem.Fields["Navigation Title"]?.Value,
                    NavigationUrl = LinkManager.GetItemUrl(childItem),
                    IsActiveLink = childItem.ID == PageContext.Current.Item.ID
                });
            }
            return navigations;
        }
    }
}