using ADSM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ADSM.ViewModels
{
    public class VMActivity
    {
        public IDictionary<string,List<Activities>> Activities { get; set; }
    }
}