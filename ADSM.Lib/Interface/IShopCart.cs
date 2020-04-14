using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSM.Lib.Interface
{
    public interface IShopCart
    {
        Task<dynamic> AddToCart<T>(T entity);
        Task<List<T>> FetchCart<T>(T entity);
        Task<List<T>> RemoveFromCart<T>(Task entity);
    }
}
