﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.Entity.Validator;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.Booking;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.Room;
using FluentValidation.Results;
using GalaSoft.MvvmLight.Command;
using System.ServiceModel;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{

    public class BookingsFormViewModel : PageViewModel, IDataErrorInfo
    {
        #region Private fields

        private bool _isNewBooking;

        private IBookingService _bookingService;
        private IRoomService _roomService;
        private IAssetComponent _assetService;

        private Booking _booking;
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
        private ObservableCollection<Booking> _bookingConflicts;

        #endregion

        public BookingsFormViewModel()
            : this(null)
        {
        }

        public BookingsFormViewModel(Booking booking)
            : base()
        {
            _booking = booking;

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
            }
            else
            {
                _bookingService = new BookingServiceClient();
                _roomService = new RoomServiceClient();
                _assetService = new AssetComponent();
                _isConflicting = false;
                RefreshRooms();
                RefreshAssets();
            }

            if (_booking == null)
            {
                // New booking
                _isNewBooking = true;
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
                getBookedItem();
            }
            else
            {
                // Edit booking
                _isNewBooking = false;

                _startDate = _booking.StartDate.Date;
                _startTime = _booking.StartDate.TimeOfDay;
                _endDate = _booking.EndDate.Date;
                _endTime = _booking.EndDate.TimeOfDay;
                // Since we do not have navigational properties on Booking and properly 
                // implemented Compare methods on Room, have to do it the ugly way
                foreach (var r in RoomList)
                {
                    if (r.ID == _booking.RoomID)
                    {
                        _room = r;
                        break;
                    }
                }

                // pre-select booked assets
                foreach (var bookedAsset in _booking.AssetBookings)
                {
                    var asset = AssetList.First(a => a.Asset.ID == bookedAsset.AssetID);
                    if (asset != null)
                    {
                        asset.IsSelected = true;
                    }
                }

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
            MakeBookingCommand = new RelayCommand(this.MakeBooking, () => IsValid && !IsConflicting);
            CancelCommand = new RelayCommand(this.Cancel);
            SelectedAssetsChangedCommand = new RelayCommand<SelectionChangedEventArgs>(this.SelectedAssetsChanged);

            // Subscribe to own PropertyChanging event, to AJAX-ly call BLL validations
            this.PropertyChanged += new PropertyChangedEventHandler(BookingsFormViewModel_PropertyChanged);
        }

        public class AssetListItem
        {
            public AssetListItem()
            {
                this.Conflicts = new ObservableCollection<AssetBooking>();
            }
            
            public Asset Asset { get; set; }
            public Room Room { get; set; }
            public bool IsSelected { get; set; }
            public bool HasConflicts
            {
                get
                {
                    if (Conflicts == null)
                    {
                        return false;
                    }
                    return Conflicts.Count > 0;
                }
            }
            public ObservableCollection<AssetBooking> Conflicts
            {
                get;
                set;
            }
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
                getBookedItem();
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
                getBookedItem();
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
                getBookedItem();
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
                getBookedItem();
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

        public Booking Booking
        {
            get
            {
                return _booking;
            }
            set
            {
                if (value == _booking)
                {
                    return;
                }
                _booking = value;
                RaisePropertyChanged("Booking");
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

        public ObservableCollection<AssetListItem> AssetList
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

        public ObservableCollection<Booking> BookingConflicts
        {
            get
            {
                return _bookingConflicts;
            }
            private set
            {
                if (value == _bookingConflicts)
                {
                    return;
                }
                _bookingConflicts = value;
                RaisePropertyChanged("BookingConflicts");
            }
        }

        private DateTime StartDateTime
        {
            get
            {
                return _startDate.Add(_startTime);
            }
        }

        private DateTime EndDateTime
        {
            get
            {
                return _endDate.Add(_endTime);
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
        public ICommand SelectedAssetsChangedCommand
        {
            get;
            private set;
        }

        #endregion

        private void RefreshRooms()
        {
            RoomList = new ObservableCollection<Room>(_roomService.GetList(true));
        }

        private void RefreshAssets()
        {
            var assets = _assetService.GetEnabledList();
            if (RoomList == null)
            {
                RefreshRooms();
            }
            AssetList = new ObservableCollection<AssetListItem>();
            foreach (var asset in assets)
            {
                AssetList.Add(new AssetListItem()
                {
                    Asset = asset,
                    Room = RoomList.First(r => r.ID == asset.RoomID),
                    IsSelected = false
                });
            }
        }

        private void getBookedItem()
        {
            if (StartDateTime < EndDateTime)
            {
                Booking detail = new Entity.Model.Booking();
                detail.ID = _booking.ID;
                detail.StartDate = StartDateTime;
                detail.EndDate = EndDateTime;
                PageViewModel.StateManager.AllBookedItem = _bookingService.getBookedItem(detail);
                RefreshRooms();
                RefreshAssets();
                RaisePropertyChanged("RoomList");
                RaisePropertyChanged("AssetList");
            }
        }

        #region Command methods

        private void MakeBooking()
        {
            // Fill in the blanks
            _booking.RoomID = _room.ID;
            _booking.StartDate = StartDate.Add(StartTime);
            _booking.EndDate = EndDate.Add(EndTime);
            _booking.UserID = StateManager.CurrentUser.ID;

            if (_booking.ID > 0)
                _booking.ModifiedBy = StateManager.CurrentUser.ID;

            // Assets
            _booking.AssetBookings = new List<AssetBooking>();
            foreach (var asset in AssetList)
            {
                if (asset.IsSelected)
                {
                    _booking.AssetBookings.Add(new AssetBooking()
                    {
                        AssetID = asset.Asset.ID,
                        Status = true
                    });
                }
            }

            MessengerInstance.Send(new LoadingMessage("Saving booking..."));

            Exception exceptionResult = null;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                try
                {
                    _booking = _bookingService.SaveBooking(_booking);
                }
                catch (Exception ex)
                {
                    exceptionResult = ex;
                }
            };
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                if (exceptionResult != null)
                {
                    if (exceptionResult is FaultException<RoomClashFault>)
                    {
                        var clashFault = ((FaultException<RoomClashFault>)exceptionResult).Detail;
                        IsConflicting = true;
                        BookingConflicts = new ObservableCollection<Booking>(clashFault.Clashes);
                        MessageBox.Show(@"That room is no longer available for the chosen time. 
Please choose a different room or time.", 
                            "Room no longer available", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else if (exceptionResult is FaultException<AssetClashFault>)
                    {
                        var clashFault = ((FaultException<AssetClashFault>)exceptionResult).Detail;
                        MessageBox.Show(@"Some of the assets are no longer available for the chosen time. Please choose a different asset or time.",
                            "Assets no longer available", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show(exceptionResult.Message, "Error creating booking", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    MessengerInstance.Send(new LoadingMessage(false));
                    return;
                }
                if (_isNewBooking)
                {
                    MessageBox.Show(
                        String.Format(@"Your booking has been made!
Your booking reference number is: {0}", _booking.RefNum), 
                            "Booking created", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Your booking has been updated.", "Booking updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                MessengerInstance.Send(new LoadingMessage(false));

                // Go back to bookings list
                Cancel();
            };
            worker.RunWorkerAsync();

        }

        private void Cancel()
        {
            MessengerInstance.Send(new ChangePageMessage(typeof(BookingsListViewModel)));
        }

        private void SelectedAssetsChanged(SelectionChangedEventArgs args)
        {
            RaisePropertyChanged("AssetList");
            // This will in turn cause BookingsFormViewModel_PropertyChanged to be called
        }

        private void BookingsFormViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Room":
                case "StartDate":
                case "StartTime":
                case "EndDate":
                case "EndTime":
                case "AssetList":
                    Debug.WriteLine("\n------ Checking availability\n");
                    CheckAvailability();
                    break;
            }
        }

        private void CheckAvailability()
        {
            // Execute only if all required fields have been filled and are validated
            if (!IsValid)
            {
                return;
            }
            var booking = new Booking()
            {
                ID = Booking.ID,
                StartDate = StartDateTime,
                EndDate = EndDateTime,
                RoomID = Room.ID,
                AssetBookings = (from asset in AssetList
                                select new AssetBooking() { AssetID = asset.Asset.ID }).ToList()
            };
            

            List<Booking> roomResults = null;
            List<Booking> assetResults = null;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                roomResults = new List<Booking>(_bookingService.CheckRoomAvailability(booking));
                assetResults = new List<Booking>(_bookingService.CheckAssetAvailability(booking));
            };
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                bool hasRoomConflicts = false;
                bool hasAssetConflicts = false;
                
                // Handle room conflicts
                BookingConflicts = new ObservableCollection<Booking>(roomResults);
                if (roomResults.Count > 0)
                {
                    hasRoomConflicts = true;
                }

                // Handle asset conflicts
                // Remove all existing conflics from every asset
                foreach (var asset in AssetList)
                {
                    asset.Conflicts.Clear();
                }
                if (assetResults.Count > 0)
                {
                    if (BookingConflicts.Count > 0)
                    {
                        BookingConflicts.Union(new ObservableCollection<Booking>(assetResults));
                    }
                    else
                    {
                        BookingConflicts = new ObservableCollection<Booking>(assetResults);
                    }

                    foreach (Booking result in assetResults)
                    {
                        foreach (var item in result.AssetBookings)
                        {
                            var assetQuery = from assetListItem in AssetList
                                             where assetListItem.Asset.ID == item.AssetID
                                             select assetListItem;
                            var asset = assetQuery.FirstOrDefault();
                            if (asset != null)
                            {
                                asset.Conflicts.Add(item);
                                if (asset.IsSelected)
                                {
                                    hasAssetConflicts = true;
                                }
                            }
                        }
                    }
                }
                IsConflicting = hasRoomConflicts || hasAssetConflicts;

                RaisePropertyChanged("BookingConflicts");
                RaisePropertyChanged("IsConflicting");
            };
            worker.RunWorkerAsync();
        }

        #endregion

        #region FluentValidation Members

        public bool IsValid
        {
            get { return SelfValidate().IsValid; }
        }

        public FluentValidation.Results.ValidationResult SelfValidate()
        {
            var r = ValidationHelper.Validate<BookingsFormValidator, BookingsFormViewModel>(this);
            foreach (var er in r.Errors)
            {
                Debug.WriteLine(er.ErrorMessage);
            }
            return r;
        }

        #endregion

        #region IDataErrorInfo Members
        public string Error
        {
            get
            {
                var r = ValidationHelper.GetError(SelfValidate());
                return r;
            }
        }

        public string this[string columnName]
        {
            get
            {
                var __ValidationResults = SelfValidate();
                if (__ValidationResults == null) return string.Empty;
                var __ColumnResults = __ValidationResults.Errors.FirstOrDefault<ValidationFailure>(x => string.Compare(x.PropertyName, columnName, true) == 0);
                return __ColumnResults != null ? __ColumnResults.ErrorMessage : string.Empty;
            }
        }
        #endregion

    }
}
