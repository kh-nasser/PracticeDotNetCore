namespace DataLayer.Entities.Chats
{
    public class Chat:BaseEntity
    {
        public string ChatBody{ get; set; }
        public long UserId{ get; set; }
    }
}
