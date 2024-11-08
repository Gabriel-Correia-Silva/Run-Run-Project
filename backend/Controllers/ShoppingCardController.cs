using backend.Data;
using backend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShoppingCardController : Controller
{
    private readonly ApplicationDbContext _context;
    
    public ShoppingCardController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public ActionResult<List<ShoppingCardModel>> AllShoppingCards()
    {
        var shoppingCards = _context.ShoppingCard.ToList();
        if (shoppingCards == null || shoppingCards.Count == 0)
        {
            return NotFound();
        }
        
        return Ok(shoppingCards);
    }
    
    [HttpGet("{id}")]
    public ActionResult<ShoppingCardModel> OneShoppingCard(int idCard)
    {
        var shoppingCard = _context.ShoppingCard.Find(idCard);
        if (shoppingCard == null)
        {
            return NotFound();
        }
        
        return Ok(shoppingCard);
    }
    
    [HttpPost]
    public ActionResult<ShoppingCardModel> CreateShoppingCard(ShoppingCardModel shoppingCard)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        
        _context.ShoppingCard.Add(shoppingCard);
        _context.SaveChanges();
        return CreatedAtAction(nameof(OneShoppingCard), new { id = shoppingCard.IdCard }, shoppingCard);
    }
    
    [HttpPut("{id}")]
    public ActionResult<ShoppingCardModel> UpdateShoppingCard(int idCard, ShoppingCardModel shoppingCard)
    {
        if (idCard != shoppingCard.IdCard)
        {
            return BadRequest();
        }
        
        _context.Entry(shoppingCard).State = EntityState.Modified;
        _context.SaveChanges();
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    public ActionResult<ShoppingCardModel> DeleteShoppingCard(int idCard)
    {
        var shoppingCard = _context.ShoppingCard.Find(idCard);
        if (shoppingCard == null)
        {
            return NotFound();
        }
        
        _context.ShoppingCard.Remove(shoppingCard);
        _context.SaveChanges();
        return NoContent();
    }
}