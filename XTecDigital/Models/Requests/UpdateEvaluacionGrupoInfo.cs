namespace XTecDigital.Models.Requests
{
    public class UpdateEvaluacionGrupoInfo
    {
        public int IdEvaluacionGrupo { get; set; }
        public decimal Nota { get; set; }
        public string Observaciones { get; set; }
        public string Detalle { get; set; }
        public string FileData { get; set; }
        public int Size { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }

    }
}