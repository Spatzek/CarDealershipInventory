using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarDealershipInventory.Core.ApplicationServices;
using CarDealershipInventory.Core.Entity;
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
        [HttpGet]
        public ActionResult<IEnumerable<Model>> Get()
        {
            return Ok(_modelService.GetAllModels());
        }

        // GET api/<ModelsController>/5
        [HttpGet("{id}")]
        public ActionResult<Model> Get(int id)
        {
            return Ok(_modelService.GetModelById(id));
        }

        // POST api/<ModelsController>
        [HttpPost]
        public ActionResult<Model> Post([FromBody] Model model)
        {
            return Ok(_modelService.CreateModel(model));
        }

        // PUT api/<ModelsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ModelsController>/5
        [HttpDelete("{id}")]
        public ActionResult<Model> Delete(int id)
        {
            return Ok(_modelService.DeleteModel(id));
        }
    }
}
