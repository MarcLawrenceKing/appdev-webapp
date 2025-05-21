using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace appdev_webapp.Models
{
    public class Expense
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] //auto increment
        public int Id { get; set; }

        public decimal Value { get; set; }

        [Required]
        public string? Description { get; set; }
    }
}
