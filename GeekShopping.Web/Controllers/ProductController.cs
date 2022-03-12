using GeekShopping.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace GeekShopping.Web.Controllers
{
    internal interface IProduct
    {
        Task<IEnumerable<Product>> GetAll(string url);
        Task<Product> GetById(string url,int id);
        Task<bool> Criar(string url,Product product);
        Task<bool> Editar(string url,Product product,int id);
        Task<Product> EditarView(string url,int id);
        Task<bool> Deletar(string url,int id);
    }
    public class ProductController : Controller, IProduct
    {
        //Injeção de dependencia  para IHttpClienteFactory
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

       

        public  async Task<bool> Criar(string url, Product product)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);

            if (product != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json"
                    );
            }
            else
            {
                return false;
            }

            var cliente = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await cliente.SendAsync(request);

            //validar se atualizou e retorna bolean
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> Deletar(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, url);

           
            var cliente = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await cliente.SendAsync(request);

            //validar se atualizou e retorna bolean
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// EDITAR       
       

        public async Task<bool> Editar(string url, Product product, int id)
        {
           var request = new HttpRequestMessage(HttpMethod.Put, url);

            if(product != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json"
                    );
            }
            else
            {
                return false;
            }

            var cliente = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await cliente.SendAsync(request);

            //validar se atualizou e retorna bolean
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public async Task<Product> EditarView(string url, int id)
        {
           

            var request = new HttpRequestMessage(HttpMethod.Get, url);


            var cliente = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await cliente.SendAsync(request);

            //validar se atualizou e retorna bolean
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Product>(jsonString);
                return result;
            }
            else
            {
                return null;
            }


        }

        public async Task<IEnumerable<Product>> GetAll(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);


            var cliente = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await cliente.SendAsync(request);

            //validar se atualizou e retorna bolean
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<IEnumerable<Product>>(jsonString);
                return result;
            }
            else
            {
                return null;
            }
        }

        public async Task<Product> GetById(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);


            var cliente = _httpClientFactory.CreateClient();
            HttpResponseMessage response = await cliente.SendAsync(request);

            //validar se atualizou e retorna bolean
            if (response.IsSuccessStatusCode)
            {
               var jsonString = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Product>(jsonString);
            }
            else
            {
                return null;
            }
           
        }

        // Controllers
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var url = "http://localhost:5107/api/product";
            var result = await GetAll(url);
            return View(result);           
            
        }
        [HttpGet]
        public IActionResult Criar()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar(Product produto)
        {
            if (ModelState.IsValid)
            { 
                var url = "http://localhost:5107/api/product";
                await Criar(url, produto);
                return RedirectToAction("Index");
            }
            return View(produto);
        }

        [HttpGet]
        public async Task<IActionResult> EditarViewGet(int? id)
        {
           
            if(id == null)
            {
                return View();
            }

            var url = $"http://localhost:5107/api/product/{id}";
            var produto = await GetById(url, (int)id);
           
            return View(produto);

        }
        [HttpPost]
        public async Task<IActionResult> Editar(int id,Product produto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var url = $"http://localhost:5107/api/product";
            id = produto.Product_ID;
            await Editar(url, produto,id);
            return RedirectToAction("Index");
        }
    }
}

   

