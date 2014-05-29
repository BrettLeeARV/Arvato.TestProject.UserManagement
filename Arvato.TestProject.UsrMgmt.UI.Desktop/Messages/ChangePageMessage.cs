using System;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight;
using Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Messages
{
    /// <summary>
    /// Message to tell the recipient to change the current view model shown in the app
    /// Can either accept a System.Type of the view model, or an instance of the view model
    /// </summary>
    public class ChangePageMessage : MessageBase
    {

        public ChangePageMessage(Type viewModel)
        {
            ChangeBy = MessageType.Type;
            ViewModelType = viewModel;
        }

        public ChangePageMessage(PageViewModel viewModelInstance)
        {
            ChangeBy = MessageType.Instance;
            ViewModelInstance = viewModelInstance;
        }

        public enum MessageType { Type, Instance };

        public MessageType ChangeBy { get; set; }
        public Type ViewModelType { get; set; }
        public PageViewModel ViewModelInstance { get; set; }
    }
}