using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities
{
    public class Post:BaseEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Slug { get; set; }
        #region Relations
        public bool IsDelete { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public ICollection<PostComment> PostComments { get; set; }
        public string ImageName { get; set; }
        #endregion

    }
}
