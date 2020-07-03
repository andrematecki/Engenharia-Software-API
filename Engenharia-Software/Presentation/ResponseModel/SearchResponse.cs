using Engenharia_Software.Domain.Entities;

namespace Engenharia_Software.Presentation.ResponseModel
{
    public class SearchResponse
    {
        public string Title { get; set; }
        public string Snippet { get; set; }
        public string Link { get; set; }

        public SearchResponse(Search search)
        {
            if (search != null)
            {
                Title = search.Title;
                Snippet = search.Snippet;
                Link = search.Link;
            }
        }
    }
}
