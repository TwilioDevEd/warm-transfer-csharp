using System.Linq;

namespace WarmTransfer.Web.Models.Repository
{
    public interface ICallsRepository
    {
        int CreateIfNotExists(string agentId, string conferenceId);
    }

    public class CallsRepository : ICallsRepository
    {
        private readonly WarmTransferContext _context;

        public CallsRepository(WarmTransferContext context)
        {
            _context = context;
        }

        public int CreateIfNotExists(string agentId, string conferenceId)
        {
            if (_context.Calls.Any(c => c.AgentId == agentId))
            {
                return 0;
            }

            _context.Calls.Add(new Call(agentId, conferenceId));
            return _context.SaveChanges();
        }

    }
}