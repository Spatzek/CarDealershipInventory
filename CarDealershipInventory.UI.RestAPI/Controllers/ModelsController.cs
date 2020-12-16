using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealershipInventory.Core.ApplicationServices;
using CarDealershipInventory.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarDealershipInventory.UI.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelsController : ControllerBase
    {
        private readonly IModelService _modelService;
        public ModelsController(IModelService modelService)
        {
            _modelService = modelService;
        }

        // GET: api/<ModelsController>
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Model>> Get()
        {
            try
            {
                return Ok(_modelService.GetAllModels());
            }
            catch (NullReferenceException e)
            {
                return StatusCode(404, e.Message);
            }
            
        }

        // GET api/<ModelsController>/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Model> Get(int id)
        {
            try
            {
                return Ok(_modelService.GetModelById(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (NullReferenceException e)
            {
                return StatusCode(404, e.Message);
            }

        }

        // POST api/<ModelsController>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Model> Post([FromBody] Model model)
        {
            try
            {
                return Ok(_modelService.CreateModel(model));
            }
            catch (ArgumentException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // PUT api/<ModelsController>/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public ActionResult<Model> Put(int id, [FromBody] Model model)
        {
            if (id != model.ModelId)
            {
                return StatusCode(500, "ID of path and model do not match");
            }

            try
            {
                return Ok(_modelService.EditModel(model));
            }            
            catch (ArgumentException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(500, e.Message);
            }
        }

        // DELETE api/<ModelsController>/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Model> Delete(int id)
        {
            try
            {
                return Ok(_modelService.DeleteModel(id));
            }
            catch (ArgumentException e)
            {
                return StatusCode(500, e.Message);
            }
            catch (InvalidOperationException e)
            {
                return StatusCode(500, e.Message);
            }
            
        }
    }
}
