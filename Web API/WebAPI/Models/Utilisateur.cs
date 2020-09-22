namespace WebAPI.Models
{
    public partial class Utilisateur
    {
        public Utilisateur()
        {
        }

        public long Id { get; set; }
        public string UsrIdExtern { get; set; }
        public string UsrLogin { get; set; }
        public string UsrName { get; set; }
        public string UsrEmail { get; set; }
        public string UsrTel { get; set; }
        public string UsrPasswordHash { get; set; }
        public bool UsrActive { get; set; }
        public int UsrType { get; set; }
        public long SalarieId { get; set; }
    }
}