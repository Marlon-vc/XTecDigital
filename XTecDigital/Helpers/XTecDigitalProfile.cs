using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using XTecDigital.Models;
using XTecDigital.Models.Dtos;

namespace XTecDigital.Helpers
{
    public class XTecDigitalProfile: Profile
    {
        public XTecDigitalProfile()
        {
            //Crear mapas para dtos
            CreateMap<Carpeta, CarpetaDto>();
            CreateMap<Archivo, ArchivoDto>();
            CreateMap<Curso, CursoDto>();
            CreateMap<Evaluacion, EvaluacionDto>();
            CreateMap<Rubro, RubroDto>();
            CreateMap<Noticia, NoticiaDto>();
        }
    }
}
