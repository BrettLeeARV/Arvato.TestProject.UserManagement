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
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class AssetsListViewModel : PageViewModel
    { 
        private IAssetComponent assetService;
        private ICollection<Asset> assets;

        public AssetsListViewModel()
            : base()
        {
            // set up model data
            assetService = new AssetComponent();
            FormViewModel = new AssetsFormViewModel();

            // set up commands
            AddAssetCommand = new RelayCommand(this.AddAsset, () => true);
            DeleteAssetCommand = new RelayCommand(this.DeleteAsset,
                () => FormViewModel.CurrentAsset != null && FormViewModel.CurrentAsset.ID != 0);

            MessengerInstance.Register<NotificationMessage>(this, (message) =>
            {
                if (message.Notification == "AssetSaved")
                {
                    RefreshAssets();
                }
            });
        }

        public ICollection<Asset> Assets
        {
            get
            {
                return assets;
            }
            set
            {
                if (assets != value)
                {
                    assets = value;
                    RaisePropertyChanged("Assets");
                }
            }
        }

        public AssetsFormViewModel FormViewModel
        {
            get;
            private set;
        }

        #region Command properties

        public ICommand AddAssetCommand
        {
            get;
            private set;
        }
        public ICommand DeleteAssetCommand
        {
            get;
            private set;
        }

        #endregion

        protected override void OnNavigatingTo(object s, EventArgs e)
        {
            RefreshAssets();
        }

        private void RefreshAssets()
        {
            Assets = assetService.GetList();
        }

        #region Command methods

        private void AddAsset()
        {
            FormViewModel.CurrentAsset = new Asset();
        }

        private void DeleteAsset()
        {
            var result = MessageBox.Show(
                String.Format(@"Are you sure you want to delete asset ""{0}""?", FormViewModel.CurrentAsset.Name),
                "Deleting asset",
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
                    assetService.Delete(FormViewModel.CurrentAsset);
                };
                worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                    MessengerInstance.Send(new LoadingMessage(false));
                    RefreshAssets();
                    FormViewModel.CurrentAsset = new Asset();
                };
                worker.RunWorkerAsync();
            }
            catch (Exception)
            {
                // TODO: implement messaging to move MessageBox calls to view code-behind
                MessageBox.Show("There was a problem deleting the asset.");
                return;
            }
        }

        #endregion

    }
}
