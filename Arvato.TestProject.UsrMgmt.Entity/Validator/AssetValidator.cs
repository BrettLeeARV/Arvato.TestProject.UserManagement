using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using FluentValidation.Validators;

namespace Arvato.TestProject.UsrMgmt.Entity.Validator
{
    public class AssetValidator : AbstractValidator<Asset>, IValidator<Asset>
    {
        public AssetValidator()
        {
            RuleFor(asset => asset.Name).NotEmpty();
        }
    }
}
