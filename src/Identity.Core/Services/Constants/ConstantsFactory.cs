using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Identity.Core.Services.Constants.GenericConstants;

namespace Identity.Core.Services.Constants
{
    public class ConstantsFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ConstantsFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IBaseContantsService GetConstantsService(string contextName)
        {
            if (contextName == ApplicationContextNames.Dev2.ToString())
                return (IBaseContantsService)_serviceProvider.GetService(typeof(Dev2ContantsService));

            return (IBaseContantsService)_serviceProvider.GetService(typeof(BaseConstantsService));
        }

    }
}
