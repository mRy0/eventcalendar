using Ical.Net.Serialization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCalendar.Controllers
{
    public class CalController : Controller
    {
        [HttpGet("cal/all.ics")]
        [Produces("text/calendar")]
        public IActionResult GetCal()
        {
            try
            {
                var calendar = new Ical.Net.Calendar();
                //caldav outlook props
                calendar.Properties.Add(new Ical.Net.CalendarProperty("X-WR-CALNAME", "Veranstaltungen"));
                calendar.Properties.Add(new Ical.Net.CalendarProperty("X-PUBLISHED-TTL", "PT1H ")); //update every hour //PT1H  PT1M
                calendar.Method = "PUBLISH";

                calendar.Events.AddRange(CalendarDownloader.CalendarManager.Instance.GetICals());


                var serializer = new CalendarSerializer();
                var serializedCalendar = serializer.SerializeToString(calendar);
                return Ok(serializedCalendar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return this.NotFound();
            }
        }


        [HttpGet("cal/dbg.ics")]
        public IActionResult GetDbgCal(string id)
        {
            try
            {

                var calendar = new Ical.Net.Calendar();
                //caldav outlook props
                calendar.Properties.Add(new Ical.Net.CalendarProperty("X-WR-CALNAME", "Veranstaltungen"));
                calendar.Properties.Add(new Ical.Net.CalendarProperty("X-PUBLISHED-TTL", "PT1H ")); //update every hour //PT1H  PT1M
                calendar.Method = "PUBLISH";

                calendar.Events.AddRange(CalendarDownloader.CalendarManager.Instance.GetICals());


                var serializer = new CalendarSerializer();
                var serializedCalendar = serializer.SerializeToString(calendar);
                return Ok(serializedCalendar);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return this.NotFound();
            }
        }
    }
}
