using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;
[ApiController]
[Route("api/[controller]")]
public class InventoryController : Controller
{
    private readonly ApplicationDbContext _context;

    public InventoryController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<InventoryModal>> AllInventory()
    {
        var Inventory = _context.Inventory.ToList();
        if (Inventory == null || Inventory.Count == 0)
        {
            NotFound();
        }

        return Ok(Inventory);
    }

    [HttpGet("{id}")]
    public ActionResult<InventoryModal> OnlyOneInventory(int id)
    {
        var item = _context.Inventory.Find(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(id);

    }

    [HttpPost("{id}")]
    public ActionResult<InventoryModal> CreateInventory(InventoryModal inventory)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Inventory.Add(inventory);
        _context.SaveChanges();
        return CreatedAtAction(nameof(OnlyOneInventory), new { id = inventory.IdInventory }, inventory);
    }

    [HttpPut("{id}")]
    public ActionResult<InventoryModal> UpdateInventory(int id, InventoryModal inventory)
    {
        if (id != inventory.IdInventory)
        {
            return BadRequest();
        }
        else if (!_context.Inventory.Any(u=> u.IdInventory == id))
        {
            return NotFound();
        }

        _context.Entry(inventory).State = EntityState.Modified;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult<InventoryModal> DeleteInventory(int id)
    {
        var inventory = _context.Inventory.Find(id);
        if (inventory == null)
        {
            return NotFound();
        }

        _context.Inventory.Remove(inventory);
        _context.SaveChanges();
        return NoContent();
    }
}