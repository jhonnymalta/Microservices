using GeekShopping_ProductAPI.Data;
using GeekShopping_ProductAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping_ProductAPI.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        public readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        
        [HttpGet]        
        public IActionResult GetAll()
        {
            Product[] arrProduct = _context.Products.ToArray();
            return Ok(arrProduct);
        }

        
        [HttpGet("{id}")]
        public  IActionResult GetById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            var produto =  _context.Products.SingleOrDefault(p => p.Product_ID == id);
            return Ok(produto);
        }
        
        [HttpPost]
        public IActionResult Criar(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();
        }
      

        [HttpPut]
        public IActionResult Editar(int id,Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return Ok(Json("Alterado com sucesso!"));
            }
            return BadRequest();
        }

             
        [HttpDelete("{id}")]
        public IActionResult Deletar(int? id)
        {

            var product = _context.Products.FirstOrDefault(p => p.Product_ID == id);
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok();
        }
    }
}
