using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace BetEventScanner.Providers
{
    public static class HtmlEx
    {
        public static HtmlNode GetIdNode(this HtmlDocument doc, string id)
        {
            return doc.GetElementbyId(id);
        }

        public static HtmlNode GetCssNode(this HtmlDocument doc, string cssSelector)
        {
            return doc.DocumentNode.QuerySelector(cssSelector);
        }

        public static IList<HtmlNode> GetCssNodes(this HtmlDocument doc, string cssSelector)
        {
            return doc.DocumentNode.QuerySelectorAll(cssSelector);
        }
    }
}