namespace DataLayer.Entities
{
    public class BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        public int Id { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
