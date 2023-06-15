using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinhHaiShop.Model.Models
{
    [Table("PostTags")]
    public class PostTag
    {

        [Column(Order = 1)]
        public int PostID { set; get; }

        [Column(TypeName = "varchar", Order = 2)]
        [MaxLength(50)]
        public string TagID { set; get; }

        [ForeignKey("PostID")]
        public virtual Post Post { set; get; }

        [ForeignKey("TagID")]
        public virtual Tag Tag { set; get; }
    }
}
