using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class IngredienteController:BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public IngredienteController(IUnitOfWork unitOfWork, IMapper mapper){
            this._unitOfWork=unitOfWork;
            _Mapper=mapper;
        }

        [HttpPost()]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IngredienteDTO>> Post (IngredientePostDTO ingredientedto){
            var ingrediente= _Mapper.Map<Ingrediente>(ingredientedto);
            _unitOfWork.Ingredientes.Add(ingrediente);
            await _unitOfWork.SaveAsync();
            if(ingrediente==null) return BadRequest();
            return _Mapper.Map<IngredienteDTO>(ingrediente);
        }

        [HttpGet()]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<IngredienteAllDTO>>> Get ([FromQuery] Params param){
            var ingredientes= await _unitOfWork.Ingredientes.GetAllAsync(param.PageIndex, param.PageSize, param.Search);
            var lstIngredientes= _Mapper.Map<List<IngredienteAllDTO>>(ingredientes.registros);
            return new Pager<IngredienteAllDTO>(lstIngredientes, ingredientes.totalRegistros, param.PageIndex, param.PageSize, param.Search);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IngredienteAllDTO>> Get (int id){
            var chef = await _unitOfWork.Ingredientes.GetByIdAsync(id);
            return _Mapper.Map<IngredienteAllDTO>(chef);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IngredienteDTO>> Put (int id, [FromBody]IngredientePostDTO ingredienteEdit){
            if(ingredienteEdit == null) return NotFound();
            var ingrediente= _Mapper.Map<Ingrediente>(ingredienteEdit);
            ingrediente.Id=id;
            _unitOfWork.Ingredientes.Update(ingrediente);
            await _unitOfWork.SaveAsync();
            return _Mapper.Map<IngredienteDTO>(ingrediente);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete (int id){
            var ingrediente= await _unitOfWork.Ingredientes.GetByIdAsync(id);
            if(ingrediente==null) return BadRequest();
            _unitOfWork.Ingredientes.Remove(ingrediente);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }