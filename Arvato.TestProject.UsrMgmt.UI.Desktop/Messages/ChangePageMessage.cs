using System;
namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Messages
{
    public class ChangePageMessage
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