namespace WebAPI.Models
{
    public abstract class DBAction
    {
        protected DbFirstContext db;

        public DbFirstContext Db { get => db; set => db = value; }

        protected DBAction(DbFirstContext pdb)
        {
            this.Db = pdb;
        }
    }
}