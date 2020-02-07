using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources.PropertyType
{
    public class SavePropertyTypeResourceValidator : AbstractValidator<SavePropertyTypeResource>
    {
        public SavePropertyTypeResourceValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty().MaximumLength(100);
        }
    }
}
