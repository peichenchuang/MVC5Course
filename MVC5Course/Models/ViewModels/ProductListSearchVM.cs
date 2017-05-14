using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ViewModels
{
    public class ProductListSearchVM : IValidatableObject
    {
        
        public string q { get; set; }
        public int StockMin { get; set; }
        public int StockMax { get; set; }

        IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        {
            if(StockMax < StockMin)
            {
                yield return new ValidationResult("庫存資料篩選條件錯誤", new string[] { "stockMax", "stockMin" });
            }
        }
    }
}