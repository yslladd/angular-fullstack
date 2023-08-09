using Angular16API.Data;
using Angular16API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Angular16API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuperHeroController : ControllerBase
    {
        private readonly DataContext _context;

        public SuperHeroController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<SuperHero>>> GetSuperHeros()
        {
            return Ok(await _context.SuperHeros!.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<SuperHero>> GetById(int id)
        {
            var superHero = await _context.SuperHeros!.FirstOrDefaultAsync(key => key.Id == id);
            if (superHero == null)
            {
                return NotFound();
            }
            return Ok(superHero);
        }

        [HttpPost]
        public async Task<ActionResult<SuperHero>> Create(SuperHero superHero)
        {
            var createdSuperHero = _context.Add(superHero);
            await _context.SaveChangesAsync();
            var result = CreatedAtAction(nameof(GetById), new { id = superHero.Id }, createdSuperHero);

            return Ok(await _context.SuperHeros!.ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Update(int id, SuperHero updatedSuperHero)
        {
            var existingSuperHero = await GetById(id);
            if (existingSuperHero.Result == null)
            {
                return NotFound();
            }
            var hero = (OkObjectResult)existingSuperHero.Result;

            SuperHero superHero = (SuperHero)hero!.Value!;
            superHero.Name = updatedSuperHero.Name;
            superHero.FirstName = updatedSuperHero.FirstName;
            superHero.LastName = updatedSuperHero.LastName;
            superHero.Place = updatedSuperHero.Place;

            _context.Update(superHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros!.ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<SuperHero>>> Delete(int id)
        {
            var existingSuperHero = await GetById(id);
            if (existingSuperHero.Result == null)
            {
                return NotFound();
            }
            var hero = (OkObjectResult)existingSuperHero.Result;
            SuperHero superHero = (SuperHero)hero!.Value!;

            _context.Remove(superHero);
            await _context.SaveChangesAsync();

            return Ok(await _context.SuperHeros!.ToListAsync());
        }

    }
}
