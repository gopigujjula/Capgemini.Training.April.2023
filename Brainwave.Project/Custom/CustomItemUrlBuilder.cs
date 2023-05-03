using Sitecore.Buckets.Extensions;
using Sitecore.Buckets.Managers;
using Sitecore.Data.Items;
using Sitecore.IO;
using Sitecore.Links.UrlBuilders;
using Sitecore.Web.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Gymster.Project.Custom
{
    public class CustomItemUrlBuilder : ItemUrlBuilder
    {
        public CustomItemUrlBuilder(DefaultItemUrlBuilderOptions defaultOptions) : base(defaultOptions)
        {
        }

        public override string Build(Item item, ItemUrlBuilderOptions options)
        {
            //Sitecore/Conetnt/Gymster/Home/Shop/Books/2019/05/11/10/50/Outliers
            //gymster.dev.local/Shop/Books/2019/05/11/10/50/Outliers
            //gymster.dev.local/Shop/Books/Outliers
            //gymster.dev.local/Shop/Books+outliers
            if (BucketManager.IsItemContainedWithinBucket(item))
            {
                var bucketItem = item.GetParentBucketItemOrParent();//Books
                if (bucketItem != null && bucketItem.IsABucket())
                {
                    //gymster.dev.local/Shop/Books
                    var bucketUrl = base.Build(bucketItem, options);
                    return FileUtil.MakePath(bucketUrl, item.Name.Replace(' ', '-'));
                }
            }
            return base.Build(item, options);
        }
    }
}