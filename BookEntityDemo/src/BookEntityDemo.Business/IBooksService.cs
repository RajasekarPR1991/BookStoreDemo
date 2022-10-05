using BookEntityDemo.Data.Models;
using BookEntityDemo.Models.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookEntityDemo.Business
{
    public interface IBooksService
    {
        /// <summary>
        /// get list of all books
        /// </summary>
        /// <returns></returns>
        List<Books> GetAllBooks();

        /// <summary>
        /// get book details by id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Task<Books> GetBookDetailsById(string bookId);

        /// <summary>
        ///  add book record
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        Task<bool> AddBookRecord(Books bookModel);


        /// <summary>
        ///  edit book record
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        Task<BaseResponseModel> UpdateBookRecord(Books bookModel);


        /// <summary>
        /// delete book record
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        Task<BaseResponseModel> DeleteBookRecord(string bookId);
    }
}
