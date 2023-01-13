using BasicWebAPi.Models.Dto;

namespace BasicWebAPi.Data
{
    public class ContactsData
    {
        public static List<ContactDTO> contactsList = new List<ContactDTO>
        {
            new ContactDTO{ContactId = 1, ContactName = "Mario", CompanyId = 1 , CountryId = 1} 
        };
    }
}
