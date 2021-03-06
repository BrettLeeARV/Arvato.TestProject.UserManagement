﻿using System.Windows.Controls;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using GalaSoft.MvvmLight.Messaging;
using GalaSoft.MvvmLight.Threading;
using Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels;
using System.Diagnostics;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class BookingsListPage : UserControl
    {
        public BookingsListPage()
        {
            InitializeComponent();

            Messenger.Default.Register<UpdateCalendarMessage>(this, RespondToUpdateCalendar);
        }

        private void RespondToUpdateCalendar(UpdateCalendarMessage message)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(() =>
            {
                filterCalendar.SelectedDates.Clear();
                filterCalendar.SelectedDates.AddRange(message.Start, message.End);
            });
        }
    }
}
