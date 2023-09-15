using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Controllers;
using API.Dtos;
using AutoMapper;
using Dominio.Entities;

namespace API.Profiles;
    public class MappingProfiles: Profile
    {
        public MappingProfiles(){
            CreateMap<Chef, ChefDTO>().ReverseMap();
            CreateMap<Chef, ChefIdDTO>().ReverseMap();
            CreateMap<Chef, ChefAllDTO>().ReverseMap();
            CreateMap<Hamburguesa, HamburguesaDTO>().ReverseMap();
            CreateMap<Hamburguesa, HamburguesaAllDTO>().ReverseMap();
            CreateMap<Hamburguesa, HamburguesaPostDTO>().ReverseMap();
            CreateMap<Ingrediente, IngredienteDTO>().ReverseMap();
            CreateMap<Ingrediente, IngredienteAllDTO>().ReverseMap();
            CreateMap<Ingrediente, IngredientePostDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaAllDTO>().ReverseMap();
            CreateMap<Categoria, CategoriaPostDTO>().ReverseMap();
            CreateMap<HamburguesaIngrediente, HamburguesaIngredienteDTO>().ReverseMap();
        }
    
    }
