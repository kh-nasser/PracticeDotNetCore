// See https://aka.ms/new-console-template for more information
using mongoDbTest.Utils;
using System.Drawing;
using System.Security.Cryptography;

Console.WriteLine("Hello, World!");

var service = new MongoDbTestService();

var random = 0;
using (var rg = RandomNumberGenerator.Create())
{
    byte[] rno = new byte[5];
    rg.GetBytes(rno);
    random = BitConverter.ToInt32(rno, 0);
}
var age = new Random().Next(0, 100);
// CONVERT JPG TO A BYTE ARRAY
byte[] binaryContent = File.ReadAllBytes("d:\\image.jpg");

await service.InsertEntity(new mongoDbTest.Entity.User()
{
    Age = age,
    Name = $"test-{random}-{Guid.NewGuid()}",
    ContentImage = binaryContent
});
//await service.InsertEntity();
//await service.UpdateUserCollection("62b23019352a1001e24c3af6", "Jack", age);
await service.ReadWithLinq();
//await service.ReadUserCollection(age);

Console.ReadKey();