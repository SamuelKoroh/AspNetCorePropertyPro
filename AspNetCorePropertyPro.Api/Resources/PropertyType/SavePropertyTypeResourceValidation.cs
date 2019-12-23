using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCorePropertyPro.Api.Resources.PropertyType
{
    public class SavePropertyTypeResourceValidation : AbstractValidator<SavePropertyTypeResource>
    {
        public SavePropertyTypeResourceValidation()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
        }
    }
}
