using System;
using System.Collections.Generic;
using System.Text;
using Engenharia_Software.Domain.Entities;
using Engenharia_Software.Presentation;

namespace Engenharia_Software.Domain.Services
{
    public interface ISearchService
    {
        ICollection<Search> Search(string q, User user);
    }
}
