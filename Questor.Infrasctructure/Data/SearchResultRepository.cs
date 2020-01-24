using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Questor.Core.Data;
using Questor.Core.Data.Entities;

namespace Questor.Infrasctructure.Data
{
    public class SearchResultRepository : QuestorRepository<SearchResult>
    {
        private readonly IAsyncRepository<SearchResultItem> _itemsRepository;
        
        public SearchResultRepository(QuestorContext context, IAsyncRepository<SearchResultItem> itemsRepository) : 
            base(context)
        {
            this._itemsRepository = itemsRepository;
        }

        public override async Task<SearchResult> FindAsync(int id)
        {
            var searchResult = this._context.SearchResults.Include(r => r.SearchResultItems).FirstOrDefaultAsync(r => r.Id == id);
            return await searchResult;
        }
    }
}