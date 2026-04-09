using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TaskManagementAPI.Data;
using TastManagementAPI.Models;
using TastManagementAPI.Models;

namespace TaskManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TasksController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<TaskItem>> Get()
        {
            return Ok(_context.Tasks.ToList());
        }

        [HttpPost]
        public ActionResult<List<TaskItem>> AddTask(TaskItem task)
        {
            if (string.IsNullOrWhiteSpace(task.Title))
            {
                return BadRequest("Title is required");
            }

            _context.Tasks.Add(task);
            _context.SaveChanges();

            return Ok(_context.Tasks.ToList());
        }

        [HttpDelete("{id}")]
        public ActionResult<List<TaskItem>> Delete(int id)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound("Task not found");
            }

            _context.Tasks.Remove(task);
            _context.SaveChanges();

            return Ok(_context.Tasks.ToList());
        }

        [HttpPut("{id}")]
        public ActionResult<List<TaskItem>> Update(int id, TaskItem updatedTask)
        {
            var task = _context.Tasks.FirstOrDefault(t => t.Id == id);

            if (task == null)
            {
                return NotFound("Task not found");
            }

            if (string.IsNullOrWhiteSpace(updatedTask.Title))
            {
                return BadRequest("Title is required");
            }

            task.Title = updatedTask.Title;
            task.IsCompleted = updatedTask.IsCompleted;

            _context.SaveChanges();

            return Ok(_context.Tasks.ToList());
        }
    }
}