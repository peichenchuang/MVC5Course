using System;
using System.ComponentModel.DataAnnotations;

namespace MVC5Course.Models.ValidationAttributes
{
    public class 商品名稱必須包含WillAttribute : DataTypeAttribute
    {
        public 商品名稱必須包含WillAttribute():base(DataType.Text)
        {

        }

        public override bool IsValid(object value)
        {
            var str = (string)value;
            return str.Contains("Will");
        }
    }
}