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

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class RoomsFormViewModel : PageViewModel, IDataErrorInfo
    {
        private IRoomComponent _roomService;
        private Room _currentRoom;

        public RoomsFormViewModel()
            : base()
        {
            _roomService = new RoomComponent();

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
                _roomService.Save(_currentRoom);
                RaisePropertyChanged("CurrentRoom");
            }
        }

        #region FluentValidation Members

        public bool IsValid
        {
            get { return SelfValidate().IsValid; }
        }

        public ValidationResult SelfValidate()
        {
            var r = ValidationHelper.Validate<RoomsFormValidator, RoomsFormViewModel>(this);
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
