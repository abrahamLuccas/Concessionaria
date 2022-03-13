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
        [HttpGet]
        public async Task<IActionResult> Delete (Guid? id)
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
        [HttpPost, ActionName("Delete")]    
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid? id)
        {
            var carro = await _appContext.Carros.FindAsync(id);
            _appContext.Carros.Remove(carro);
            await _appContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit (Guid? id)
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
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid? id, Carros carro, IList<IFormFile> Img)
        {
            if (id == null)
            {
                return NotFound();
            }
            var dadosAntigos = _appContext.Carros.AsNoTracking().FirstOrDefault(p => p.Id == id);
            IFormFile uploadedImage = Img.FirstOrDefault();
            MemoryStream ms = new MemoryStream();
            if (Img.Count > 0)
            {
                uploadedImage.OpenReadStream().CopyTo(ms);
                carro.Foto =  ms.ToArray();
            }
            else
            {
                carro.Foto = dadosAntigos.Foto;
            }
            if (ModelState.IsValid)
            {
                _appContext.Update(carro);
                await _appContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(carro);
        }
    }
}
