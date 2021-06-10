using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.BLL.DTO
{
    public class BookStoreProfile : Profile
    {
        public BookStoreProfile()
        {
            CreateMap<DAL.Entities.Book, Book>().ReverseMap();
            CreateMap<DAL.Entities.Author, Author>().ReverseMap();
            CreateMap<DAL.Entities.Member, Member>().ReverseMap();
            CreateMap<DAL.Entities.Borrowing, Borrowing>().ReverseMap();
        }
    }
}
