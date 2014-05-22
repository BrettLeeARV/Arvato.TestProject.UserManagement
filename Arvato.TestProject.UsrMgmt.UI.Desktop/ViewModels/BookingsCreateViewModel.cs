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
    // Temporary for design time
    public class Room
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public int Capacity { get; set; }
    }

    public class BookingsCreateViewModel : ViewModelBase
    {

        #region Private fields

        //private IRoomService _roomService;
        private IBookingService _bookingService;

        private Room _room;
        private Booking _booking; // don't expose properties of this object, set manually in code

        // Separate date and time into two fields/properties, so we can validate date and time fields separately
        private DateTime _startDate;
        private DateTime _startTime;
        private DateTime _endDate;
        private DateTime _endTime;

        // Status fields
        private bool _isConflicting;

        #endregion

        public BookingsCreateViewModel()
        {
            // Initialize fields
            if (IsInDesignMode)
            {
                
            }
            else
            {
                _bookingService = new BookingService();
                //_roomService = new RoomService();
                //RoomList = RoomService.GetAllEnabled();
            }

            // Hardcoded design time data
            RoomList = new ObservableCollection<Room>()
                {
                    new Room() { Name = "Winterfell", Location = "25th floor", Capacity = 10 },
                    new Room() { Name = "King's Landing", Location = "24th floor", Capacity = 8 }
                };
            RoomAssets = "Epson projector, whiteboard";
            Room = RoomList[1];

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
        public DateTime StartTime
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
        public DateTime EndTime
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
