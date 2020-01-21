using Questor.Core.Entities.Base;

namespace Questor.Core.Entities
{
    public class SearchEngine : BaseEntity
    {
        public string Name { get; set; }

        public string Url { get; set; }
    }
}