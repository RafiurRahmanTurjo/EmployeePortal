namespace EmployeePortalWeb.Models.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool SubscribedToNewsletter { get; set; }

    }
}
