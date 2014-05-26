namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Messages
{
    public class ChangeViewModelMessage
    {
        public ChangeViewModelMessage()
        {
        }

        public ChangeViewModelMessage(string name)
        {
            ViewModelName = name;
        }

        public string ViewModelName { get; set; }
    }
}