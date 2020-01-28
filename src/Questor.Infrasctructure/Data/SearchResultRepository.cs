using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Questor.Core.Data;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Data
{
    public class SearchResultRepository : QuestorRepository<SearchResult, int>
    {
        public SearchResultRepository(QuestorContext context) : 
            base(context)
        {
        }

        public override async Task<SearchResult> FindAsync(int id)
        {
            var searchResult = 
                this._context.SearchResults.Include(searchResult => searchResult.SearchResultItems)
                    .FirstOrDefaultAsync(searchResult => searchResult.Id == id);
            
            return await searchResult;
        }
    }
}