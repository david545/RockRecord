using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;
namespace RockRecord.Models
{
    [DisplayName("唱片資訊")]
    [DisplayColumn("Name")]
    public class Album:ISearch
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage="請選擇藝術家")]
        public int ArtistId { get; set; }
        [Required(ErrorMessage="請選擇唱片類型")]
        public int GenreId { get; set; }

        [JsonIgnore]
        [DisplayName("藝術家")]
        [ForeignKey("ArtistId")]
        public virtual Artist Artist { get; set; }
        
        [DisplayName("曲風")]
        [ForeignKey("GenreId")]
        public virtual Genre Genre { get; set; }

        [DisplayName("唱片名稱")]
        [Required(ErrorMessage = "請輸入唱片名稱")]
        public string Name { get; set; }

        [DisplayName("唱片簡介")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }


        [DisplayName("發售日期")]
        [Description("如無發售日期，代表查無")]
        public DateTime? PublicDate { get; set; }


        [Required]
        [DisplayName("價格")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        [DisplayName("歌曲")]
        [JsonIgnore]
        public virtual ICollection<Song> Songs { get; set; }

        [JsonIgnore]
        public virtual ICollection<Review> Reviews { get; set; }

        [DisplayName("庫存")]
        public int Stock { get; set; }


        [DisplayName("評價")]
        [NotMapped]
        [UIHint("Rating")]
        public int Rating
        {
            get 
            {

                if (Reviews!=null && Reviews.Count > 0)
                {
  
                    
                    double avg = Reviews.Average(r => r.Rating);
                    if (avg % 1 > 0.0 && avg % 1 > 0.5)
                        return (int)Math.Ceiling(avg);
                    else
                        return (int)Math.Floor(avg);
                }
                else
                {
                    return 0;
                }
            }
        }

        [NotMapped]
        public int Popular
        {
            get
            {
                if (Reviews == null) return 0;
                return Reviews.Count;
            }
        }

  
        [NotMapped]
        public SearchType SearchType
        {
            get { return SearchType.Album; }
        }


    }
}