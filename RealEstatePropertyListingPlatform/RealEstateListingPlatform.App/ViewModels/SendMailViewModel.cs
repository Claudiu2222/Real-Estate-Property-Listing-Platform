namespace RealEstateListingPlatform.App.ViewModels
{
    public class SendMailViewModel
    {
        public string To { get; set; } = string.Empty;
        public string ReplyTo { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
