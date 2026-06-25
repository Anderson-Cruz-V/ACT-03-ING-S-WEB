using BibliotecaAPI.Data;
using BibliotecaAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace BibliotecaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly BibliotecaContext _context;

        public LibrosController(BibliotecaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetLibros()
        {
            var libros = _context.Libros.ToList();
            return Ok(libros);
        }

        [HttpGet("{id}")]
        public IActionResult GetLibro(int id)
        {
            var libro = _context.Libros.Find(id);

            if (libro == null)
            {
                return NotFound();
            }

            return Ok(libro);
        }

        [HttpPost]
        public IActionResult CrearLibro(Libro libro)
        {
            _context.Libros.Add(libro);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetLibro), new { id = libro.Id }, libro);
        }

        [HttpPut("{id}")]
        public IActionResult ActualizarLibro(int id, Libro libro)
        {
            var libroExistente = _context.Libros.Find(id);

            if (libroExistente == null)
            {
                return NotFound();
            }

            libroExistente.Titulo = libro.Titulo;
            libroExistente.Autor = libro.Autor;
            libroExistente.AnioPublicacion = libro.AnioPublicacion;
            libroExistente.Genero = libro.Genero;
            libroExistente.NumeroPaginas = libro.NumeroPaginas;
            libroExistente.Precio = libro.Precio;
            libroExistente.Disponible = libro.Disponible;

            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult EliminarLibro(int id)
        {
            var libro = _context.Libros.Find(id);

            if (libro == null)
            {
                return NotFound();
            }

            _context.Libros.Remove(libro);
            _context.SaveChanges();

            return NoContent();
        }
    }
}