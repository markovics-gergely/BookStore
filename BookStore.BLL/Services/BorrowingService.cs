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
    public class BorrowingService : IBorrowingService
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;
        public BorrowingService(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteBorrowAsync(int borrowId)
        {
            _context.Borrows.Remove(new DAL.Entities.Borrowing { Id = borrowId });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Borrows.SingleOrDefault(b => b.Id == borrowId) == null)
                    throw new EntityNotFoundException("Nem található a kölcsönzés");
                else throw;
            }
        }

        public async Task<Borrowing> GetBorrowAsync(int borrowId)
        {
            return await _mapper.ProjectTo<Borrowing>(
                _context.Borrows.Where(b => b.Id == borrowId)
                ).SingleOrDefaultAsync()
                ?? throw new EntityNotFoundException("Nem található a kölcsönzés");
        }

        public async Task<IEnumerable<Borrowing>> GetBorrowsAsync()
        {
            return await
                _mapper.ProjectTo<Borrowing>(_context.Borrows)
                .ToListAsync();
        }

        public async Task<IEnumerable<Borrowing>> GetBorrowsByMemberNameOrBookTitleAsync(string memberName, string bookTitle)
        {
            if (memberName != null && bookTitle != null)
                return await _mapper.ProjectTo<Borrowing>(
                    _context.Borrows.Where(b => b.Borrower.Name.Contains(memberName) &&
                                                   b.BorrowedBook.Title.Contains(bookTitle))).ToListAsync();
            else if (memberName != null)
                return await _mapper.ProjectTo<Borrowing>(
                    _context.Borrows.Where(b => b.Borrower.Name.Contains(memberName))).ToListAsync();
            else return await _mapper.ProjectTo<Borrowing>(
                _context.Borrows.Where(b => b.BorrowedBook.Title.Contains(bookTitle))).ToListAsync();
        }

        public async Task<IEnumerable<Borrowing>> GetBorrowsActiveAsync()
        {
            return await _mapper.ProjectTo<Borrowing>(
                _context.Borrows.Where(b => b.ReturnDate == null)).ToListAsync();
        }

        public async Task<Borrowing> InsertBorrowAsync(Borrowing newBorrow)
        {
            var efBorrowing = _mapper.Map<DAL.Entities.Borrowing>(newBorrow);
            _context.Borrows.Add(efBorrowing);
            await _context.SaveChangesAsync();
            return await GetBorrowAsync(efBorrowing.Id);
        }

        public async Task UpdateBorrowAsync(int borrowId, Borrowing updatedBorrow)
        {
            var borrowing = await GetBorrowAsync(borrowId);
            var efBorrowing = _mapper.Map<DAL.Entities.Borrowing>(updatedBorrow);
            efBorrowing.Id = borrowId;
            efBorrowing.RowVersion = borrowing.RowVersion;

            var entry = _context.Attach(efBorrowing);
            entry.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Borrows
                .SingleOrDefaultAsync(b => b.Id == borrowId) == null)
                    throw new EntityNotFoundException("Nem található a kölcsönzés");
                else throw;
            }
        }

        public async Task InsertBorrowReturnDateAsync(int borrowId)
        {
            DateTime returnDate = DateTime.Today;
            var efBorrowing = await _context.Borrows.SingleOrDefaultAsync(b => b.Id == borrowId);
            
            if (efBorrowing == null)
                throw new EntityNotFoundException("Nem található a kölcsönzés ID");
            efBorrowing.ReturnDate = returnDate;
            var entry = _context.Attach(efBorrowing);
            entry.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Borrows
                .SingleOrDefaultAsync(b => b.Id == borrowId) == null)
                    throw new EntityNotFoundException("Nem található a kölcsönzés");
                else throw;
            }
        }
    }
}
