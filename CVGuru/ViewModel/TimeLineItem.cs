using System;
using System.Collections.Generic;

namespace CVGuru.ViewModel
{
    public class TimeLineItem
    {
        public TimeLineItem()
        {
            
        }

        public TimeLineItem(DateTime startDate, DateTime endDate, string label, string imageURI, List<ExtendedInfoItem> properties)
        {
            StartDate = startDate;
            EndDate = endDate;
            Duration = endDate - startDate;
            Label = label;
            ImageUri = imageURI;
            Properties = properties;
        }

        public string ImageUri { get; set; }
        public string Label { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public TimeSpan Duration { get; set; }
        public List<ExtendedInfoItem> Properties { get; set; }
    }
}
