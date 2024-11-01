using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace backend.Models
{
    public class ItensModel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Idtem { get; set; }

        [Required]
        public string ItenName { get; set; }

        [Required]
        public int QuantitativeIten { get; set; } 

        [Required]
        public float ValeuIten { get; set; } }
}