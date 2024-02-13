namespace RealEstateListingPlatform.App.Services.Responses
{
    public class ApiResponseImageUpload
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string? UploadUrl { get; set; }
    }
}
