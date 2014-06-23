using Arvato.TestProject.UsrMgmt.Entity.Model;
using GalaSoft.MvvmLight;
using System.Collections.Generic;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop
{
    public class StateManager : ObservableObject
    {
        private static StateManager _instance = null;
        private User _currentUser;
        //private Room _currentRoom;
        private IEnumerable<Room> _allRoom;
        private IEnumerable<User> _allUser;
        private IList<string> _userRoles;
        private IEnumerable<string> _allBookedItem;

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

        public IEnumerable<Room> AllRoom
        {
            get
            {
                return _allRoom;
            }
            set
            {
                _allRoom = value;
            }
        }

        public IEnumerable<User> AllUser
        {
            get
            {
                return _allUser;
            }
            set
            {
                _allUser = value;
            }
        }

        public IList<string> UserRoles
        {
            get
            {
                return _userRoles;
            }
            set
            {
                _userRoles = value;
            }
        }

        public IEnumerable<string> AllBookedItem
        {
            get
            {
                return _allBookedItem;
            }
            set
            {
                _allBookedItem = value;
            }
        }

        //public Room CurrentRoom
        //{
        //    get
        //    {
        //        return _currentRoom;
        //    }
        //    set
        //    {
        //        _currentRoom = value;
        //        RaisePropertyChanged("CurrentRoom");
        //    }
        //}
    }
}
