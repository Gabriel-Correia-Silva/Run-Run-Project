using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Models;

public class ShoppingCardModel
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    
    public int IdCard { get; set; }
    
    public int IdUser { get; set; }

    public List<List<Models.ItensModel>> ListItensSelect { get; set; }

}