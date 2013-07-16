using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RockRecord.Models
{
    [DisplayName("客戶評價")]
    public class Review
    {
        [Key]
        public int Id { get; set; }
       

        public virtual Album Album { get; set; }

       
        public virtual Member Member { get; set; }


        [DisplayName("評論")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage="請輸入評論內容")]
        [MaxLength(500,ErrorMessage="評論長度不得超過500字")]
        public string Comment { get; set; }

        [DisplayName("評價")]
        [UIHint("Rating")]
        [Required(ErrorMessage="請選擇您的評分")]
        public int Rating{ get; set; }

        [DisplayName("留言日期")]
        public DateTime ReviewDate { get; set; }

  
    }
}