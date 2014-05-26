using System;
using System.Windows.Markup;
using System.Windows.Data;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Utils
{
    public abstract class BaseConverter : MarkupExtension
    {
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
