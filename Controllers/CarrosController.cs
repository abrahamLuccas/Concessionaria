using Concessionaria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Concessionaria.Controllers
{
    public class CarrosController : Controller
    {
        private readonly Concessionaria.Data.AppContext _appContext;
        public CarrosController(Concessionaria.Data.AppContext appContext)
        {
            _appContext = appContext;   
        }
        public async Task<IActionResult> Index()
        {
            var allCarros = await _appContext.Carros.ToListAsync();
            return View(allCarros);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            return View();  
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Carros carro, IList<IFormFile> Img)
        {
            //Verificar o tamanho da imagem 
            IFormFile uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                carro.Foto = ms.ToArray();
            }

            _appContext.Carros.Add(carro);
            _appContext.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var carro = await _appContext.Carros.FindAsync(id);
            if (carro == null)
            {
                return BadRequest();
            }
            return View(carro);
        }
    }
}
