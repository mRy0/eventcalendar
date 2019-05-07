using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCalendar.CalendarDownloader
{
    public class CalendarManager :IDisposable
    {
        public static CalendarManager Instance = null;

        public IEnumerable<CEvent> Events
        {
            get
            {
                return _events.Values;
            }
        }



        private Dictionary<string, CEvent> _events = new Dictionary<string, CEvent>();
        private System.Threading.Timer _timer;

        public CalendarManager()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                throw new Exception("use instance");
            }


            _timer = new System.Threading.Timer(new System.Threading.TimerCallback(UpdateCalendar));

            //twice a day
            _timer.Change(10000, 12 * 60 * 60 * 1000);




        }

        public void Dispose()
        {
            _timer.Dispose();
        }



        public IEnumerable<Ical.Net.CalendarComponents.CalendarEvent> GetICals()
        {
            return _events.OrderBy(ev => ev.Value.Start).Select(ev => CEventToIcal(ev.Value));
        }

        private Ical.Net.CalendarComponents.CalendarEvent CEventToIcal(CEvent cevent)
        {
            var ev =  new Ical.Net.CalendarComponents.CalendarEvent()
            {
                Start = ToIcalDate(cevent.Start),
                
                End = ToIcalDate(cevent.End.AddDays(1)),
                Description = cevent.Info,
                Summary = cevent.Name,
                Location = cevent.Location,
                IsAllDay = false,
                Uid = "REV_" + cevent.Id,
                
            };

            ev.IsAllDay = true;
            return ev;
        }


        public static Ical.Net.DataTypes.CalDateTime ToIcalDate(DateTime time)
        {
            return new Ical.Net.DataTypes.CalDateTime(time.Year, time.Month, time.Day);


            //return new Ical.Net.DataTypes.CalDateTime(time.ToUniversalTime());
        }

        private void UpdateCalendar(object state)
        {
            //load x events
            try
            {
                var xEvs = XPageDownloader.GetEvents();
                foreach (var ev in xEvs)
                {
                    if (!_events.ContainsKey(ev.Id))
                    {
                        _events.Add(ev.Id, ev);
                    }
                }

            }
            catch (Exception ex)
            {
                Logger.Log("error while parsing all x events");
              
            }



            var remBef = DateTime.Now.AddDays(-7);
            var keys = _events.Keys.ToArray();
            foreach(var key in keys)
            {
                if(_events[key].Start < remBef)
                {
                    _events.Remove(key);
                }
            }

        }
    }
}
