using Identity.Infrastructure.DataAccess;
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
            //SetUnityOfWorkContext();
            return _unitOfWork;
        }

        protected async void CommitTransaction(string contextName)
        {
 
            await _unitOfWork.Save(contextName);
        }

        public string GetContextNameFromApplicationId(int id)
        {
            return _unitOfWork.ContextNamesByApplicationIdDictionary[id];
        }

     
    }
     
}
