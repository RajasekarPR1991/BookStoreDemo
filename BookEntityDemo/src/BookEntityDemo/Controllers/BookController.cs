using AutoMapper;
using BookEntityDemo.Business;
using BookEntityDemo.Data.Models;
using BookEntityDemo.Models.Request;
using BookEntityDemo.Models.Response;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookEntityDemo.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/book")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBooksService _booksService;
        private readonly ILogger<BookController> _logger;
        private readonly IMapper _mapper;

        public BookController(IBooksService booksService, ILogger<BookController> logger, IMapper mapper)
        { 
            _booksService = booksService;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// get all books
        /// </summary>       
        /// <returns></returns>
        [HttpGet]
        [Route("lists")]
        [ProducesResponseType(typeof(List<Books>), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult GetAllBooks()
        {
            try
            {
                var books = _booksService.GetAllBooks();
                if (books == null) return NotFound();
                return Ok(books);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get All Books Failed : " + ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// get book details by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     GET /details
        ///     {
        ///        "id": "73b83093-881b-4d11-8d66-97c82f4dbb3b"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("details")]
        [ProducesResponseType(typeof(Books), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetBook(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id)) throw new Exception();
                var book = await _booksService.GetBookDetailsById(id);
                if (book == null) return NotFound();
                return Ok(book);
            }
            catch (Exception ex)
            {
                _logger.LogError("Get Book by Id Failed : " + ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// add book record
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /add
        ///     {
        ///        "name" : "test",
        ///        "authorName" : "test1"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPost]
        [Route("add")]
        [ProducesResponseType(typeof(BaseResponseModel), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddBook(BookRequestModel bookrequestModel)
        {
            if (bookrequestModel == null) throw new Exception();
            try
            {
                var book = new Books
                {
                    Name = bookrequestModel.Name,
                    AuthorName = bookrequestModel.AuthorName
                };
                
                await _booksService.AddBookRecord(book);
                var model = _mapper.Map<BookResponseModel>(book);
                if(model != null)
                {
                   return Ok(new BaseResponseModel { Success = true });
                }
                return Ok(new BaseResponseModel { Success = false });
            }
            catch (Exception ex)
            {
                _logger.LogError("Add Book Record Failed : " + ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// update book record
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     PUT /update
        ///     {
        ///        "id": "73b83093-881b-4d11-8d66-97c82f4dbb3b",
        ///        "name" : "test",
        ///        "authorName" : "test1"
        ///     }
        ///
        /// </remarks>
        /// <returns></returns>
        [HttpPut]
        [Route("update")]
        [ProducesResponseType(typeof(BaseResponseModel), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBook(BookUpdateRequestModel bookrequestModel)
        {
            if (bookrequestModel == null) throw new Exception();
            try
            {
                if (string.IsNullOrWhiteSpace(bookrequestModel.Id)) throw new NullReferenceException();
                var book = new Books
                {
                    Id = bookrequestModel.Id,
                    Name = bookrequestModel.Name,
                    AuthorName = bookrequestModel.AuthorName
                };
                var model = await _booksService.UpdateBookRecord(book);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Update Book Record Failed : " + ex.Message);
                return BadRequest();
            }
        }

        /// <summary>
        /// delete book
        /// </summary>
        /// <remarks>
        /// Sample request:
        ///
        ///     DELETE /delete
        ///     {
        ///        "id": "73b83093-881b-4d11-8d66-97c82f4dbb3b"
        ///     }
        ///
        /// </remarks>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("delete")]
        [ProducesResponseType(typeof(BaseResponseModel), 200)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteBook(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id)) throw new Exception();
                var model = await _booksService.DeleteBookRecord(id);
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError("Delete Book Record Failed : " + ex.Message);
                return BadRequest();
            }
        }
    }
}