using System;
using System.Linq;

namespace AbstractBaseSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // we'll grab an instance of our "repository" patterned object
            var repository = new AwesomeAppRepository();

            // note that we only have access to our business methods
            // as in.. we can't do a repository.Insert<Book>(...)
            repository.InsertBook("I Am Awesome", "John Doe");
            repository.InsertBook("Building XNA 2.0 Games: A Practical Guide for Independent Game Development", "John Sedlak");

            // do some lookup work
            var queryResult = repository.LookForBooksByAuthor("John Sedlak");
            Console.WriteLine($"John wrote {queryResult.Count()} book[s].");
        }
    }
}
