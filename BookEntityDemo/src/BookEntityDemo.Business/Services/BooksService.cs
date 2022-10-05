using BookEntityDemo.Data.Models;
using BookEntityDemo.Models.Response;
using BookEntityDemo.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookEntityDemo.Business.Services
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _booksRepository;
        public BooksService(IBooksRepository booksRepository)
        {
            _booksRepository = booksRepository;
        }

        /// <summary>
        ///  add book record
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        public async Task<bool> AddBookRecord(Books bookModel)
        {
            return await _booksRepository.AddBookRecord(bookModel);
        }

        /// <summary>
        /// delete book record
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<BaseResponseModel> DeleteBookRecord(string bookId)
        {
            return await _booksRepository.DeleteBookRecord(bookId);
        }

        /// <summary>
        /// get list of all books
        /// </summary>
        /// <returns></returns>
        public List<Books> GetAllBooks()
        {
            return _booksRepository.GetAllBooks();
        }

        /// <summary>
        /// get book details by id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<Books> GetBookDetailsById(string bookId)
        {
            return await _booksRepository.GetBookDetailsById(bookId);
        }

        /// <summary>
        ///  edit book record
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        public async Task<BaseResponseModel> UpdateBookRecord(Books bookModel)
        {
            return await _booksRepository.UpdateBookRecord(bookModel);
        }
    }
}
