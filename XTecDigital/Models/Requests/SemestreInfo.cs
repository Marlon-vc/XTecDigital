using System.Collections.Generic;

namespace XTecDigital.Models.Requests
{
    public class SemestreInfo
    {
        public int Id { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }
        public List<GrupoInfo> Grupos { get; set; }
        
    }
}