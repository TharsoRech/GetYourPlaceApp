using GetYourPlaceApp.Models;

namespace GetYourPlaceApp.Services.Converters
{
    public class ObservableCollectionIsEmptyOrNullConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        { 
        
            if(value is null)
                return true;

            if(value is ObservableCollection<Property> observableCollection)
                   return observableCollection.Count == 0 ;

            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null && value is ObservableCollection<Property> observableCollection)
                return observableCollection.Count <= 0;

            return true; 
        }
    }
}
