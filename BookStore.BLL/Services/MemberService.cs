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
    public class MemberService : IMemberService
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;
        public MemberService(NorthwindContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task DeleteMemberAsync(int memberId)
        {
            _context.Members.Remove(new DAL.Entities.Member { Id = memberId });
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (_context.Members.SingleOrDefault(m => m.Id == memberId) == null)
                    throw new EntityNotFoundException("Nem található a tag");
                else throw;
            }
        }

        public async Task<Member> GetMemberAsync(int memberId)
        {
            return await _mapper.ProjectTo<Member>(
                _context.Members.Where(m => m.Id == memberId)
                ).SingleOrDefaultAsync()
                ?? throw new EntityNotFoundException("Nem található a tag");
        }

        public async Task<IEnumerable<Member>> GetMembersAsync()
        {
            return await
                _mapper.ProjectTo<Member>(_context.Members)
                .ToListAsync();
        }

        public async Task<IEnumerable<Member>> GetMembersByNameAsync(string memberName)
        {
            return await
                _mapper.ProjectTo<Member>(_context.Members.Where(m => m.Name.Contains(memberName)))
                .ToListAsync();
        }

        public async Task<Member> InsertMemberAsync(Member newMember)
        {
            var efMember = _mapper.Map<DAL.Entities.Member>(newMember);
            _context.Members.Add(efMember);
            await _context.SaveChangesAsync();
            return await GetMemberAsync(efMember.Id);
        }

        public async Task UpdateMemberAsync(int memberId, Member updatedMember)
        {
            var member = await GetMemberAsync(memberId);
            var efMember = _mapper.Map<DAL.Entities.Member>(updatedMember);
            efMember.Id = memberId;
            efMember.RowVersion = member.RowVersion;

            var entry = _context.Attach(efMember);
            entry.State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (await _context.Members
                .SingleOrDefaultAsync(b => b.Id == memberId) == null)
                    throw new EntityNotFoundException("Nem található a tag");
                else throw;
            }
        }
    }
}
