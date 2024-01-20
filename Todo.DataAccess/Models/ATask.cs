using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Todo.DataAccess.Models
{
    [Table("ATask")]
    public class ATask
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
