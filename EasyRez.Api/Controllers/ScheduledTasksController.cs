using EasyRez.Domain.Jobs;
using EasyRez.Infrastructure.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace EasyRez.Api.Controllers
{
    // Hızlı bir DTO (Data Transfer Object)
    public record CreateTaskRequest(
        string Url,
        string HttpMethod,
        string? Payload,
        JobIntervalType IntervalType,
        int IntervalValue
    );
    
    [Route("api/[controller]")]
    public class ScheduledTasksController : ApiController // Sizin temel controller'ınız
    {
        private readonly EasyRezDbContext _db;

        // DİKKAT: Normalde buraya Repository enjekte etmelisiniz.
        // Bu sadece hızlı bir örnektir.
        public ScheduledTasksController(EasyRezDbContext db)
        {
            _db = db;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskRequest request)
        {
            // Gerçekte burada 'ICurrentUserService' gibi bir servisten
            // mevcut kullanıcının kimliğini (UserId) almalısınız.
            var currentUserId = "ornek-kullanici-123"; 

            var task = ScheduledTask.Create(
                currentUserId,
                request.HttpMethod,
                request.Url,
                request.Payload,
                request.IntervalType,
                request.IntervalValue
            );

            // Bunu Repository veya MediatR Command ile yapın
            await _db.Set<ScheduledTask>().AddAsync(task);
            await _db.SaveChangesAsync();

            return Ok(task);
        }
    }
}