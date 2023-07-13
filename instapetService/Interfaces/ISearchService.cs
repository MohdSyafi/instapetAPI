using instapetService.ServiceModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace instapetService.Interfaces
{
    public interface ISearchService
    {
        Task<List<SearchResult>> SearchUser(string input, int UserId);
    }
}
