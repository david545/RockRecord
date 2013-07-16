using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace RockRecord.Models
{


    public class OrderHeader
    {
        [Key]
        [DisplayName("訂單編號")]
        public int Id { get; set; }

        [Required]
        [DisplayName("訂購時間")]
        public DateTime OrderDate { get; set; }

        [Required(ErrorMessage = "請輸入收件人姓名")]
        [DisplayName("收件人姓名")]
        [MaxLength(40, ErrorMessage = "收件人姓名不得超過40個字元")]
        public string ContactName { get; set; }

        [Required(ErrorMessage="請輸入郵遞區號")]
        [DisplayName("郵遞區號")]
        [RegularExpression("^\\d{3}$",ErrorMessage="請輸入長度3碼的數字")]
        public int Zipcode { get; set; }

        [DisplayName("城市")]
        [Required(ErrorMessage="請輸入城市")]
        public string City { get; set; }

        [DisplayName("鄉/鎮/區")]
        [Required(ErrorMessage = "請輸入鄉/鎮/區")]
        public string State { get; set; }

        [Required(ErrorMessage = "請輸入收件人地址")]
        [DisplayName("收件地址")]
        public string ContactAddress { get; set; }

        [NotMapped]
        [DisplayName("收件地址")]
        public string Address 
        {
            get
            {
                return Zipcode + City + State + ContactAddress;
            }
        }

        [Required(ErrorMessage = "請輸入電話號碼")]
        [DisplayName("電話號碼")]
        [MaxLength(25, ErrorMessage = "電話號碼長度不得少於25字元")]
        [DataType(DataType.PhoneNumber)]
        public string ContactPhone { get; set; }

        private string _memo = "";
        [DisplayName("備註")]
        [DataType(DataType.MultilineText)]
        [MaxLength(250,ErrorMessage="備註不得超過250個字元")]
        public string Memo {
            get 
            { 
                if(String.IsNullOrEmpty(_memo)) return "無" ;
                return _memo;
            }
            set
            {
                _memo = value;
            }
        }

        [NotMapped]
        [DisplayName("商品數量")]
        public int TotalAmount
        {
            get
            {
                if (OrderDetails != null)
                    return OrderDetails.Sum(od => od.Amount);
                else
                    return 0;
            }
        }

        [Required]
        [DisplayName("訂單金額")]
        [DataType(DataType.Currency)]
        public int TotalPrice { get; set; }


        public virtual Member Member { get; set; }
        [DisplayName("處理狀況")]
        public virtual OrderStatus OrderStatus { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}