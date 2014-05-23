using Arvato.TestProject.UsrMgmt.Entity.Model;
using GalaSoft.MvvmLight;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop
{
    public class StateManager : ObservableObject
    {
        private static StateManager _instance = null;
        private User _currentUser;

        private StateManager()
        {
        }

        public static StateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StateManager();
                }
                return _instance;
            }
        }

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                _currentUser = value;
                RaisePropertyChanged("CurrentUser");
            }
        }
    }
}
