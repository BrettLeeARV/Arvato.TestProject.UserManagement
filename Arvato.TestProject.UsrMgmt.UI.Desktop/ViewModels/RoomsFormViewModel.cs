using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.Entity.Validator;
using FluentValidation.Results;
using System.ComponentModel;
using System.Diagnostics;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.Room;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using GalaSoft.MvvmLight.Messaging;


namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class RoomsFormViewModel : PageViewModel, IDataErrorInfo
    {
        private IRoomService _roomService;
        private Room _currentRoom;

        public RoomsFormViewModel()
            : base()
        {
            _roomService = new RoomServiceClient();

            _currentRoom = new Room();

            SaveRoomCommand = new RelayCommand(this.SaveRoom,
                () => IsValid);

        }

        public Room CurrentRoom
        {
            get
            {
                return _currentRoom;
            }
            set
            {
                if (value != _currentRoom)
                {
                    _currentRoom = value;
                    RaisePropertyChanged("CurrentRoom");
                    RaisePropertyChanged("Name");
                    RaisePropertyChanged("Location");
                    RaisePropertyChanged("Capacity");
                }
            }
        }

        public string Name
        {
            get
            {
                if (_currentRoom == null)
                {
                    return null;
                }
                else
                {
                    return _currentRoom.Name;
                }
            }
            set
            {
                if (value != _currentRoom.Name)
                {
                    _currentRoom.Name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        public string Location
        {
            get
            {
                if (_currentRoom == null)
                {
                    return null;
                }
                else
                {
                    return _currentRoom.Location;
                }
            }
            set
            {
                if (value != _currentRoom.Location)
                {
                    _currentRoom.Location = value;
                    RaisePropertyChanged("Location");
                }
            }
        }

        public int Capacity
        {
            get
            {
                if (_currentRoom == null)
                {
                    return 0;
                }
                else
                {
                    return _currentRoom.Capacity;
                }
            }
            set
            {
                if (value != _currentRoom.Capacity)
                {
                    _currentRoom.Capacity = value;
                    RaisePropertyChanged("Capacity");
                }
            }
        }

        public ICommand SaveRoomCommand
        {
            get;
            private set;
        }

        private void SaveRoom()
        {
            RoomValidator validator = new RoomValidator();
            ValidationResult results = validator.Validate(_currentRoom);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }
            else
            {
                MessengerInstance.Send(new LoadingMessage("Saving room..."));

                Exception exceptionResult = null;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    _currentRoom = _roomService.Save(_currentRoom);
                };

                worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                    RaisePropertyChanged("CurrentRoom");

                    MessengerInstance.Send(new NotificationMessage("RoomSaved"));

                    MessengerInstance.Send(new LoadingMessage(false));
                    Cancel();
                };
                worker.RunWorkerAsync();
            }
        }

        private void Cancel()
        {
            MessengerInstance.Send(new ChangePageMessage(typeof(RoomsListViewModel)));
        }

        #region FluentValidation Members

        public bool IsValid
        {
            get { return SelfValidate().IsValid; }
        }

        public ValidationResult SelfValidate()
        {
            ValidationResult validateRoom = new ValidationResult();
            if (CurrentRoom != null)
            {
                validateRoom = ValidationHelper.Validate<RoomValidator, Room>(CurrentRoom);
            }
            var validateVM = ValidationHelper.Validate<RoomsFormValidator, RoomsFormViewModel>(this);
            return new ValidationResult(validateRoom.Errors.Concat(validateVM.Errors));
        }

        #endregion

        #region IDataErrorInfo Members
        public string Error
        {
            get { 
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
