using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DeptApi.Models
{
    public class Dept
    {
        [Key]
        public int DeptId { get; set; }
        public string DeptName { get; set; } = string.Empty;
        public int NumOfEmployees { get; set; }
        public int ManagerId { get; set; }
        [ForeignKey("ManagerId")]
        public Manager? Manager { get; set; }

    }
}
