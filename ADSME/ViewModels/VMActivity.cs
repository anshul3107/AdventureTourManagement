using ADSM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSM.ViewModels
{
    public class VMActivity
    {
        public IDictionary<string,List<ShowActivity>> Activities { get; set; }
    }

    public class ShowActivity
    {
        public int activity_id { get; set; }
        public string activity_name { get; set; }

    }
}