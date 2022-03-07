using GeekShopping_ProductAPI.Data;
using GeekShopping_ProductAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace GeekShopping_ProductAPI.Controllers
{
    public class ProductController : Controller
    {
        public readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;
        }
        [Route("api/[controller]")]
        [HttpGet]
        public IActionResult GetAll()
        {
            Product[] arrProduct = _context.Products.ToArray();
            return  Ok(Json(arrProduct));
        }

       
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            return Ok(Json(id));
        }
        [Route("api/[controller]")]
        [HttpPost]
        public IActionResult Criar(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
            return Ok();
        }
    }
}
