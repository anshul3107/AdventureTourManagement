using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSM.Models.Shopping
{
    public class ShoppingCart
    {
        ADSMDbContext dbcontext = new ADSMDbContext();


    }

    public class ShoppingCartFactory : IFilterFactory
    {
        public bool IsReusable => throw new NotImplementedException();

        public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
        {
            throw new NotImplementedException();
        }
    }
}