using backend.Models;
using backend.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public ActionResult<List<UserModel>> AllUser()
    {
        var Users = _context.Users.ToList();
        if (Users == null || Users.Count == 0)
        {
            NotFound();
        }
        
            return Ok(Users);
        
    }

    [HttpGet("{id}")]
    public ActionResult<UserModel> UniqueUser(int id)
    {
        var UserUnique = _context.Users;
        if (UserUnique == null)
        {
            NotFound();
        }

        return Ok(id);
    }

    [HttpPost]
    public ActionResult<UserModel> CreateUser(UserModel user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _context.Users.Add(user);
        _context.SaveChanges();
        return CreatedAtAction(nameof(UniqueUser), new { id = user.IdUser }, user);
    }

    [HttpPut]
    public ActionResult<UserModel> UpdateUser(int Id, UserModel user)
    {
        if (Id != user.IdUser)
        {
            return BadRequest();
        }
        else if (!_context.Users.Any(u => u.IdUser == Id))
        {
            return NotFound();
        }

        _context.Entry(user).State = EntityState.Modified;
        _context.SaveChanges();
        return NoContent();
    }

    [HttpDelete]
    public ActionResult<UserModel> DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        _context.SaveChanges();
        return NoContent();

    }
}