using DataLayer.Entities.Chats;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Entities.Users
{
    public class UserGroup : BaseEntity
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("GroupId")]
        public ChatGroup Group { get; set; }
        #endregion
    }
}
