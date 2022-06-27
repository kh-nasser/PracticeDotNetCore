using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using DataLayer.Entities.Chats;

namespace DataLayer.Entities.Users
{
    public class UserGroup:BaseEntity
    {
        public long UserId { get; set; }
        public long GroupId { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("GroupId")]
        public ChatGroup ChatGroup { get; set; }

        #endregion
    }
}