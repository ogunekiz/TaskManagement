using Microsoft.AspNetCore.Mvc;
using TaskManagement.Bll.Abstract;
using Task = TaskManagement.Entity.Concrete.Task;

namespace TaskManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskManagementController : ControllerBase
    {
        private readonly ITaskManagementService _taskManagementService;

        public TaskManagementController(ITaskManagementService taskManagementService)
        {
            _taskManagementService = taskManagementService;
        }


        [HttpGet("GetTask")]
        public IActionResult GetTask(int id)
        {
            Task? task = _taskManagementService.GetById(id);

            if (task is null)
            {
                return NotFound();
            }

            else
                return Ok(task);
        }


        [HttpGet("GetTaskList")]
        public IActionResult GetTaskList()
        {
            IEnumerable<Task> taskList = _taskManagementService.GetAll();

            if (!taskList.Any())
            {
                return NotFound();
            }

            else
                return Ok(taskList);
        }


        [HttpPost("AddTask")]
        public IActionResult AddTask([FromBody] Task newTask)
        {
            if (newTask == null)
                return BadRequest("Task cannot be null."); // 400 Bad Request

            bool addedStatus = _taskManagementService.Add(newTask);

            if (!addedStatus)
            {
                return StatusCode(500, "An error occurred while creating the task."); // 500 Internal Server Error
            }

            else
                return CreatedAtAction(nameof(GetTask), new { id = newTask.Id }, newTask); // 201 Created
        }


        [HttpPut("UpdateTask")]
        public IActionResult UpdateTask([FromBody] Task updateTask)
        {
            if (updateTask == null)
                return BadRequest("Task cannot be null."); // 400 Bad Request

            bool updatedStatus = _taskManagementService.Update(updateTask);

            if (!updatedStatus)
            {
                return StatusCode(500, "An error occurred while creating the task."); // 500 Internal Server Error
            }

            else
                return Ok(updatedStatus); // 200 Success
        }


        [HttpDelete("DeleteTask")]
        public IActionResult DeleteTask(int id)
        {
            bool deletedStatus = _taskManagementService.Delete(id);

            if (!deletedStatus)
            {
                return StatusCode(500, "An error occurred while creating the task."); // 500 Internal Server Error
            }

            else
                return Ok(deletedStatus); // 200 Success
        }


    }
}
