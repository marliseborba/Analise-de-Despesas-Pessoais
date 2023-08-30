namespace Expenses.Models.ViewModels
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }
        public KeyWord? KeyWord { get; set; }
        public List<KeyWord>? KeyWords { get; set; }
        public List<int>? Keys { get; set; }
        public int? Key { get; set; } 
    }
}
