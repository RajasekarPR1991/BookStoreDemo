using BookEntityDemo.Data.Context;
using BookEntityDemo.Data.Models;
using BookEntityDemo.Models.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookEntityDemo.Repository.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly BookContext _context;
        public BooksRepository(BookContext context)
        {
            _context = context;
        }

        /// <summary>
        ///  add book record
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>

        public async Task<bool> AddBookRecord(Books bookModel)
        {
            try
            {
                bookModel.Id = Guid.NewGuid().ToString();
                await _context.AddAsync(bookModel);
                var createdRowCount = await _context.SaveChangesAsync();
                return createdRowCount > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// delete book record
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<BaseResponseModel> DeleteBookRecord(string bookId)
        {
            BaseResponseModel model = new BaseResponseModel();
            try
            {
                Books _bk = await GetBookDetailsById(bookId);
                if (_bk != null)
                {
                    _context.Remove(_bk);
                    await _context.SaveChangesAsync();
                    model.Success = true;
                }
                else
                {
                    model.Success = false;
                    model.Message = "Book Record Not Found";
                }
            }
            catch (Exception ex)
            {
                model.Success = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }

        /// <summary>
        /// get list of all books
        /// </summary>
        /// <returns></returns>
        public List<Books> GetAllBooks()
        {
            List<Books> bookList;
            try
            {
                bookList = _context.Set<Books>().ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bookList;
        }

        /// <summary>
        /// get book details by id
        /// </summary>
        /// <param name="bookId"></param>
        /// <returns></returns>
        public async Task<Books> GetBookDetailsById(string bookId)
        {
            Books book;
            try
            {
                book = await _context.FindAsync<Books>(bookId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return book;
        }

        /// <summary>
        ///  edit book record
        /// </summary>
        /// <param name="bookModel"></param>
        /// <returns></returns>
        public async Task<BaseResponseModel> UpdateBookRecord(Books bookModel)
        {
            BaseResponseModel model = new BaseResponseModel();
            try
            {
                Books _bk = await GetBookDetailsById(bookModel.Id);
                if (_bk != null)
                {
                    _bk.Name = bookModel.Name;
                    _bk.AuthorName = bookModel.AuthorName;
                    _context.Update(_bk);
                    await _context.SaveChangesAsync();
                    model.Success = true;
                }
                else
                {
                    model.Success = false;
                    model.Message = "Book Record Not found";
                }
            }
            catch (Exception ex)
            {
                model.Success = false;
                model.Message = "Error : " + ex.Message;
            }
            return model;
        }
    }
}
