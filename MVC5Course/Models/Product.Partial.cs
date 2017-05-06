namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "請輸入商品名稱")]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [MinLength(3, ErrorMessage = "商品長度不能低於3個字元")]
        public string ProductName { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:0}")]
        [Range(0, 9999, ErrorMessage = "請輸入正確的商品價格")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        public Nullable<bool> Active { get; set; }
        [Required]
        [Range(0, 100, ErrorMessage = "請輸入正確數量")]
        public Nullable<decimal> Stock { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}