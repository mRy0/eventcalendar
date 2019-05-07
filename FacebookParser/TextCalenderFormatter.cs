using Microsoft.AspNetCore.Mvc.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventCalendar
{
    public class TextCalendarFormatter : StringOutputFormatter
    {
        public TextCalendarFormatter()
        {
            SupportedMediaTypes.Add("text/calendar");
        }
    }
}
