using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.BLL.DTO;

namespace BookStore.BLL.Interfaces
{
    public interface IMemberService
    {
        Task<Member> GetMemberAsync(int memberId);
        Task<IEnumerable<Member>> GetMembersByNameAsync(string memberName);
        Task<IEnumerable<Member>> GetMembersAsync();
        Task<Member> InsertMemberAsync(Member newMember);
        Task UpdateMemberAsync(int memberId, Member updatedMember);
        Task DeleteMemberAsync(int memberId);
    }
}
