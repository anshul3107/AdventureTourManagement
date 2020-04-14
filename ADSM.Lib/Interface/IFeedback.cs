using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADSM.Lib.Interface
{
   public interface IFeedback
    {
        Task GiveFeedback<T>(T entity);

    }
}
