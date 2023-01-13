namespace BasicWebAPi.Models
{
    public class Country : Contact
    {
        public int CountryId { get; set; }
        public string? CountryName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
