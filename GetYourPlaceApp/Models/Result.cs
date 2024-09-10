namespace GetYourPlaceApp.Models
{
    public class Result<T>
    {
        public bool success { get; set; }
        public T? data { get; set; }
        public int errorCode { get; set; }
        public string message { get; set; }
    }
}
