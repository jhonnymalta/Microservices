using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekShopping.ProductAPI.Model
{
    [Table("Produtos")]
    public class Product
    {
        [Key]
        public int Id { get; set; }


        [Column("Nome")]
        [Required]
        [StringLength(150)]
        public string? Nome { get; set; }

        [Column("Preco")]
        [Required]
        [Range(1,10000)]
        public decimal Preco { get; set; }

        [Column("Descricao")]
        [StringLength(500)]
        public string? Descricao { get; set; }

        [Column("Categoria")]
        [Required]
        [StringLength(100)]
        public string? Categoria { get; set; }

        [Column("ImagemURL")]
        [StringLength(255)]
        public string? ImageUrl { get; set; }
    }
}
