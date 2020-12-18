namespace XTecDigital.Models.Dtos
{
    public class CarpetaDto
    {
        public int Id { get; set; }
        public int IdGrupo { get; set; }
        public string Nombre { get; set; }
        public bool SoloLectura { get; set; }
        public bool Raiz { get; set; }
    }
}