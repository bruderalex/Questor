using Questor.Core.Data.Entities.Base;

namespace Questor.Core.Data.Entities
{
    public class SearchEngine : BaseEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}