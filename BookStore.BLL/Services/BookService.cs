using BookStore.BLL.DTO;
using BookStore.BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.DAL;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using BookStore.BLL.Exceptions;

namespace BookStore.BLL.Services
{
    public class BookService : IBookService
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;
        public BookService(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteBookAsync(int bookId)
        {
            _context.Books.Remove(new DAL.Entities.Book { Id = bookId, ISBN = "" });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Books.SingleOrDefault(b => b.Id == bookId) == null)
                    throw new EntityNotFoundException("Nem található a könyv");
                else throw;
            }
        }

        public async Task<Book> GetBookAsync(int bookId)
        {
            return await _mapper.ProjectTo<Book>(
                _context.Books.Where(b => b.Id == bookId)
                ).SingleOrDefaultAsync()
                ?? throw new EntityNotFoundException("Nem található a könyv");
        }

        public async Task<IEnumerable<Book>> GetBooksAsync()
        {
            return await
                _mapper.ProjectTo<Book>(_context.Books)
                .ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetBooksByTitleAsync(string bookTitle)
        {
            return await
                _mapper.ProjectTo<Book>(_context.Books.Where(b => b.Title.Contains(bookTitle)))
                .ToListAsync();
        }

        public async Task<Book> InsertBookAsync(Book newBook)
        {
            var efBook = _mapper.Map<DAL.Entities.Book>(newBook);
            _context.Books.Add(efBook);
            await _context.SaveChangesAsync();
            return await GetBookAsync(efBook.Id);
        }

        public async Task UpdateBookAsync(int bookId, Book updatedBook)
        {
            var book = await GetBookAsync(bookId);
            var efBook = _mapper.Map<DAL.Entities.Book>(updatedBook);
            efBook.Id = bookId;
            efBook.RowVersion = book.RowVersion;

            var entry = _context.Attach(efBook);
            entry.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Books
                .SingleOrDefaultAsync(b => b.Id == bookId) == null)
                    throw new EntityNotFoundException("Nem található a könyv");
                else throw;
            }
        }
    }
}
