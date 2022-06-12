using DataLayer.Entities.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer.Entities.Chats
{
    public class Chat : BaseEntity
    {
        public string ChatBody { get; set; }
        public long UserId { get; set; }
        public long x { get; set; }

        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("GroupId")]
        public ChatGroup ChatGroup { get; set; }

        #endregion
    }
}
