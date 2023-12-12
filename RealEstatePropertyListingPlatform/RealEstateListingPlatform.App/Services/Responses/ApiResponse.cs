namespace RealEstateListingPlatform.App.Services.Responses
{
    public class ApiResponse<T>
    {
        public string Message { get; set; } = string.Empty;
        public string? ValidationErrors { get; set; }
        public bool IsSuccess { get; set; }

        public List<T> Data { get; set; }
    }
}
