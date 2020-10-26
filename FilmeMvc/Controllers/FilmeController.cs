using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FilmeMvc.Models;
using System.Linq;
using System.Threading.Tasks;

namespace FilmeMvc.Controllers
{
    public class FilmeController : Controller
    {
        private readonly FilmeContext _db;

        [BindProperty]
        public Filme Filme { get; set; }

        public FilmeController(FilmeContext db)
        {
            _db = db;
        }

        // GET: Filmes
        public IActionResult Index()
        {
            var filme = _db.Filmes.ToList();
            return View(filme);
        }

        // GET: Filmes
        public IActionResult InserirAtualizar(int? id)
        {
            Filme = new Filme();
            if (id == null)
            {
                //Inserir
                return View(Filme);
            }
            //Atualizar
            Filme = _db.Filmes.FirstOrDefault(u => u.Id == id);
            if (Filme == null)
            {
                return NotFound();
            }
            return View(Filme);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult InserirAtualizar()
        {
            if (ModelState.IsValid)
            {
                if (Filme.Id == 0)
                {
                    //Inserir
                    _db.Filmes.Add(Filme);
                }
                else
                {
                    //Atualizar
                    _db.Filmes.Update(Filme);
                }
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Filme);
        }

        #region API Calls

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data =  _db.Filmes.ToList() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var filmeDb = _db.Filmes.FirstOrDefault(u => u.Id == id);
            if (filmeDb == null)
            {
                return Json(new { success = false, message = "Erro ao tentar excluir o registro." });
            }
            _db.Filmes.Remove(filmeDb);

            _db.SaveChanges();
            return Json(new { success = true, message = "Registro excluído com sucesso." });
        }
        #endregion
    }
}
