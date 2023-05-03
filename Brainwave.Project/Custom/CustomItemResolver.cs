using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Pipelines.HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gymster.Project.Custom
{
    public class CustomItemResolver:HttpRequestProcessor
    {
        public override void Process(HttpRequestArgs args)
        {
            //http://gymster.dev.local/Books/Outliers
            if (Sitecore.Context.Item == null)
            {
                ///Books/Outliers
                var requestUrl = args.Url.ItemPath;
                var index = requestUrl.LastIndexOf('/');

                if (index > 0)
                {
                    var bookTemplateId = new Sitecore.Data.ID("{C77CBF36-E660-4C1A-AB94-85F172CA180A}");
                    var itemName = requestUrl.Substring(index + 1).Replace("-", " ");//Last lecture

                    using (var context = ContentSearchManager.GetIndex("sitecore_web_index").CreateSearchContext())
                    {
                        var result = context.GetQueryable<SearchResultItem>().
                            Where(f => f.TemplateId == bookTemplateId
                                        && f.Language == Sitecore.Context.Language.Name
                                        && f.Name == itemName).FirstOrDefault();
                        if (result != null)
                            Sitecore.Context.Item = result.GetItem();
                    }
                }
            }
        }
    }
}