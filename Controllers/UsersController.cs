using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserContext _context;

    public UsersController(UserContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers() => Ok(await _context.Users.ToListAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser(User user)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, User user)
    {
        if (id != user.Id || !ModelState.IsValid) return BadRequest();
        _context.Entry(user).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return NotFound();
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}