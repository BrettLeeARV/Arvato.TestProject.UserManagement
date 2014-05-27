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

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class BookingsListViewModel : PageViewModel
    {

        private IBookingService bookingService;
        private ICollection<Booking> bookings;

        public BookingsListViewModel()
            : base()
        {
            // set up model data
            bookingService = new BookingService();
            RefreshBookings();

            // set up commands
            AddBookingCommand = new RelayCommand(this.AddBooking);
            //EditBookingCommand = new RelayCommand(this.EditBooking,
            //    // Enable Edit Booking button if a booking is selected
            //    () => CurrentBooking != null);
            //CancelBookingCommand = new RelayCommand(this.CancelBooking, 
            //    // enable Delete User button if a user is selected
            //    () => CurrentBooking != null);
        }

        public ICollection<Booking> Bookings
        {
            get
            {
                return bookings;
            }
            set
            {
                if (bookings != value)
                {
                    bookings = value;
                    RaisePropertyChanged("Bookings");
                }
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
            Bookings = bookingService.GetUserOwnBooking(StateManager.CurrentUser.ID);
        }

        #region Command methods

        private void AddBooking()
        {
            MessengerInstance.Send(new ChangePageMessage(typeof(BookingsCreateViewModel)));
        }

        private void EditBooking(User user)
        {
            throw new NotImplementedException();
        }

        private void CancelBooking()
        {
            //var result = MessageBox.Show(
            //    String.Format(@"Are you sure you want to delete user ""{0}""?", FormViewModel.CurrentUser.LoginID),
            //    "Deleting user",
            //    MessageBoxButton.YesNo,
            //    MessageBoxImage.Warning);

            //if (result == MessageBoxResult.No)
            //{
            //    return;
            //}

            //try
            //{
            //    //bookingService.Delete(FormViewModel.CurrentUser);
            //}
            //catch (Exception)
            //{
            //    // TODO: implement messaging to move MessageBox calls to view code-behind
            //    MessageBox.Show("There was a problem deleting the user.");
            //    return;
            //}

            ////FormViewModel.CurrentUser = null;
            //RefreshBookings();
        }

        #endregion

    }
}
