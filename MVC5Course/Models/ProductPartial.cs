using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    [MetadataType(typeof(ProductPartial))]
    public partial class Product
    {

    }

    public partial class ProductPartial
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "請輸入商品名稱")]
        [MinLength(3, ErrorMessage = "商品長度不能低於3個字元")]
        public string ProductName { get; set; }
        [Required]
        [Range(0, 9999, ErrorMessage = "請輸入正確的商品價格")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "請輸入正確數量")]
        public Nullable<decimal> Stock { get; set; }
    }
}