using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.ObjectModel;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{

    public class BookingsCreateViewModel : ViewModelBase
    {

        public class TimeComboBoxItem
        {
            public string ValueString
            {
                get
                {
                    return new DateTime(0).Add(Time).ToShortTimeString();
                }
            }
            public TimeSpan Time
            {
                get;
                set;
            }
        }

        #region Private fields

        private IBookingService _bookingService;
        private IRoomService _roomService;

        private Booking _booking; // don't expose properties of this object, set manually in code
        private Room _room;

        // Separate date and time into two fields/properties, so we can validate date and time fields separately
        private DateTime _startDate;
        private TimeSpan _startTime;
        private DateTime _endDate;
        private TimeSpan _endTime;

        // ComboBox options
        private ObservableCollection<TimeComboBoxItem> _allTimeOptions;

        // Status fields
        private bool _isConflicting;

        #endregion

        public BookingsCreateViewModel()
        {
            // Initialize fields
            if (IsInDesignMode)
            {
                // Hardcoded design time data
                RoomList = new ObservableCollection<Room>()
                {
                    new Room() { Name = "Winterfell", Location = "25th floor", Capacity = 10 },
                    new Room() { Name = "King's Landing", Location = "24th floor", Capacity = 8 }
                };
            }
            else
            {
                _bookingService = new BookingService();
                _roomService = new RoomService();
                RoomList = new ObservableCollection<Room>(_roomService.GetList());
            }

            RoomAssets = "Epson projector, whiteboard";

            _allTimeOptions = new ObservableCollection<TimeComboBoxItem>();
            // Generate TimeComboBoxitems from 00:00 to 23:30, in 30 minute increments
            for (var i = 0; i < 48; i++)
            {
                var hours = i / 2;
                var minutes = (i % 2 == 0) ? 0 : 30;
                _allTimeOptions.Add(new TimeComboBoxItem() { Time = new TimeSpan(hours, minutes, 0) });
            }

            _isConflicting = false;

            // Wire up commands
            MakeBookingCommand = new RelayCommand(this.MakeBooking);
        }

        #region Properties

        public DateTime StartDate
        {
            get
            {
                return _startDate;
            }
            set
            {
                if (value == _startDate)
                {
                    return;
                }
                _startDate = value;
                RaisePropertyChanged("StartDate");
            }
        }
        public TimeSpan StartTime
        {
            get
            {
                return _startTime;
            }
            set
            {
                if (value == _startTime)
                {
                    return;
                }
                _startTime = value;
                RaisePropertyChanged("StartTime");
            }
        }
        public DateTime EndDate
        {
            get
            {
                return _endDate;
            }
            set
            {
                if (value == _endDate)
                {
                    return;
                }
                _endDate = value;
                RaisePropertyChanged("EndDate");
            }
        }
        public TimeSpan EndTime
        {
            get
            {
                return _endTime;
            }
            set
            {
                if (value == _endTime)
                {
                    return;
                }
                _endTime = value;
                RaisePropertyChanged("EndTime");
            }
        }
        public bool IsConflicting
        {
            get
            {
                return _isConflicting;
            }
            set
            {
                if (value == _isConflicting)
                {
                    return;
                }
                _isConflicting = value;
                RaisePropertyChanged("IsConflicting");
            }
        }

        public Room Room
        {
            get
            {
                return _room;
            }
            set
            {
                if (value == _room)
                {
                    return;
                }
                _room = value;
                RaisePropertyChanged("Room");
            }
        }

        public ObservableCollection<Room> RoomList
        {
            get;
            private set;
        }

        public string RoomAssets
        {
            get;
            private set;
        }

        public ObservableCollection<TimeComboBoxItem> StartTimeOptions
        {
            get
            {
                return _allTimeOptions;
            }
            private set
            {
                if (value == _allTimeOptions)
                {
                    return;
                }
                _allTimeOptions = value;
                RaisePropertyChanged("StartTimeOptions");
            }
        }
        public ObservableCollection<TimeComboBoxItem> EndTimeOptions
        {
            get
            {
                return _allTimeOptions;
            }
            private set
            {
                if (value == _allTimeOptions)
                {
                    return;
                }
                _allTimeOptions = value;
                RaisePropertyChanged("EndTimeOptions");
            }
        }

        #endregion

        #region Commands

        public ICommand MakeBookingCommand
        {
            get;
            private set;
        }

        #endregion

        #region Command methods

        private void MakeBooking()
        {

        }

        #endregion

    }
}
