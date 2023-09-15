using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class ChefController:BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public ChefController(IUnitOfWork unitOfWork, IMapper mapper){
            this._unitOfWork=unitOfWork;
            _Mapper=mapper;
        }

        [HttpPost()]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChefIdDTO>> Post (ChefDTO chefdto){
            var chef= _Mapper.Map<Chef>(chefdto);
            _unitOfWork.Chefs.Add(chef);
            await _unitOfWork.SaveAsync();
            if(chef==null) return BadRequest();
            return _Mapper.Map<ChefIdDTO>(chef);
        }

        [HttpGet()]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<ChefAllDTO>>> Get ([FromQuery] Params param){
            var chefs= await _unitOfWork.Chefs.GetAllAsync(param.PageIndex, param.PageSize, param.Search);
            var lstChefs= _Mapper.Map<List<ChefAllDTO>>(chefs.registros);
            return new Pager<ChefAllDTO>(lstChefs, chefs.totalRegistros, param.PageIndex, param.PageSize, param.Search);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChefAllDTO>> Get (int id){
            var chef = await _unitOfWork.Chefs.GetByIdAsync(id);
            return _Mapper.Map<ChefAllDTO>(chef);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ChefIdDTO>> Put (int id, [FromBody]ChefDTO chefEdit){
            if(chefEdit == null) return NotFound();
            var chef= _Mapper.Map<Chef>(chefEdit);
            chef.Id=id;
            _unitOfWork.Chefs.Update(chef);
            await _unitOfWork.SaveAsync();
            return _Mapper.Map<ChefIdDTO>(chef);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete (int id){
            var chef= await _unitOfWork.Chefs.GetByIdAsync(id);
            if(chef==null) return BadRequest();
            _unitOfWork.Chefs.Remove(chef);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
