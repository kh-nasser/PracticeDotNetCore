﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataLayer.Entities.Users;

namespace DataLayer.Entities.Chats
{
    public class ChatGroup:BaseEntity
    {
        [MaxLength(100)]
        public string GroupTitle { get; set; }
        [MaxLength(110)]
        public string GroupToken { get; set; }
        [MaxLength(110)]
        public string ImageName { get; set; }
        public long OwnerId { get; set; }
        public long? ReceiverId { get; set; }
        public bool IsPrivate { get; set; }


        #region MyRegion
        [ForeignKey("OwnerId")]
        public User User { get; set; }
        [ForeignKey("ReceiverId")]
        public User Receiver { get; set; }
        public ICollection<Chat> Chats { get; set; }
        public ICollection<UserGroup> UserGroups { get; set; }

        #endregion
    }
}