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
using System.Collections.ObjectModel;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class AssetsFormViewModel : PageViewModel, IDataErrorInfo
    {
        private IAssetComponent _assetService;
        private IRoomComponent _roomService;

        private Room _filterRoom;

        private ObservableCollection<RoomComboBoxItem> _allRoomOptions;

        private Asset _currentAsset;

        public AssetsFormViewModel()
            : base()
        {
            if (IsInDesignMode)
            {
                #region Design-time data
                Room dummyRoom1 = new Room() { Name = "Meeting Room 1", ID = 1 };
                Room dummyRoom2 = new Room() { Name = "Meeting Room 2", ID = 2 };

                // set up sample model data
                _allRoomOptions = new ObservableCollection<RoomComboBoxItem>()
                {
                    new RoomComboBoxItem()
                };

                _allRoomOptions.Add(new RoomComboBoxItem() { Room = dummyRoom1 });
                _allRoomOptions.Add(new RoomComboBoxItem() { Room = dummyRoom2 });

                #endregion
            }
            else
            {
                _assetService = new AssetComponent();
                _roomService = new RoomComponent();

                _currentAsset = new Asset();

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

                SaveAssetCommand = new RelayCommand(this.SaveAsset,
                    () => IsValid);
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
                        return "Any";
                    }
                    else
                    {
                        return Room.Name;
                    }
                }
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

        public Asset CurrentAsset
        {
            get
            {
                return _currentAsset;
            }
            set
            {
                if (value != _currentAsset)
                {
                    _currentAsset = value;
                    RaisePropertyChanged("CurrentAsset");
                }
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

        public ICommand SaveAssetCommand
        {
            get;
            private set;
        }

        private void SaveAsset()
        {
            AssetValidator validator = new AssetValidator();
            ValidationResult results = validator.Validate(_currentAsset);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }
            else
            {
                _assetService.Save(_currentAsset);
                RaisePropertyChanged("CurrentAsset");
            }
        }

        #region FluentValidation Members

        public bool IsValid
        {
            get { return SelfValidate().IsValid; }
        }

        public ValidationResult SelfValidate()
        {
            var r = ValidationHelper.Validate<AssetsFormValidator, AssetsFormViewModel>(this);
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
