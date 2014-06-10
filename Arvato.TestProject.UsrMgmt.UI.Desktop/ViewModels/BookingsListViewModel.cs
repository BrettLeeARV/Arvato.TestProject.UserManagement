using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.Booking;
using GalaSoft.MvvmLight.Command;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class BookingsListViewModel : PageViewModel
    {
        private bool _isInitialized;

        private IBookingService _bookingService;
        private IRoomComponent _roomService;
        private IUserComponent _userService;
        private ObservableCollection<Booking> _bookings;
        private Booking _selectedBooking;

        private DateTime _filterStartDate;
        private DateTime _filterEndDate;
        private Room _filterRoom;
        private User _filterUser;
        private bool _filterCanceled;

        // To avoid unnecessarily sending UpdateCalendarMessages, remember the dates sent out in the last message
        private DateTime _calendarStartDate;
        private DateTime _calendarEndDate;

        private bool _isLoadingBookings;

        // ComboBox options
        // If these options will be reused in other parts of the app, consider putting them somewhere global and cache them
        private ObservableCollection<RoomComboBoxItem> _allRoomOptions;
        private ObservableCollection<UserComboBoxItem> _allUserOptions;

        public BookingsListViewModel()
            : base()
        {

            if (IsInDesignMode)
            {
                #region Design-time data
                User dummyUser1 = new User() { FirstName = "John", LastName = "Doe" };
                User dummyUser2 = new User() { FirstName = "Alice", LastName = "Wondergirl" };
                Room dummyRoom1 = new Room() { Name = "The Red Room" };
                Room dummyRoom2 = new Room() { Name = "International Space Station" };
                // set up sample model data
                Bookings = new ObservableCollection<Booking>()
                {
                    //new Booking() { 
                    //    ID = 1, StartDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(-2).AddHours(1), Room = dummyRoom1, User = dummyUser1, RefNum = "abc123", IsCanceled = true },
                    //new Booking() { 
                    //    ID = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(2), Room = dummyRoom2, User = dummyUser2, RefNum = "def456"},
                    //new Booking() { 
                    //    ID = 3, StartDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddDays(2).AddHours(1), Room = dummyRoom1, User = dummyUser2, RefNum="ghi789" }
                };

                SelectedBooking = Bookings[2];
                #endregion
            }
            else
            {
                _isInitialized = false;
            }
        }

        #region Internal classes

        public class RoomComboBoxItem
        {
            public Room Room { get; set; }
            public string ValueString
            {
                get
                {
                    if (Room == null)
                    {
                        return "Any room";
                    }
                    else
                    {
                        return Room.Name;
                    }
                }
            }
        }

        public class UserComboBoxItem
        {
            public User User { get; set; }
            public string ValueString
            {
                get
                {
                    if (User == null)
                    {
                        return "Anyone";
                    }
                    else
                    {
                        return String.Format("{0}, {1}", User.LastName, User.FirstName);
                    }
                }
            }
        }

        #endregion

        #region Binding properties

        public ObservableCollection<Booking> Bookings
        {
            get
            {
                return _bookings;
            }
            set
            {
                if (value == _bookings)
                {
                    return;
                }
                _bookings = value;
                RaisePropertyChanged("Bookings");
            }
        }

        public ObservableCollection<RoomComboBoxItem> RoomOptions
        {
            get
            {
                return _allRoomOptions;
            }
            private set
            {
                if (value == _allRoomOptions)
                {
                    return;
                }
                _allRoomOptions = value;
                RaisePropertyChanged("RoomOptions");
            }
        }

        public ObservableCollection<UserComboBoxItem> UserOptions
        {
            get
            {
                return _allUserOptions;
            }
            private set
            {
                if (value == _allUserOptions)
                {
                    return;
                }
                _allUserOptions = value;
                RaisePropertyChanged("UserOptions");
            }
        }

        public Booking SelectedBooking
        {
            get
            {
                return _selectedBooking;
            }
            set
            {
                if (_selectedBooking == value)
                {
                    return;
                }
                _selectedBooking = value;
                RaisePropertyChanged("SelectedBooking");
            }
        }

        public DateTime FilterStartDate
        {
            get
            {
                return _filterStartDate;
            }
            set
            {
                if (value == _filterStartDate)
                {
                    return;
                }
                _filterStartDate = value.Date;
                RaisePropertyChanged("FilterStartDate");
            }
        }

        public DateTime FilterEndDate
        {
            get
            {
                return _filterEndDate;
            }
            set
            {
                if (value == _filterEndDate || value == null) // don't allow null
                {
                    return;
                }
                _filterEndDate = value.Date;
                RaisePropertyChanged("FilterEndDate");
            }
        }

        public Room FilterRoom
        {
            get
            {
                return _filterRoom;
            }
            set
            {
                if (value == _filterRoom)
                {
                    return;
                }
                _filterRoom = value;
                RaisePropertyChanged("FilterRoom");
            }
        }

        public User FilterUser
        {
            get
            {
                return _filterUser;
            }
            set
            {
                if (value == _filterUser)
                {
                    return;
                }
                _filterUser = value;
                RaisePropertyChanged("FilterRoom");
            }
        }

        public bool FilterCanceled
        {
            get
            {
                return _filterCanceled;
            }
            set
            {
                if (value == _filterCanceled)
                {
                    return;
                }
                _filterCanceled = value;
                RaisePropertyChanged("FilterCanceled");
            }
        }

        public bool IsLoadingBookings
        {
            get
            {
                return _isLoadingBookings;
            }
            set
            {
                if (value == _isLoadingBookings)
                {
                    return;
                }
                _isLoadingBookings = value;
                RaisePropertyChanged("IsLoadingBookings");
            }
        }

        #endregion

        #region Command properties

        public ICommand AddBookingCommand
        {
            get;
            private set;
        }
        public ICommand EditBookingCommand
        {
            get;
            private set;
        }
        public ICommand CancelBookingCommand
        {
            get;
            private set;
        }
        public ICommand SelectedDatesCommand
        {
            get;
            private set;
        }
        public ICommand UpdateCalendarCommand
        {
            get;
            private set;
        }

        #endregion

        #region Methods

        protected override void OnNavigatingTo(object s, EventArgs e)
        {
            if (_isInitialized)
            {
                RefreshBookings();
            }
            else
            {
                Initialize();
            }
        }

        private void Initialize()
        {
            MessengerInstance.Send(new LoadingMessage("Please wait..."));

            // set up commands
            AddBookingCommand = new RelayCommand(this.AddBooking);
            EditBookingCommand = new RelayCommand(this.EditBooking, CanEditSelectedBooking);
            CancelBookingCommand = new RelayCommand(this.CancelBooking, CanEditSelectedBooking);
            SelectedDatesCommand = new RelayCommand<SelectionChangedEventArgs>(this.SelectedDates);
            UpdateCalendarCommand = new RelayCommand( () => UpdateCalendarControl(true) );

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {   
                // set up options for filtering
                _roomService = new RoomComponent();
                _userService = new UserComponent();
                var rooms = _roomService.GetList();
                _allRoomOptions = new ObservableCollection<RoomComboBoxItem>()
                {
                    new RoomComboBoxItem()
                };
                foreach (var room in rooms)
                {
                    _allRoomOptions.Add(new RoomComboBoxItem() { Room = room });
                }
                RaisePropertyChanged("RoomOptions");
                var users = _userService.GetList();
                _allUserOptions = new ObservableCollection<UserComboBoxItem>()
                {
                    new UserComboBoxItem()
                };
                foreach (var user in users)
                {
                    _allUserOptions.Add(new UserComboBoxItem() { User = user });
                }
                RaisePropertyChanged("UserOptions");

                // set up sensible defaults for filters
                FilterStartDate = DateTime.Today;
                FilterEndDate = FilterStartDate.AddMonths(1);
                FilterUser = StateManager.CurrentUser;
                FilterCanceled = false;

                this.PropertyChanged += new PropertyChangedEventHandler(BookingsListViewModel_PropertyChanged);
            }; 
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                _isInitialized = true;
                MessengerInstance.Send(new LoadingMessage(false));

                // Finally, get the bookings needed (will start it's own BackgroundWorker)
                _bookingService = new BookingServiceClient();
                RefreshBookings(true);
            };
            worker.RunWorkerAsync();
            
        }

        /// <summary>
        /// Sends a message to the view to update the Calendar control, only if necessary.
        /// </summary>
        /// <param name="forced">Set to true if an update should be performed regardless if it's necessary.</param>
        private void UpdateCalendarControl(bool forced = false)
        {
            var shouldUpdate = forced ||
                FilterStartDate != _calendarStartDate ||
                FilterEndDate != _calendarEndDate;
            if (shouldUpdate)
            {
                _calendarStartDate = FilterStartDate;
                _calendarEndDate = FilterEndDate;
                MessengerInstance.Send(new UpdateCalendarMessage(FilterStartDate, FilterEndDate));
            }
        }

        private void RefreshBookings(bool showModal = false)
        {
            var userId = 0;
            var roomId = 0;
            List<Booking> results = null;

            if (FilterUser != null)
            {
                userId = FilterUser.ID;
            }
            if (FilterRoom != null)
            {
                roomId = FilterRoom.ID;
            }

            if (showModal)
            {
                MessengerInstance.Send(new LoadingMessage("Getting bookings..."));
            }
            IsLoadingBookings = true;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                results = new List<Booking>(_bookingService.GetBookings(FilterStartDate, FilterEndDate, userId, roomId, FilterCanceled));
            };
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                Bookings = new ObservableCollection<Booking>(results);
                IsLoadingBookings = false;
                if (showModal)
                {
                    MessengerInstance.Send(new LoadingMessage(false));
                }
            };
            worker.RunWorkerAsync();
        }

        private void BookingsListViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "FilterStartDate":
                case "FilterEndDate":
                    UpdateCalendarControl();
                    goto case "FilterRoom";
                case "FilterRoom":
                case "FilterUser":
                case "FilterCanceled":
                    Debug.WriteLine("\n------ Refreshing bookings\n");
                    RefreshBookings();
                    break;
            }
        }

        #endregion

        #region Command methods

        private void AddBooking()
        {
            SelectedBooking = null;
            EditBooking();
        }

        private void EditBooking()
        {
            BookingsFormViewModel formViewModel = null;

            MessengerInstance.Send(new LoadingMessage(true));

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                formViewModel = new BookingsFormViewModel(SelectedBooking);
            };
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                MessengerInstance.Send(new LoadingMessage(false));
                MessengerInstance.Send(new ChangePageMessage(formViewModel));
            };
            worker.RunWorkerAsync();
        }

        private void CancelBooking()
        {
            var result = MessageBox.Show(
                String.Format(@"Are you sure you want to cancel booking ""{0}""?", SelectedBooking.RefNum),
                "Cancelling booking",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                _bookingService.CancelBooking(SelectedBooking);
            }
            catch (Exception)
            {
                // TODO: implement messaging to move MessageBox calls to view code-behind
                MessageBox.Show("There was a problem cancelling the booking.");
                return;
            }

            SelectedBooking = null;
            RefreshBookings();
        }

        private void SelectedDates(SelectionChangedEventArgs ea)
        {
            if (ea.AddedItems.Count > 0)
            {
                var firstSelection = (DateTime)ea.AddedItems[0];
                var lastSelection = (DateTime)ea.AddedItems[ea.AddedItems.Count - 1];
                DateTime startDate = firstSelection;
                DateTime endDate = lastSelection;
                // If the user selected forwards, firstSelection will be the start date
                // If the user selected backwards, firstSelection will be the end date
                if (firstSelection > lastSelection)
                {
                    startDate = lastSelection;
                    endDate = lastSelection;
                }
                if (startDate != _calendarStartDate)
                {
                    _calendarStartDate = startDate;
                    FilterStartDate = startDate;
                }
                if (endDate != _calendarEndDate)
                {
                    _calendarEndDate = endDate;
                    FilterEndDate = endDate;
                }
            }
        }

        private bool CanEditSelectedBooking()
        {
            // TODO: add authorisation (can edit own/other's bookings)
            return SelectedBooking != null && !SelectedBooking.IsCanceled;
        }

        #endregion

    }
}
