using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BetEventScanner.Providers
{
    public static class HtmlEx
    {
        public static List<HtmlNode> RemoveTextNodes(this HtmlNodeCollection nodes)
            => nodes.Where(x => x.Name != "#text").ToList();

        public static string TrimmedRemoveLR(this string str)
            => str.Replace("\\r\\n", "").Trim();

        public static HtmlNode GetIdNode(this string html, string id)
            => Load(html).GetIdNode(id);

        public static HtmlNode GetCssNode(this string html, string css)
            => Load(html).GetCssNode(css);

        public static HtmlNode GetIdNode(this HtmlDocument doc, string id)
            => doc.GetElementbyId(id);

        public static HtmlNode GetCssNode(this HtmlDocument doc, string cssSelector)
            => doc.DocumentNode.QuerySelector(cssSelector);

        public static IList<HtmlNode> GetCssNodes(this HtmlDocument doc, string cssSelector)
            => doc.DocumentNode.QuerySelectorAll(cssSelector);

        private static HtmlDocument Load(string html)
        {
            var htmlDoc = new HtmlDocument();
            htmlDoc.LoadHtml(html);
            return htmlDoc;
        }
    }
}