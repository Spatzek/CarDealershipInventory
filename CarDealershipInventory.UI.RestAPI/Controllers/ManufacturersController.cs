using CarDealershipInventory.Core.ApplicationServices;
using CarDealershipInventory.Core.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CarDealershipInventory.UI.RestAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturersController : ControllerBase
    {
        private IManufacturerService _manuService;

        public ManufacturersController(IManufacturerService manuService)
        {
            _manuService = manuService;
        }

        // GET: api/<ManufacturersController>
        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<Manufacturer>> Get()
        {
            try
            {
                return Ok(_manuService.GetAllManufacturers());
            }
            catch (NullReferenceException e)
            {
                return StatusCode(404, e.Message);
            }
        }

        // GET api/<ManufacturersController>/5
        [Authorize]
        [HttpGet("{id}")]
        public ActionResult<Manufacturer> Get(int id)
        {
            try
            {
                return Ok(_manuService.GetManufacturerById(id));
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

        // POST api/<ManufacturersController>
        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public ActionResult<Manufacturer> Post([FromBody] Manufacturer manufacturer)
        {
            try
            {
                return Ok(_manuService.CreateManufacturer(manufacturer));
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

        // PUT api/<ManufacturersController>/5
        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ManufacturersController>/5
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public ActionResult<Manufacturer> Delete(int id)
        {
            try
            {
                return Ok(_manuService.DeleteManufacturer(id));
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
