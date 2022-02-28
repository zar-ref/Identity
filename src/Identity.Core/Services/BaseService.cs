using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Core.Services
{
    public class BaseService
    {

        protected IUnityOfWork _unitOfWork;
        protected IUnityOfWork GetUnitOfWorkInstance()
        {
            return _unitOfWork;
        }

        protected async void CommitTransaction()
        {
 
            await _unitOfWork.Save();
        }
    }
}
