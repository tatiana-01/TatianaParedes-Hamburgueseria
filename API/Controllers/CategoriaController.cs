using API.Dtos;
using API.Helpers;
using AutoMapper;
using Dominio.Entities;
using Dominio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]
    public class CategoriaController:BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public CategoriaController(IUnitOfWork unitOfWork, IMapper mapper){
            this._unitOfWork=unitOfWork;
            _Mapper=mapper;
        }

        [HttpPost()]
        [ApiVersion("1.0")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaDTO>> Post (CategoriaPostDTO categoriadto){
            var categoria= _Mapper.Map<Categoria>(categoriadto);
            _unitOfWork.Categorias.Add(categoria);
            await _unitOfWork.SaveAsync();
            if(categoria==null) return BadRequest();
            return _Mapper.Map<CategoriaDTO>(categoria);
        }

        [HttpGet()]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<CategoriaAllDTO>>> Get ([FromQuery] Params param){
            var categorias= await _unitOfWork.Categorias.GetAllAsync(param.PageIndex, param.PageSize, param.Search);
            var lstCategorias= _Mapper.Map<List<CategoriaAllDTO>>(categorias.registros);
            return new Pager<CategoriaAllDTO>(lstCategorias, categorias.totalRegistros, param.PageIndex, param.PageSize, param.Search);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaAllDTO>> Get (int id){
            var chef = await _unitOfWork.Categorias.GetByIdAsync(id);
            return _Mapper.Map<CategoriaAllDTO>(chef);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoriaDTO>> Put (int id, [FromBody]CategoriaPostDTO categoriaEdit){
            if(categoriaEdit == null) return NotFound();
            var categoria= _Mapper.Map<Categoria>(categoriaEdit);
            categoria.Id=id;
            _unitOfWork.Categorias.Update(categoria);
            await _unitOfWork.SaveAsync();
            return _Mapper.Map<CategoriaDTO>(categoria);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete (int id){
            var categoria= await _unitOfWork.Categorias.GetByIdAsync(id);
            if(categoria==null) return BadRequest();
            _unitOfWork.Categorias.Remove(categoria);
            await _unitOfWork.SaveAsync();
            return NoContent();
        }
    }
