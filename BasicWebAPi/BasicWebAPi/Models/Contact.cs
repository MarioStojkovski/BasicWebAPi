namespace BasicWebAPi.Models
{
    public class Contact 
    {
        public int ContactId { get; set; }
        public string? ContactName { get; set; }
        public int CompanyId { get; set; }
        public int CountryId { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
