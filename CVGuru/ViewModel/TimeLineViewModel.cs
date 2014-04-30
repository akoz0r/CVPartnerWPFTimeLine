using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace CVGuru.ViewModel
{
    public class TimeLineViewModel : ViewModelBase
    {
        private TimeLineItem selectedItem;
        public List<TimeLineItem> Segments { get; set; }
        public TimeLineItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                if (Equals(value, selectedItem)) return;
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public DateTime StartDate
        {
            get { return Segments.Min(s => s.StartDate).AddDays(-1); }
        }

        public DateTime EndDate
        {
            get { return Segments.Max(s => s.EndDate).AddDays(1); }
        }
    }
}
