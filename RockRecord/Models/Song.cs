using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockRecord.Models
{
    [DisplayName("歌曲")]
    public class Song:ISearch
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("歌曲名稱")]
        [Required]
        public string Name { get; set; }

        [DisplayName("歌曲順序")]
        [Required]
        public int SongNumber { get; set; }

        [DisplayName("歌曲長度")]
        [Required]
        public int Length { get; set; }

        [DisplayName("唱片名稱")]
        public virtual Album Album { get; set; }

        [NotMapped]
        public SearchType SearchType
        {
            get { return SearchType.Song; }
        }
    }
}