using System;
using GalaSoft.MvvmLight.Messaging;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Messages
{
    public class ChangePageMessage : MessageBase
    {
        public ChangePageMessage()
        {
        }

        public ChangePageMessage(Type type)
        {
            ViewModel = type;
        }

        public Type ViewModel { get; set; }
    }
}