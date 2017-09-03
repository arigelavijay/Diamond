using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NetStock
{
    public static class HtmlHelpers
    {
        public static bool hasRight(string securableItem)
        {
            if (!string.IsNullOrWhiteSpace(securableItem))
            {
                var securables = (List<NetStock.Contract.Securables>)System.Web.HttpContext.Current.Session["SsnSecurables"];
                return securables.Where(x => x.SecurableItem == securableItem).Count() > 0;
            }

            return false;
        }
    }
}