using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.Room;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;


namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class RoomsListViewModel : PageViewModel
    {
        private IRoomService roomService;
        private ICollection<Room> rooms;

        public RoomsListViewModel()
            : base()
        {
            // set up model data
            roomService = new RoomServiceClient();
            FormViewModel = new RoomsFormViewModel();

            // set up commands
            AddRoomCommand = new RelayCommand(this.AddRoom, () => true);
            DeleteRoomCommand = new RelayCommand(this.DeleteRoom,
                () => FormViewModel.CurrentRoom != null && FormViewModel.CurrentRoom.ID != 0);

            MessengerInstance.Register<NotificationMessage>(this, (message) =>
            {
                if (message.Notification == "RoomSaved")
                {
                    RefreshRooms();
                }
            });
        }

        public ICollection<Room> Rooms
        {
            get
            {
                return rooms;
            }
            set
            {
                if (rooms != value)
                {
                    rooms = value;
                    RaisePropertyChanged("Rooms");
                }
            }
        }

        public RoomsFormViewModel FormViewModel
        {
            get;
            private set;
        }

        #region Command properties

        public ICommand AddRoomCommand
        {
            get;
            private set;
        }
        public ICommand DeleteRoomCommand
        {
            get;
            private set;
        }

        #endregion

        protected override void OnNavigatingTo(object s, EventArgs e)
        {
            RefreshRooms();
        }

        private void RefreshRooms()
        {
            Rooms = roomService.GetList(false);
        }

        #region Command methods

        private void AddRoom()
        {
            FormViewModel.CurrentRoom = new Room();
        }

        private void DeleteRoom()
        {
            var result = MessageBox.Show(
                String.Format(@"Are you sure you want to delete room ""{0}""?", FormViewModel.CurrentRoom.Name),
                "Deleting room",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                MessengerInstance.Send(new LoadingMessage("Deleting room..."));

                Exception exceptionResult = null;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    roomService.Delete(FormViewModel.CurrentRoom);
                };
                worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                    MessengerInstance.Send(new LoadingMessage(false));
                    RefreshRooms();
                    FormViewModel.CurrentRoom = new Room();
                };
                worker.RunWorkerAsync();
            }
            catch (Exception)
            {
                // TODO: implement messaging to move MessageBox calls to view code-behind
                MessageBox.Show("There was a problem deleting the room.");
                return;
            }
            
        }

        #endregion

    }
}
