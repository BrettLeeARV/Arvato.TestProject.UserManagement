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

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class BookingsListViewModel : PageViewModel
    {

        private IBookingService _bookingService;
        // Should there be a app-wide list of rooms/users? So we don't need to keep pinging the server
        private IRoomService _roomService;
        private IUserService _userService;
        private ICollection<Booking> _bookings;
        private Booking _selectedBooking;

        public BookingsListViewModel()
            : base()
        {
            // set up model data
            _bookingService = new BookingService();
            RefreshBookings();

            // set up options for filtering
            _roomService = new RoomService();
            _userService = new UserService();
            Rooms = _roomService.GetList();
            Users = _userService.GetList();
            FilterStartDate = DateTime.Today;
            FilterEndDate = DateTime.Today;
            FilterUser = StateManager.CurrentUser;

            // set up commands
            AddBookingCommand = new RelayCommand(this.AddBooking);
            EditBookingCommand = new RelayCommand(this.EditBooking,
                // Enable Edit Booking button if a booking is selected
                () => SelectedBooking != null);
            CancelBookingCommand = new RelayCommand(this.CancelBooking,
                // enable Delete User button if a user is selected
                () => SelectedBooking != null);
        }

        public ICollection<Booking> Bookings
        {
            get
            {
                return _bookings;
            }
            set
            {
                if (_bookings != value)
                {
                    _bookings = value;
                    RaisePropertyChanged("Bookings");
                }
            }
        }

        public ICollection<Room> Rooms
        {
            get;
            set;
        }
        
        public ICollection<User> Users
        {
            get;
            set;
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
            get;
            set;
        }

        public DateTime FilterEndDate
        {
            get;
            set;
        }

        public Room FilterRoom
        {
            get;
            set;
        }

        public User FilterUser
        {
            get;
            set;
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
            Bookings = _bookingService.GetUserOwnBooking(StateManager.CurrentUser.ID);
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

        #endregion

    }
}
