namespace XTecDigital.Models.Requests
{
    public class UploadInfo
    {
        public string FileData { get; set; }
        public string Name { get; set; }
        public int Size { get; set; }
        public int FolderId { get; set; }
        public int GroupId { get; set; }
    }
}