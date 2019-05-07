using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCalendar
{
    public class CEvent
    {
        public string Id { set; get; }
        public string Name { set; get; }
        public string Info { set; get; }
        public DateTime Start { set; get; }
        public DateTime End { set; get; }
        public string Location { set; get; }
    }
}
