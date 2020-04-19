using AdventureTourManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdventureTourManagement.ViewModels
{
    public class VMActivityRating:ActivityRatings
    {
        public string ActivityName { get; set; }
        public string UserEmail { get; set; }

    }
}
