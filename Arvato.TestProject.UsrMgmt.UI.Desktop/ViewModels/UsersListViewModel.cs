﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.UI.Desktop.MVVM;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class UsersListViewModel : ViewModelBase
    {

        private IUserService userService;
        private ICollection<User> users;

        public UsersListViewModel()
        {
            // set up model data
            userService = new UserService();
            users = userService.GetList();

            // set up commands
            AddUserCommand = new ActionCommand(this.AddUser, () => true);
        }

        public ICollection<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged("Users");
                }
            }
        }

        #region Command properties

        public ICommand AddUserCommand
        {
            get;
            private set;
        }
        public ICommand EditUserCommand
        {
            get;
            private set;
        }
        public ICommand DeleteUserCommand
        {
            get;
            private set;
        }
        public ICommand SignOutCommand
        {
            get;
            private set;
        }

        #endregion

        #region Command methods

        private void AddUser()
        {
            NavigationService.Navigate(new Pages.UsersFormPage());
        }

        private void EditUser()
        {
            NavigationService.Navigate(new Pages.UsersFormPage(selectedUser));
        }

        #endregion


    }
}