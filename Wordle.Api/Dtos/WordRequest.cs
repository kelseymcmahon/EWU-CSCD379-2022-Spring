namespace Wordle.Api.Dtos
{
    public class WordRequest
    {
        public int WordsPerPage { get; set; }
        public int PageNum { get; set; }
        public string WordFilter { get; set; }

        public WordRequest(int wordsPerPage, int pageNum, string wordFilter)
        {
            WordsPerPage = wordsPerPage;
            PageNum = pageNum;
            WordFilter = wordFilter;
        }
    }
}
