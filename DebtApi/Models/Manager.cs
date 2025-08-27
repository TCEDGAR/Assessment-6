using System.ComponentModel.DataAnnotations;

namespace DeptApi.Models
{
    public class Manager
    {
        [Key]
        public int ManagerId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<Dept>? Departments { get; set; }

    }
}
