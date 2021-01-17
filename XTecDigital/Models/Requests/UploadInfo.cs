namespace XTecDigital.Models.Requests
{
    public class UploadInfo
    {
        public string FileData { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }

        public string Carpeta { get; set; }
        public string TipoCarpeta { get; set; }
        public int Numero { get; set; }
        public string Curso { get; set; }
        public int Anio { get; set; }
        public string Periodo { get; set; }
    }
}