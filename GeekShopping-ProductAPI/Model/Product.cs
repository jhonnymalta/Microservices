using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping_ProductAPI.Model
{
    [Table("Produto")]
    public class Product
    {
        [Key]
        public int Product_ID{ get; set; }

        [Required]
        [Column("Nome")]
        [StringLength(50,ErrorMessage ="O nome deve conter no máximo 50 caracterer")]
        public string Name { get; set; }

        [Column("Descricao")]
        [StringLength(250,ErrorMessage ="A Descrição deve conter até 250 caracter")]
        public string ShortDescription { get; set; }

        [Required]
        [Column("Preco")]
        public decimal Price { get; set; }
    }
}
