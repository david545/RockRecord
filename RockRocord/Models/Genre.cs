using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RockRecord.Models
{
    [DisplayName("唱片類別")]
    [DisplayColumn("Name")]
    public class Genre
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("唱片類別名稱")]
        [Required(ErrorMessage = "請輸入唱片類別名稱")]
        [MaxLength(50)]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

    }
}