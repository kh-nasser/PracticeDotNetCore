using GenericRepositoryMongoDb.CoreLayer.Dal.Repository;
using GenericRepositoryMongoDb.CoreLayer.Dal.Repository.Abstractions;
using GenericRepositoryMongoDb.DataModels.MongoDb.Documents;

namespace GenericRepositoryMongoDb.CoreLayer.Dal
{
    public class MongoDbUnitOfWork
    {
        public IMongoRepository<Person> personRepository;

        public MongoDbUnitOfWork(MongoRepository<Person> personRepository)
        {
            this.personRepository = personRepository;
        }
        /*
    [HttpPost("registerPerson")]
    public async Task AddPerson(string firstName, string lastName)
    {
        var person = new Person()
        {
            FirstName = "John",
            LastName = "Doe"
        };

        await _peopleRepository.InsertOneAsync(person);
    }

    [HttpGet("getPeopleData")]
    public IEnumerable<string> GetPeopleData()
    {
        var people = _peopleRepository.FilterBy(
            filter => filter.FirstName != "test",
            projection => projection.FirstName
        );
        return people;
    }*/
    }
}
