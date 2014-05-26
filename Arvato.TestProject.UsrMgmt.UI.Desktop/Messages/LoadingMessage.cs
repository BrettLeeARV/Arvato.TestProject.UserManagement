using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Messages
{
    public class LoadingMessage
    {
        public LoadingMessage()
        {
            ShowLoading = false;
        }

        public LoadingMessage(string text)
        {
            ShowLoading = true;
            Text = text;
        }

        public LoadingMessage(bool show)
        {
            ShowLoading = show;
        }

        public bool ShowLoading { get; set; }
        public string Text { get; set; }
    }
}
