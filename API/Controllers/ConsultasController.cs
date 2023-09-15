using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Dtos;
using API.Helpers;
using Microsoft.AspNetCore.Mvc;
using Dominio.Entities;
using Dominio.Interfaces;
using AutoMapper;
using System.Collections;

namespace API.Controllers;
[ApiVersion("1.0")]
[ApiVersion("1.1")]

    public class ConsultasController:BaseApiController
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _Mapper;

        public ConsultasController(IUnitOfWork unitOfWork, IMapper mapper){
            this._unitOfWork=unitOfWork;
            _Mapper=mapper;
        }

        [HttpGet("stockmenos400")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<IngredienteAllDTO>>> Get400 ([FromQuery] Params param){
            var categorias= await _unitOfWork.Ingredientes.GetStock400(param.PageIndex, param.PageSize, param.Search);
            var lstCategorias= _Mapper.Map<List<IngredienteAllDTO>>(categorias.registros);
            return new Pager<IngredienteAllDTO>(lstCategorias, categorias.totalRegistros, param.PageIndex, param.PageSize, param.Search);
        }

        [HttpGet("vegetarianas")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<object> GetVeggies (){
            var vegetarianas= await _unitOfWork.Hamburguesas.GetVegetariana();
            return vegetarianas;
        }

        [HttpGet("chefsCarnes")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<ChefAllDTO>> GetChefs (){
            var chefs= await _unitOfWork.Chefs.GetChefsCarnes();
            return _Mapper.Map<IEnumerable<ChefAllDTO>>(chefs);
        }

        [HttpGet("nombrechef/{chef}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<HamburguesaAllDTO>> GetHamburguesasChef (string chef){
            var hamburguesas=await _unitOfWork.Hamburguesas.GetHamburguesas(chef);
            if(hamburguesas==null)BadRequest();
             return _Mapper.Map<IEnumerable<HamburguesaAllDTO>>(hamburguesas);
        }

        [HttpGet("menora9")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<HamburguesaAllDTO>> Get9Hamburguesas (){
            var hamburguesas=await _unitOfWork.Hamburguesas.get9();
            return _Mapper.Map<IEnumerable<HamburguesaAllDTO>>(hamburguesas);
        }

        [HttpGet("precio2a5")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Pager<IngredienteAllDTO>>> Get2a5 ([FromQuery] Params param){
            var categorias= await _unitOfWork.Ingredientes.GetPrecio2a5(param.PageIndex, param.PageSize, param.Search);
            var lstCategorias= _Mapper.Map<List<IngredienteAllDTO>>(categorias.registros);
            return new Pager<IngredienteAllDTO>(lstCategorias, categorias.totalRegistros, param.PageIndex, param.PageSize, param.Search);
        }

        
        [HttpPost("{idIngrediente}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<HamburguesaIngredienteDTO>> Post (int idIngrediente){
            var hamburguesa= _unitOfWork.HamburguesaIngredientes.AddToClassic(idIngrediente);
            await _unitOfWork.SaveAsync();
            if(idIngrediente==null) return BadRequest();
            return _Mapper.Map<HamburguesaIngredienteDTO>(hamburguesa);
        }

        [HttpGet("gourmet")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<CategoriaAllDTO>> GetFoourmet (){
            var categorias= await _unitOfWork.Categorias.GetGourmet();
            return _Mapper.Map<IEnumerable<CategoriaAllDTO>>(categorias);
        }

        [HttpGet("precioascendente")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<HamburguesaAllDTO>> Getascendente (){
            var hamburguesas= await _unitOfWork.Hamburguesas.getAscending();
            return _Mapper.Map<IEnumerable<HamburguesaAllDTO>>(hamburguesas);
        }

        [HttpGet("conpanintegral")]
        [MapToApiVersion("1.1")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<HamburguesaIngredienteDTO>> conpanintegral (){
            List<Hamburguesa>hamburguesas= new List<Hamburguesa>();
        var HamburguesaIngrediente= await _unitOfWork.Hamburguesas.GetHamburguesasconPanIntegral();
        foreach (var item in HamburguesaIngrediente)
        {
            var hamburguesa= await _unitOfWork.Hamburguesas.Primero(item.Hamburguesa_id);
            hamburguesas.Add(hamburguesa);
        }
            return _Mapper.Map<IEnumerable<HamburguesaIngredienteDTO>>(hamburguesas);
        }

        [HttpGet("IngredienteMasCaro")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IngredienteDTO Caro (){
            
            return _Mapper.Map<IngredienteDTO>(_unitOfWork.Ingredientes.GetMostExpensive());
        }

        [HttpPost("Cambio")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IngredienteDTO cambio (){
            var result=_Mapper.Map<Ingrediente>(_unitOfWork.Ingredientes.cambioPanPorFresco());
            _unitOfWork.Ingredientes.Update(result);
            _unitOfWork.SaveAsync();
            return _Mapper.Map<IngredienteDTO>(result);
        }
    }
