using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Darren.Dyer.lab2.Models.ViewModels
{
    public class ItemViewModel
    {
        public int ItemId { get; set; }

        public string Name { get; set; }

        public string Category { get; set; }

        public bool Selected { get; set; }
    }
}