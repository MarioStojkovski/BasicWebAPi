namespace BasicWebAPi.Models
{
    public class Company : Contact
    {
        public int CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
