using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ProductUpdateBatchVM
    {
        public int ProductID { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
    }
}