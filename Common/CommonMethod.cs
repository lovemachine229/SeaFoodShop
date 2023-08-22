using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public static class ExtendMethod
    {
        public static string MD5EnCryptor(string text)
        {
            if (String.IsNullOrEmpty(text))
                return text;

            MD5 md5 = new MD5CryptoServiceProvider();

            //compute hash from the bytes of text
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text + Shared.MD5_KEY));

            //get hash result after compute it
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }

            return strBuilder.ToString();
        }

        public static DateTime StringToDateTime(string strDateTime)
        {
            return DateTime.ParseExact(strDateTime, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

        public static string DateTimeToString(this DateTime date)
        {
            return date.Day + "/" + date.Month + "/" + date.Year;
        }

        public static string LabelFormatCurrency(decimal num) 
        {
            return string.Format("{0:0,0} ₫", num);
        }

        public static List<OrderStatus> GetOrderStatus()
        {
            List<OrderStatus> list = new List<OrderStatus>()
            {
                new OrderStatus { Id = 1, Name = "Chờ xử lý" },
                new OrderStatus { Id = 2, Name = "Đang xử lý" },
                new OrderStatus { Id = 3, Name = "Đang giao hàng" },
                new OrderStatus { Id = 4, Name = "Hoàn thành" },
                new OrderStatus { Id = 5, Name = "Hủy" },
            };
            return list;
        }

        public static List<OrderStatus> GetOrderPayment()
        {
            List<OrderStatus> list = new List<OrderStatus>()
            {
                new OrderStatus { Id = 0, Name = "Chưa thanh toán" },
                new OrderStatus { Id = 1, Name = "Đã thanh toán" },
            };
            return list;
        }
    }
    public enum OrderStatusEnum
    {
        Pending = 1,
        Process = 2,
        Delivery = 3,
        Done = 4,
        Cancel = 5
    }

    public class OrderStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
