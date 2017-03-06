using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Darren.Dyer.lab2.Models.ViewModels
{
    public class RentalOrderViewModel
    {
        public int RentalOrderId { get; set; }

        public int CustomerCustomerId { get; set; }

        public DateTime CheckoutDate { get; set; }

        public ICollection<ItemViewModel> Items { get; set; }

    }
}