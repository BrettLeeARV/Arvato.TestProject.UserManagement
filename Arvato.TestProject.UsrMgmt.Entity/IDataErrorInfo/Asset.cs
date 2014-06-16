using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arvato.TestProject.UsrMgmt.Entity.Validator;
using FluentValidation;
using FluentValidation.Results;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    public partial class Asset : IDataErrorInfo
    {
        #region FluentValidation Assets

        public virtual bool IsValid
        {
            get { return SelfValidate().IsValid; }
        }

        public virtual ValidationResult SelfValidate()
        {
            return ValidationHelper.Validate<AssetValidator, Asset>(this);
        }

        #endregion

        #region IDataErrorInfo Assets
        public virtual string Error
        {
            get { return ValidationHelper.GetError(SelfValidate()); }
        }

        public virtual string this[string columnName]
        {
            get
            {
                var __ValidationResults = SelfValidate();
                if (__ValidationResults == null) return string.Empty;
                var __ColumnResults = __ValidationResults.Errors.FirstOrDefault<ValidationFailure>(x => string.Compare(x.PropertyName, columnName, true) == 0);
                return __ColumnResults != null ? __ColumnResults.ErrorMessage : string.Empty;
            }
        }
        #endregion
    }
}
