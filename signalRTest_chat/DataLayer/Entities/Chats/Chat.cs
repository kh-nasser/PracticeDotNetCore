using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Users;

namespace DataLayer.Entities.Chats
{
    public class Chat:BaseEntity
    {
        public string ChatBody { get; set; }
        public string FileAttach { get; set; }
        public long UserId { get; set; }
        public long GroupId { get; set; }


        #region Relations
        [ForeignKey("UserId")]
        public User User { get; set; }
        [ForeignKey("GroupId")]
        public ChatGroup CharGroup { get; set; }


        #endregion
    }
}