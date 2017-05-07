namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product
    {
        [DisplayName("訂單數量")]
        public int 訂單數量 {
            get
            {
                return this.OrderLine.Count;
                //計算符合條件的數量
                //return this.OrderLine.Count(p => p.Qty > 300); //效能最好
                //return this.OrderLine.Where(p => p.Qty > 300).ToList().Count; //效能差
                //return this.OrderLine.Where(p => p.Qty > 300).Count(); //效能差
            }
        }
    }
    
    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        [Required(ErrorMessage = "請輸入商品名稱")]
        //[Display(Name ="商品名稱")]
        [DisplayName("商品名稱")]
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        [MinLength(3, ErrorMessage = "商品長度不能低於3個字元")]
        public string ProductName { get; set; }
        [Required]
        [Display(Name ="價格")]
        [DisplayFormat(DataFormatString = "{0:0}", ApplyFormatInEditMode = true)]
        [Range(0, 9999, ErrorMessage = "請輸入正確的商品價格")]
        public Nullable<decimal> Price { get; set; }
        [Required]
        [Display(Name = "有效")]
        public Nullable<bool> Active { get; set; }
        [Required]
        [Display(Name ="數量")]
        [Range(0, 100, ErrorMessage = "請輸入正確數量(0~100)")]
        public Nullable<decimal> Stock { get; set; }
        [Display(Name ="建立時間")]
        public System.DateTime CreatedOn { get; set; }
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
