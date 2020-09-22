namespace WebAPI.Models
{
    public partial class Tenant
    {
        public Tenant()
        {
        }

        public long Id { get; set; }
        public string Tname { get; set; }
        public string Turl { get; set; }
        public string Towner { get; set; }
        public int Tstatus { get; set; }
        public int TnbrSocietes { get; set; }
        public int TnbrSalaries { get; set; }
        public string Tnote { get; set; }
    }
}