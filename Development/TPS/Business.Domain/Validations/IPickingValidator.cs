using Business.Domain.Picking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Domain.Validations
{
    public interface IPickingValidator<T> where T : class
    {
        void SetThisValidatorTo(T model);
    }
}
