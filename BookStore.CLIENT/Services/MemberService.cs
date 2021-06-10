using BookStore.CLIENT.API;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.CLIENT.Services
{
    public class MemberService : Service<Member>
    {
        public MemberService() : base("http://localhost:5000/api/member") { }

        public async Task<ObservableCollection<Member>> GetAllMemberAsync()
        {
            return await GetAllAsync();
        }

        public async Task<Member> PostMemberAsync(Member member)
        {
            return await PostAsync(member);
        }

        public async Task PutMemberAsync(int id, Member member)
        {
            await PutAsync(member, id);
        }

        public async Task DeleteMemberAsync(int id)
        {
            await DeleteAsync(id);
        }
    }
}
