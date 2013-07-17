using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockRecord.Models
{
    public enum Role
    {
        User,
        Administrator
    }
    [DisplayName("會員資料")]
    [DisplayColumn("Name")]
    public class Member
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("會員帳號")]
        [Required(ErrorMessage = "請輸入Email地址")]
        [Description("我們值接以Email當成會員登入帳號")]
        [MaxLength(250, ErrorMessage = "Email地址長度無法超過250個字元")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DisplayName("會員密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [MaxLength(40, ErrorMessage = "密碼長度不得超過40個字元")]
        [Description("密碼將以SHA1進行雜湊運算，透過SHA1雜湊運算後的結果管為HEX表示法的字串長度接為40個字元")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("確認密碼")]
        [Required(ErrorMessage = "請輸入密碼")]
        [MaxLength(40, ErrorMessage = "密碼長度不得超過40個字元")]
        [DataType(DataType.Password)]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "請輸入一致的密碼")]
        [NotMapped]
        public string ConfirmPassword { get; set; }

        [DisplayName("姓名")]
        [Required(ErrorMessage = "請輸入性名")]
        [MaxLength(250, ErrorMessage = "姓名不得超過250個字")]
        public string Name { get; set; }

        [DisplayName("註冊時間")]
        public DateTime RegisterDate { get; set; }

    
        [DisplayName("簡介")]
        [MaxLength(500,ErrorMessage="簡介不可超過500個字")]
        [DataType(DataType.MultilineText)]
        public string Biography { get; set; }

        [DisplayName("會員資格")]
        [Required]
        public Role Role { get; set; }


        public virtual ICollection<Review> Reviews { get; set; }
        public virtual ICollection<OrderHeader> Orders { get; set; }
    }
}