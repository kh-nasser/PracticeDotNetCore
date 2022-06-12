using DataLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Chats
{
    public class ChatGroup : BaseEntity
    {
        [MaxLength(50)]
        public string GroupTitle { get; set; }
        [MaxLength(110)]
        public string GroupToken { get; set; }

        public string OwnerId { get; set; }

        #region Relations
        [ForeignKey("OwnerId")]
        public User User { get; set; }
        public ICollection<Chat> Chats{ get; set; }
        #endregion
    }
}
