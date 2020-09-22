using System.Collections.Generic;
using System.Linq;

namespace WebAPI.Models
{
    public class AuthorisationQuery : DBAction
    {
        public AuthorisationQuery(DbFirstContext pdb) : base(pdb)
        {
        }

        public List<VwAthorisation> GetListe(string login)
        {
            return db.VwAthorisations.Where(x => x.UsrLogin == login && x.UsrActive).ToList();
        }

        public VwAthorisation GetDefault(string login)
        {
            return db.VwAthorisations.FirstOrDefault(x => x.UsrLogin == login && x.UsrActive);
        }

        public VwAthorisation GetBySociete(string login, long societeId)
        {
            return db.VwAthorisations.FirstOrDefault(x => x.UsrLogin == login && x.SocieteId == societeId && x.UsrActive);
        }
    }
}