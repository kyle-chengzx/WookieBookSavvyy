using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WookieBookSavvyy.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        //creates a dictionary and adds to books by default
        private static Dictionary<string, Book> _bookDictionary = new Dictionary<string, Book>()
        {
            {"Book1Key", new Book()
            {
                title = "First Book",
                description = "Frst book ever made",
                author = "Kyle",
                imageUrl = "https://edit.org/images/cat/book-covers-big-2019101610.jpg",
                price = 100.99f
            }},
            {"Book2Key",new Book()
            {
                title = "The Giving Tree",
                description = "Tree that gives",
                author = "Shel SilverStein",
                imageUrl = "https://images-na.ssl-images-amazon.com/images/I/71wiGMKadmL.jpg",
                price = 15.99f
            }}
        };

        //grabs all books in _bookDictionary
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_bookDictionary.Values);
        }

        //gets a specific book from _bookDictionary using a string key
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetSpecificBook(string id)
        {
            if(!_bookDictionary.ContainsKey(id))
            {
                return BadRequest("not found " + id);
            }
            return Ok(_bookDictionary[id]);
        }

        //creates and adds new book into _bookDictionary
        [HttpPost]
        public IActionResult PostNewBook(Book newBook)
        {
            if(!ValidateBook(newBook))
            {
                return BadRequest("Please include title");
            }

            _bookDictionary.Add(Guid.NewGuid().ToString(), newBook);
            return Ok(_bookDictionary.Values);
        }

        //updates a specific book based on the string key
        [HttpPut]
        [Route("{id}")]
        public IActionResult Put(string id,Book updateBook)
        {
            if (!_bookDictionary.ContainsKey(id))
            {
                return BadRequest("Cannot find book with id" + id);
            }

            if (!ValidateBook(updateBook))
            {
                return BadRequest("Please include title");
            }

            Book book = _bookDictionary[id];
            book.title = updateBook.title;
            book.author = updateBook.author;
            book.description = updateBook.description;
            book.imageUrl = updateBook.imageUrl;
            book.price = updateBook.price;

            return Ok(_bookDictionary.Values);
        }

        //Deletes a specific book given the string key
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(string id)
        {
            if (!_bookDictionary.ContainsKey(id))
            {
                return BadRequest("Unable to delete missing ID" );
            }
            _bookDictionary.Remove(id);
            return Ok(_bookDictionary);
        }

        //check if book information contains title
        private bool ValidateBook(Book book)
        {
            if (book.title == null)
            {
                return false;
            }
            return true;
        }
    }
}
