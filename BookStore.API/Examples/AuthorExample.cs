using BookStore.BLL.DTO;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.API.Examples
{
    public class AuthorExample : IExamplesProvider<Author>
    {
        Author IExamplesProvider<Author>.GetExamples()
        {
            return new Author()
            {
                Name = "New Author",
                BirthDate = new DateTime(1974, 1, 13),
                BirthPlace = "A place"
            };
        }
    }
}
