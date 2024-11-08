// Controllers/ItemController.cs
using backend.Models;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<ItensModel>> AllItems()
        {
            var items = _context.Itens.ToList();
            if (items == null || items.Count == 0)
            {
                return NotFound();
            }

            return Ok(items);
        }

        [HttpGet("{id}")]
        public ActionResult<ItensModel> OneItem(int idItem)
        {
            var item = _context.Itens.Find(idItem);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }

        [HttpPost]
        public ActionResult<ItensModel> CreateItem(ItensModel item)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Itens.Add(item);
            _context.SaveChanges();
            return CreatedAtAction(nameof(OneItem), new { id = item.Idtem }, item);
            
        }

        [HttpPut("{id}")]
        public ActionResult<ItensModel> UpdateItem(int IdItem, ItensModel item)
        {
            if (IdItem != item.Idtem)
            {
                return BadRequest();
            }else if(!_context.Itens.Any(u => u.Idtem == IdItem))
            {
                return NotFound();
            }

            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
            return NoContent();

        }

        [HttpDelete("{id}")]
        public ActionResult<ItensModel> DeleteItem(int id)
        {
            var  user = _context.Itens.Find(id);
            if (user == null)
            {
                return NotFound(id);
            }

            _context.Itens.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }
    }
