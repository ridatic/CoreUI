using System;

namespace WebAPI.Models
{
    public partial class VwAthorisation
    {
        public long UsrId { get; set; }
        public long SocieteId { get; set; }
        public long RolId { get; set; }
        public long TenantId { get; set; }
        public string UsrLogin { get; set; }
        public string UsrName { get; set; }
        public string UsrEmail { get; set; }
        public bool UsrActive { get; set; }
        public string ScName { get; set; }
        public string ScType { get; set; }
        public DateTime? ScDebutEx { get; set; }
        public DateTime? ScFinEx { get; set; }
        public int ScAnneEnCours { get; set; }
        public string ScSourceType { get; set; }
        public string RolName { get; set; }
        public string RolAuthorisations { get; set; }
    }
}