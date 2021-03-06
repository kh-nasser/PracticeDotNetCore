namespace GenericRepositoryMongoDb.DataModels.MongoDb.Documents
{
    /// <summary>
    /// Person document
    /// </summary>
    [BsonCollection("people")]
    public class Person : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
