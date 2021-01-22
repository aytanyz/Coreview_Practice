using CoreCodeCamp.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using CoreCodeCamp.Models;
using AutoMapper;
using Microsoft.AspNetCore.Routing;

namespace CoreCodeCamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]                 // for body binding (we will take value from the body EX:Postman -> body)
    public class CampsController : ControllerBase
    {
        private readonly ICampRepository _repository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public CampsController(ICampRepository repository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _repository = repository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }

        [HttpGet]
        // public async Task<ActionResult> GEt()
        public async Task<IActionResult> Get( bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsAsync(includeTalks);

                CampModel[] models = _mapper.Map<CampModel[]>(results);
                // return _mapper.Map<CampModel[]>(results);

                // return models;
                return Ok(models);
            }
            catch 
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failer!");
            }
        }

        [HttpGet("{moniker}")]
        public async Task<ActionResult<CampModel>> Get(string moniker)
        {
            try
            {
                var result = await _repository.GetCampAsync(moniker);
                
                if (result == null)
                    return NotFound();
                return _mapper.Map<CampModel>(result);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failer!");
            }
        }

        [HttpGet("search")]
        public async Task<ActionResult<CampModel[]>> SearchDate(DateTime thedate, bool includeTalks = false)
        {
            try
            {
                var results = await _repository.GetAllCampsByEventDate(thedate, includeTalks);

                if (!results.Any())
                    return NotFound();
                return _mapper.Map<CampModel[]>(results);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failer!");
            }
        }

        public async Task<ActionResult<CampModel>> Post(CampModel model)
        {
            try
            {
                var existing = await _repository.GetCampAsync(model.Moniker);
                if(existing != null)
                    return BadRequest("Moniker in Use");


                var location = _linkGenerator.GetPathByAction("Get", "Camps", new { moniker = model.Moniker });
                if (string.IsNullOrWhiteSpace(location))
                    return BadRequest("Could not use current moniker");

                var camp = _mapper.Map<Camp>(model);
                _repository.Add(camp);
                if (await _repository.SaveChangesAsync())
                    return Created($"/api/camps/{camp.Moniker}", _mapper.Map<CampModel>(camp));               
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database failer!");
            }
            return BadRequest();
        }

    }
}
