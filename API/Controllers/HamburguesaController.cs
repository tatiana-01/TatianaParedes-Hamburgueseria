using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class HamburguesaController:BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public HamburguesaController(IUnitOfWork unitOfWork, IMapper mapper){
            this._unitOfWork=unitOfWork;
            _Mapper=mapper;
        }

        [HttpPost()]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HamburguesaDTO>> Post (HamburguesaPostDTO hamburguesadto){
            var hamburguesa= _Mapper.Map<Hamburguesa>(hamburguesadto);
            _unitOfWork.Hamburguesas.Add(hamburguesa);
            await _unitOfWork.SaveAsync();
            if(hamburguesa==null) return BadRequest();
            return _Mapper.Map<HamburguesaDTO>(hamburguesa);
        }

        [HttpGet()]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<HamburguesaAllDTO>>> Get ([FromQuery] Params param){
            var hamburguesas= await _unitOfWork.Hamburguesas.GetAllAsync(param.PageIndex, param.PageSize, param.Search);
            var lstHamburguesas= _Mapper.Map<List<HamburguesaAllDTO>>(hamburguesas.registros);
            return new Pager<HamburguesaAllDTO>(lstHamburguesas, hamburguesas.totalRegistros, param.PageIndex, param.PageSize, param.Search);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HamburguesaAllDTO>> Get (int id){
            var chef = await _unitOfWork.Hamburguesas.GetByIdAsync(id);
            return _Mapper.Map<HamburguesaAllDTO>(chef);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HamburguesaDTO>> Put (int id, [FromBody]HamburguesaPostDTO hamburguesaEdit){
            if(hamburguesaEdit == null) return NotFound();
            var hamburguesa= _Mapper.Map<Hamburguesa>(hamburguesaEdit);
            hamburguesa.Id=id;
            _unitOfWork.Hamburguesas.Update(hamburguesa);
            await _unitOfWork.SaveAsync();
            return _Mapper.Map<HamburguesaDTO>(hamburguesa);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete (int id){
            var hamburguesa= await _unitOfWork.Hamburguesas.GetByIdAsync(id);
            if(hamburguesa==null) return BadRequest();
            _unitOfWork.Hamburguesas.Remove(hamburguesa);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
