using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.Interface
{
    public interface IConnect
    {
        Task<string> GetConnectionAsync();
    }
}
