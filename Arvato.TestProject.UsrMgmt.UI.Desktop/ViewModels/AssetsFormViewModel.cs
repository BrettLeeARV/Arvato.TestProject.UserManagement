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
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using GalaSoft.MvvmLight.Messaging;

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
            _assetService = new AssetComponent();
            _roomService = new RoomComponent();

            _currentAsset = new Asset();

            var rooms = _roomService.GetList();
            _allRoomOptions = new ObservableCollection<RoomComboBoxItem>()
                {
                    new RoomComboBoxItem() { Room = null }
                };
            foreach (var room in rooms)
            {
                _allRoomOptions.Add(new RoomComboBoxItem() { Room = room });
            }
            RaisePropertyChanged("RoomOptions");

            SaveAssetCommand = new RelayCommand(this.SaveAsset,
                () => IsValid);
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
                        return "";
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

                    if (_currentAsset != null)
                    {
                        if (_currentAsset.RoomID == null || _currentAsset.RoomID == 0)
                        {
                            FilterRoom = null;
                        }
                        else
                        {
                            if (_currentAsset.RoomID != null)
                                FilterRoom = RoomOptions.First(r => r.Room != null && r.Room.ID == _currentAsset.RoomID).Room;
                        }
                    }
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
                 MessengerInstance.Send(new LoadingMessage("Saving asset..."));

                Exception exceptionResult = null;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    if (FilterRoom != null)
                        _currentAsset.RoomID = FilterRoom.ID;
                    else
                        _currentAsset.RoomID = null;

                    _assetService.Save(_currentAsset);
                };
                worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                    RaisePropertyChanged("CurrentAsset");

                    MessengerInstance.Send(new NotificationMessage("AssetSaved"));

                    MessengerInstance.Send(new LoadingMessage(false));
                    Cancel();
                };
                worker.RunWorkerAsync();
            }
        }

        private void Cancel()
        {
            MessengerInstance.Send(new ChangePageMessage(typeof(AssetsListViewModel)));
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
