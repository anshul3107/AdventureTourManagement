using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace AdventureTourManagement.Models.Shopping
{
    public class ShoppingCart
    {


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