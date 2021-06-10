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
    public class AuthorService : IAuthorService
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;
        public AuthorService(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteAuthorAsync(int authorId)
        {
            _context.Authors.Remove(new DAL.Entities.Author { Id = authorId });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Authors.SingleOrDefault(a => a.Id == authorId) == null)
                    throw new EntityNotFoundException("Nem található a szerző");
                else throw;
            }
        }

        public async Task<Author> GetAuthorAsync(int authorId)
        {
            return await _mapper.ProjectTo<Author>(
                _context.Authors.Where(p => p.Id == authorId)
                ).SingleOrDefaultAsync()
                ?? throw new EntityNotFoundException("Nem található a szerző");
        }

        public async Task<IEnumerable<Author>> GetAuthorsAsync()
        {
            return await
                _mapper.ProjectTo<Author>(_context.Authors)
                .ToListAsync();
        }

        public async Task<IEnumerable<Author>> GetAuthorsByNameAsync(string authorName)
        {
            return await
                _mapper.ProjectTo<Author>(_context.Authors.Where(a => a.Name.Contains(authorName)))
                .ToListAsync();
        }

        public async Task<Author> InsertAuthorAsync(Author newAuthor)
        {
            var efAuthor = _mapper.Map<DAL.Entities.Author>(newAuthor);
            //efAuthor.RowVersion = new byte[] { 1 };
            _context.Authors.Add(efAuthor);
            await _context.SaveChangesAsync();
            return await GetAuthorAsync(efAuthor.Id);
        }

        public async Task UpdateAuthorAsync(int authorId, Author updatedAuthor)
        {
            var author = await GetAuthorAsync(authorId);
            var efAuthor = _mapper.Map<DAL.Entities.Author>(updatedAuthor);
            efAuthor.Id = authorId;
            efAuthor.RowVersion = author.RowVersion;

            var entry = _context.Attach(efAuthor);
            entry.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Authors
                .SingleOrDefaultAsync(a => a.Id == authorId) == null)
                    throw new EntityNotFoundException("Nem található a szerző");
                else throw;
            }
        }
    }
}
