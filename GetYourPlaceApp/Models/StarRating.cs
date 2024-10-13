namespace GetYourPlaceApp.Models
{
    public class StarRating : ObservableObject
    {
        public int Index { get; set; }
        private Brush _starColor;

        public Brush StarColor
        {
            get => _starColor;
            set => SetProperty(ref _starColor, value);
        }
    }
}
