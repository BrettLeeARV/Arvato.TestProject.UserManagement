﻿using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{

    public class BookingsCreateViewModel : PageViewModel
    {
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
            : base()
        {
            _booking = new Booking();
            // Default booking dates are today
            _startDate = DateTime.Today;
            _endDate = DateTime.Today;
            // Default booking start time is now + rounded up to next half hour
            var startHour = DateTime.Now.Hour;
            var startMinute = DateTime.Now.Minute;
            if (startMinute < 30 && startMinute > 0)
            {
                startMinute = 30;
            }
            else if (startMinute > 30)
            {
                startMinute = 0;
                startHour++;
            }
            _startTime = new TimeSpan(startHour, startMinute, 0);
            // Default booking end time is one hour later
            _endTime = _startTime + TimeSpan.FromHours(1);
            
            // Initialize fields
            if (IsInDesignMode)
            {
                // Hardcoded design time data
                RoomList = new ObservableCollection<Room>()
                {
                    new Room() { Name = "Winterfell", Location = "25th floor", Capacity = 10 },
                    new Room() { Name = "King's Landing", Location = "24th floor", Capacity = 8 }
                };

                _isConflicting = true;

                RoomAssets = "Epson projector, whiteboard";
            }
            else
            {
                _bookingService = new BookingService();
                _roomService = new RoomService();
                _isConflicting = false;
            }

            // Generate TimeComboBoxitems from 00:00 to 23:30, in 30 minute increments
            _allTimeOptions = new ObservableCollection<TimeComboBoxItem>();
            for (var i = 0; i < 48; i++)
            {
                var hours = i / 2;
                var minutes = (i % 2 == 0) ? 0 : 30;
                _allTimeOptions.Add(new TimeComboBoxItem() { Time = new TimeSpan(hours, minutes, 0) });
            }

            // Wire up commands
            MakeBookingCommand = new RelayCommand(this.MakeBooking, () => !IsConflicting);
            CancelCommand = new RelayCommand(this.Cancel);
        }

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
        public TimeSpan CustomStartTime
        {
            get
            {
                return this.StartTime;
            }
            set
            {
                this.StartTime = value;
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
        public TimeSpan CustomEndTime
        {
            set
            {
                this.EndTime = value;
            }
            get
            {
                return this.EndTime;
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
        public ICommand CancelCommand
        {
            get;
            private set;
        }

        #endregion

        protected override void OnNavigatingTo(object s, EventArgs e)
        {
            if(!IsInDesignMode)
            {
                RefreshRooms();
            }
        }

        private void RefreshRooms()
        {
            RoomList = new ObservableCollection<Room>(_roomService.GetList());
        }

        #region Command methods

        private void MakeBooking()
        {
            // Fill in the blanks
            _booking.RoomID = _room.ID;
            _booking.StartDate = StartDate.Add(StartTime);
            _booking.EndDate = EndDate.Add(EndTime);
            _booking.UserID = StateManager.CurrentUser.ID;

            try
            {
                _bookingService.AddBooking(_booking);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error creating booking", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("Your booking has been made!", "Booking created", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private void Cancel()
        {
            MessengerInstance.Send(new ChangePageMessage(typeof(BookingsListViewModel)));
        }

        #endregion

    }
}
