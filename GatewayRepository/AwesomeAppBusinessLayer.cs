using RepositoryAbstracts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GatewayRepository
{
    /// <summary>
    /// Represents a point of contact for messaging/communication layer. For example, inject this into your ASP.NET MVC Controller.
    /// We expose only the business methods and not the underlying repository.
    /// </summary>
    public sealed class AwesomeAppBusinessLayer
    {
        private readonly IRepository _repository;

        public AwesomeAppBusinessLayer(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Inserts a book based on title and author
        /// </summary>
        /// <param name="title">The title of the book</param>
        /// <param name="author">The person or persons who wrote this magnificent material</param>
        public void InsertBook(string title, string author)
        {
            // Do some work, maybe load data from an external resource like UPC
            // In this case, we're a dumb sample, so we'll just check for duplicates
            var matchTest = _repository.Query<Book>((book) => { return book.Title.Equals(title, StringComparison.OrdinalIgnoreCase); });

            if (matchTest.Any())
            {
                return;
            }

            _repository.Insert(new Book { Title = title, Author = author });
        }

        /// <summary>
        /// Looks for all books made by an author
        /// </summary>
        /// <param name="author">The author by whom to search</param>
        /// <returns>A list of books, duh</returns>
        public IEnumerable<Book> LookForBooksByAuthor(string author)
        {
            if (string.IsNullOrWhiteSpace(author))
            {
                return new Book[] { };
            }

            return _repository.Query<Book>((book) => { return book.Author.Equals(author, StringComparison.OrdinalIgnoreCase); });
        }
    }
}
