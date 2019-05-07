using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using HtmlAgilityPack;

namespace EventCalendar.CalendarDownloader
{
    public class XPageDownloader
    {
        private const string Url = "https://x-herford.de/alle-events-im-x-herford/";
        private static System.Text.RegularExpressions.Regex _dateRegEx = new System.Text.RegularExpressions.Regex(@"((0[1-9]|[12]\d|3[01])-(0[1-9]|1[0-2])-[12]\d{3})");


        public static IEnumerable<CEvent> GetEvents()
        {
            var events = new List<CEvent>();


            var web = new HtmlWeb();
            var htmlDoc = web.Load(Url);


            var eventNodes = htmlDoc.DocumentNode.SelectNodes("//div[contains(@class, 'srp-widget-singlepost srp-post-single-column')]/*");

            foreach (var eventNode in eventNodes)
            {
                try
                {

                    var headLink = eventNode.SelectSingleNode(".//a[contains(@class, 'srp-post-title-link')]");

                    var content = eventNode.SelectSingleNode(".//div[contains(@class, 'srp-post-content')]");

                    var id = headLink.GetAttributeValue("href", "");
                    id = id.Replace("/", "");

                    var name = headLink.InnerText;

                    var text = content.InnerText;

                    var dReg = _dateRegEx.Match(name).Value.Split('-');

                    var time = new DateTime(int.Parse(dReg[2]), int.Parse(dReg[1]), int.Parse(dReg[0]));




                    events.Add(new CEvent()
                    {
                        Id = id,
                        Info = text,
                        Name = name,
                        Start = time,
                        End = time,
                        Location = "X-Herford"
                    });
                }
                catch(Exception ex)
                {
                    Logger.Log("Cant parse x event");
                }
                }
            return events;
        }

    }
}
