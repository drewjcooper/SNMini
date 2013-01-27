using HtmlAgilityPack;
using SNMini.Core;
using SNMini.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace SNMini.Controllers
{
    public class FeedController : Controller
    {
        private Regex itemRegex;
        private Dictionary<string, int> months = new Dictionary<string, int>()
        {
            {"Jan", 1}, {"Feb", 2}, {"Mar", 3}, {"Apr", 4},
            {"May", 5}, {"Jun", 6}, {"Jul", 7}, {"Aug", 8},
            {"Sep", 9}, {"Oct", 10}, {"Nov", 11}, {"Dec", 12}
        };

        public FeedController()
        {
            itemRegex = new Regex(@"<font size=1>Episode&nbsp;#(?<Episode>\d+) \| (?<Day>\d+) (?<Month>\w{3})\w? (?<Year>\d+) \| (?<Duration>\d+) +min.</font>.+?<font size=1><font size=2><b>(?<Title>.+?)</b>.+<br( /)?>(?<Description>.+?)</font>.+?lq\.mp3.+?<font size=1>(?<Filesize>[\d\.]+)", RegexOptions.Compiled | RegexOptions.Singleline);
        }
        //
        // GET: /Feed/

        public ActionResult Generate(int? quantity)
        {
            HttpContext.Response.ContentType = "application/rss+xml";
            var feed = new Feed { Channel = GenerateChannel(quantity ?? 10) };
            return View(feed);
        }

        [NonAction]
        private Channel GenerateChannel(int quantity)
        {
            return new Channel
            {
                Title = "Security Now! - 16kbps",
                Link = "http://grc.com/securitynow.htm",
                Description = "Steve Gibson, the man who coined the term spyware and created the first anti-spyware program, creator of Spinrite and ShieldsUP, discusses the hot topics in security today with Leo Laporte. Winner of the 2009 and 2007 people's choice award for best Technology/Science podcast.",
                Copyright = "Creative Commons non-commercial-attribution- 2.0 http://creativecommons.org/licenses/by-nc-sa/2.0/",
                ManagingEditor = "leo@leoville.com (Leo Laporte)",
                WebMaster = "leo@leoville.com (Leo Laporte)",
                PubDate = DateTime.Now,
                LastBuildDate = DateTime.Now,
                Ttl = 720,
                AtomLink = new AppUrl(HttpContext.Request.Url).ToString(),
                Image = GenerateImage(),
                ITunes = GenerateITunes(),
                Items = GenerateItems().Take(quantity)
            };
        }

        [NonAction]
        private ITunes GenerateITunes()
        {
            return new ITunes
            {
                Author = "TWiT",
                Subtitle = "A weekly look at security issues with Steve Gibson of ShieldsUP!",
                Summary = "Steve Gibson, the man who coined the term spyware and created the first anti-spyware program, creator of Spinrite and ShieldsUP, discusses the hot topics in security today with Leo Laporte. Winner of the 2009 and 2007 people's choice award for best Technology/Science podcast.",
                Keywords = "Security, Technology",
                Image = new AppUrl( HttpContext.Request.Url, Url.Content("~/Images/sn600audio16.jpg")).ToString(),
                Explicit = "no",
                Owner = new Owner
                {
                    Name = "Leo Laporte",
                    Email = "leo@leoville.com"
                },
                Block = "no",
                Category = new Category
                {
                    Text = "Technology",
                    SubCategory = new Category
                    {
                        Text = "Tech News"
                    }
                }
            };
        }

        [NonAction]
        private Image GenerateImage()
        {
            return new Image
            {
                Url = new AppUrl(HttpContext.Request.Url, Url.Content("~/Images/sn144audio16.jpg")).ToString(),
                Title = "Security Now! - 16kbps",
                Link = "http://grc.com/securitynow.htm",
                Width = 140,
                Height = 140,
                Description = "A weekly look at security issues with Steve Gibson of ShieldsUP!"
            };
        }

        [NonAction]
        private IEnumerable<Item> GenerateItems()
        {
            string grcPage;
            using (var web = new WebClient())
            {
                grcPage = web.DownloadString("http://grc.com/securitynow.htm").Replace(Environment.NewLine, "");
            }
            return Regex.Split(grcPage, @"<a name=""\d+""></a>").Skip(1).Select(html => GenerateItem(html));
        }

        [NonAction]
        private Item GenerateItem(string html)
        {
            var item = new Item();
            var match = itemRegex.Match(html);
            item.Title = "Security Now " + match.Groups["Episode"].Value + ": " + match.Groups["Title"].Value;
            item.Description = match.Groups["Description"].Value;
            item.Author = "leo@leoville.com (Leo Laporte)";
            item.PubDate = new DateTime(Int32.Parse(match.Groups["Year"].Value), months[match.Groups["Month"].Value], Int32.Parse(match.Groups["Day"].Value), 0, 0, 0, DateTimeKind.Local);
            item.Categories = new[] { "Gadgets", "Tech News" };
            item.Comments = "http://twit.tv/sn/" + match.Groups["Episode"].Value;
            item.ITunes = new ITunes
            {
                Author = "TWiT",
                Summary = match.Groups["Description"].Value,
                Explicit = "no",
                Duration = new TimeSpan(0, Int32.Parse(match.Groups["Duration"].Value), 0)
            };
            item.Url = String.Format("http://media.grc.com/sn/sn-{0}-lq.mp3", match.Groups["Episode"].Value);
            item.Filesize = (int)(double.Parse(match.Groups["Filesize"].Value) * 1024 * 1024);
            return item;
        }
    }
}
