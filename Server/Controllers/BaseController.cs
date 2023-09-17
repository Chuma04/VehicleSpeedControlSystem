namespace VehicleSpeedControlSystem.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BaseController<T> : ControllerBase where T : BaseEntity
{
    protected readonly DataContext _context;

    public BaseController(DataContext context) => _context = context;


    [HttpGet]
    public virtual ActionResult<List<T>> Get() => _context.Set<T>().ToList();

    [HttpGet("{Id}")]
    public virtual ActionResult<T> Get(int Id)
    {
        var t = _context.Set<T>().FirstOrDefault(x => x.Id == Id);
        return t is null ? BadRequest($"There is no  with Id = {Id}") : Ok(t);
    }

    [HttpPost]
    public virtual ActionResult<T> Post(T entity)
    {
        var t = _context.Set<T>();
        t.Add(entity);
        _context.SaveChanges();
        return Ok(entity);
    }

    [HttpPut]
    public virtual ActionResult<T> Put(T entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        _context.SaveChanges();
        return Ok(_context.Set<T>().FirstOrDefault(x => x.Id == entity.Id));
    }

    [HttpDelete("{Id}")]
    public virtual ActionResult Delete(int Id)
    {
        var t = _context.Set<T>().FirstOrDefault(x=>x.Id==Id);
        if (t is null) return BadRequest($"No Area with {Id}");
        _context.Set<T>().Remove(t);
        _context.SaveChanges();
        return Ok("Area deleted");
            
    }
        
}
