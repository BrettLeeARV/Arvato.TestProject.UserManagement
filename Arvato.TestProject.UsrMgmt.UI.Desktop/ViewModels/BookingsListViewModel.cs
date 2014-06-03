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

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class BookingsListViewModel : PageViewModel
    {

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
            var users = _userService.GetList();
            _allUserOptions = new ObservableCollection<UserComboBoxItem>()
            {
                new UserComboBoxItem()
            };
            foreach (var user in users)
            {
                _allUserOptions.Add(new UserComboBoxItem() { User = user });
            }

            // set up sensible defaults for filters
            FilterStartDate = DateTime.Today;
            FilterEndDate = FilterStartDate.AddMonths(1);
            FilterUser = StateManager.CurrentUser;
            FilterCanceled = false;

            // set up model data
            _bookingService = new BookingService();
            RefreshBookings();

            // set up commands
            AddBookingCommand = new RelayCommand(this.AddBooking);
            EditBookingCommand = new RelayCommand(this.EditBooking, CanEditSelectedBooking);
            CancelBookingCommand = new RelayCommand(this.CancelBooking, CanEditSelectedBooking);

            this.PropertyChanged += new PropertyChangedEventHandler(BookingsListViewModel_PropertyChanged);
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

        #endregion

        protected override void OnNavigatingTo(object s, EventArgs e)
        {
            RefreshBookings();
        }

        private void RefreshBookings()
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

            IsLoadingBookings = true;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                results = _bookingService.GetListByFilters(FilterStartDate, FilterEndDate, userId, roomId, FilterCanceled);
                //results = _bookingService.GetList();
            };
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                Bookings = new ObservableCollection<Booking>(results);
                IsLoadingBookings = false;
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

        private bool CanEditSelectedBooking()
        {
            // TODO: add authorisation (can edit own/other's bookings)
            return SelectedBooking != null && !SelectedBooking.IsCanceled;
        }

        #endregion

    }
}
