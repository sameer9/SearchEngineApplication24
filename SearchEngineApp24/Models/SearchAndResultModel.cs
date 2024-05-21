namespace SearchEngineApp24.Models
{
    public class SearchAndResultModel
    {

        public IEnumerable<PizzaStore> SearchStores { get; set; }
        public SearchKey SearchKeys { get; set; }
    }
}
