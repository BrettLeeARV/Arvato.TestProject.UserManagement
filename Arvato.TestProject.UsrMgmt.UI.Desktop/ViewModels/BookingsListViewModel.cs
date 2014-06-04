using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class BookingsListViewModel : PageViewModel
    {
        private bool _isInitialized;

        private IBookingService _bookingService;
        private IRoomService _roomService;
        private IUserService _userService;
        private ObservableCollection<Booking> _bookings;
        private Booking _selectedBooking;

        private DateTime _filterStartDate;
        private DateTime _filterEndDate;
        private Room _filterRoom;
        private User _filterUser;
        private bool _filterCanceled;

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
                User dummyUser1 = new User() { FirstName = "John", LastName = "Doe" };
                User dummyUser2 = new User() { FirstName = "Alice", LastName = "Wondergirl" };
                Room dummyRoom1 = new Room() { Name = "The Red Room" };
                Room dummyRoom2 = new Room() { Name = "International Space Station" };
                // set up sample model data
                Bookings = new ObservableCollection<Booking>()
                {
                    new Booking() { 
                        ID = 1, StartDate = DateTime.Now.AddDays(-2), EndDate = DateTime.Now.AddDays(-2).AddHours(1), Room = dummyRoom1, User = dummyUser1, RefNum = "abc123", IsCanceled = true },
                    new Booking() { 
                        ID = 2, StartDate = DateTime.Now, EndDate = DateTime.Now.AddHours(2), Room = dummyRoom2, User = dummyUser2, RefNum = "def456"},
                    new Booking() { 
                        ID = 3, StartDate = DateTime.Now.AddDays(2), EndDate = DateTime.Now.AddDays(2).AddHours(1), Room = dummyRoom1, User = dummyUser2, RefNum="ghi789" }
                };

                SelectedBooking = Bookings[2];
            }
            else
            {
                _isInitialized = false;
            }
        }

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

        #endregion

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

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {   
                // set up options for filtering
                _roomService = new RoomService();
                _userService = new UserService();
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

                // Manually send a message to the view, so we can set the Calendar's selected range accordingly
                MessengerInstance.Send(new UpdateCalendarMessage(FilterStartDate, FilterEndDate));

                this.PropertyChanged += new PropertyChangedEventHandler(BookingsListViewModel_PropertyChanged);
            }; 
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                _isInitialized = true;
                MessengerInstance.Send(new LoadingMessage(false));

                // Finally, get the bookings needed (will start it's own BackgroundWorker)
                _bookingService = new BookingService();
                RefreshBookings(true);
            };
            worker.RunWorkerAsync();
            
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
                results = _bookingService.GetListByFilters(FilterStartDate, FilterEndDate, userId, roomId, FilterCanceled);
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
                case "FilterRoom":
                case "FilterUser":
                case "FilterCanceled":
                    Debug.WriteLine("\n------ Refreshing bookings\n");
                    RefreshBookings();
                    break;
            }
        }

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
            var firstSelection = (DateTime) ea.AddedItems[0];
            var lastSelection = (DateTime) ea.AddedItems[ea.AddedItems.Count - 1];
            // If the user selected forwards, firstSelection will be the start date
            // If the user selected backwards, firstSelection will be the end date
            if (firstSelection < lastSelection)
            {
                FilterStartDate = firstSelection;
                FilterEndDate = lastSelection;
            }
            else
            {
                FilterStartDate = lastSelection;
                FilterEndDate = lastSelection;
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
