using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class HamburguesaIngredienteController:BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public HamburguesaIngredienteController(IUnitOfWork unitOfWork, IMapper mapper){
            this._unitOfWork=unitOfWork;
            _Mapper=mapper;
        }

        [HttpPost()]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HamburguesaIngredienteDTO>> Post (HamburguesaIngredienteDTO hamburguesadto){
            var hamburguesa= _Mapper.Map<HamburguesaIngrediente>(hamburguesadto);
            _unitOfWork.HamburguesaIngredientes.Add(hamburguesa);
            await _unitOfWork.SaveAsync();
            if(hamburguesa==null) return BadRequest();
            return _Mapper.Map<HamburguesaIngredienteDTO>(hamburguesa);
        }

        [HttpGet()]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<HamburguesaIngredienteDTO>>> Get ([FromQuery] Params param){
            var hamburguesas= await _unitOfWork.HamburguesaIngredientes.GetAllAsync(param.PageIndex, param.PageSize, param.Search);
            var lstHamburguesaIngredientes= _Mapper.Map<List<HamburguesaIngredienteDTO>>(hamburguesas.registros);
            return new Pager<HamburguesaIngredienteDTO>(lstHamburguesaIngredientes, hamburguesas.totalRegistros, param.PageIndex, param.PageSize, param.Search);
        }

        [HttpGet("{idHamburguesa}/{idIngrediente}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HamburguesaIngredienteDTO>> Get (int idHamburguesa, int idIngrediente){
            var chef = await _unitOfWork.HamburguesaIngredientes.GetByIdAsync(idHamburguesa,idIngrediente);
            return _Mapper.Map<HamburguesaIngredienteDTO>(chef);
        }

      /*   [HttpPut("{idHamburguesa}/{idIngrediente}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HamburguesaIngredienteDTO>> Put (int idHamburguesa, [FromBody]HamburguesaIngredienteDTO hamburguesaEdit){
            if(hamburguesaEdit == null) return NotFound();
            var hamburguesa= _Mapper.Map<HamburguesaIngrediente>(hamburguesaEdit);
            _unitOfWork.HamburguesaIngredientes.Update(hamburguesa);
            await _unitOfWork.SaveAsync();
            return _Mapper.Map<HamburguesaIngredienteDTO>(hamburguesa);
        } */

        [HttpDelete("{idHamburguesa}/{idIngrediente}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete (int idHamburguesa, int idIngrediente){
            var hamburguesa= await _unitOfWork.HamburguesaIngredientes.GetByIdAsync(idHamburguesa,idIngrediente);
            if(hamburguesa==null) return BadRequest();
            _unitOfWork.HamburguesaIngredientes.Remove(hamburguesa);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }