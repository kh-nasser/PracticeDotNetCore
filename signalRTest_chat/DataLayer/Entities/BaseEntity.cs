namespace DataLayer.Entities
{
    public class BaseEntity
    {
        [System.ComponentModel.DataAnnotations.Key]
        public long Id { get; set; }
        public DateTime CreateDate { get; set; }

    }
}
