using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RockRecord.Models
{
    [DisplayName("藝術家")]
    public class Artist:ISearch
    {
        [Key]
        public int Id { get; set; }

        [DisplayName("藝術家")]
        [Required]
        public string Name { get; set; }

        public virtual ICollection<Album> Albums { get; set; }

        [NotMapped]
        public SearchType SearchType
        {
            get { return SearchType.Artist; }
        }
    }
}