namespace GetYourPlaceApp.Models
{
    public class PaginationItem
    {
        public string Text { get => PageIndex.ToString(); }
        public int PageIndex { get; set; }
        public bool IsActive { get; set; }
    }
}
