using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

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
     [HttpGet]
  public ActionResult<>   
}