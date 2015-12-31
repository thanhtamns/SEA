using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models;
namespace SEA.Heplers
{
    /// <summary>
    /// nơi khởi tạo một số dữ liệu mặc định của hệ thống
    /// </summary>
    public static class CreateDefault
    {
        private static readonly string[] VietNamChar = new string[] 
        { 
            "aAeEoOuUiIdDyY-", 
            "áàạảãâấầậẩẫăắằặẳẵ", 
            "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ", 
            "éèẹẻẽêếềệểễ", 
            "ÉÈẸẺẼÊẾỀỆỂỄ", 
            "óòọỏõôốồộổỗơớờợởỡ", 
            "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ", 
            "úùụủũưứừựửữ", 
            "ÚÙỤỦŨƯỨỪỰỬỮ", 
            "íìịỉĩ", 
            "ÍÌỊỈĨ", 
            "đ", 
            "Đ", 
            "ýỳỵỷỹ", 
            "ÝỲỴỶỸ",
            " "
        };
        /// <summary>
        /// khởi tạo title của dữ liệu bất kì.
        /// trả về chuỗi string không dấu và thay khoảng trắng bằng gạch ngang '-'.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string CreateDefaultTitle(string name){
            for (int i = 1; i < VietNamChar.Length; i++)
            {
                for (int j = 0; j < VietNamChar[i].Length; j++)
                    name = name.Replace(VietNamChar[i][j], VietNamChar[0][i - 1]);
            }
            return name;    
        }
    }
}