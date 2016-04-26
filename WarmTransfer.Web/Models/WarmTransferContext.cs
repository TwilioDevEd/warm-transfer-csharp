using System.Data.Entity;

namespace WarmTransfer.Web.Models
{
    public class WarmTransferContext : DbContext
    {
        public WarmTransferContext() : base("WarmTransferConnection") { }

        public DbSet<Call> Calls { get; set; }
    }
}