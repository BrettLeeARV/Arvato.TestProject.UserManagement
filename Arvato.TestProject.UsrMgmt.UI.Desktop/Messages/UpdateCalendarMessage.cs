using System;
using GalaSoft.MvvmLight.Messaging;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Messages
{
    /// <summary>
    /// Message from the ViewModel to the View, to tell the View to update the SelectedDates of a Calendar control
    /// </summary>
    public class UpdateCalendarMessage : MessageBase
    {

        public UpdateCalendarMessage(DateTime start, DateTime end)
        {
            Start = start;
            End = end;
        }

        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        
    }
}