using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace WarmTransfer.Web.Models.Repository
{
    public interface ICallsRepository
    {
        Task<int> CreateOrUpdateAsync(string agentId, string conferenceId);

        Task<Call> FindByAgentIdAsync(string agentId);
    }

    public class CallsRepository : ICallsRepository
    {
        private readonly WarmTransferContext _context;

        public CallsRepository(WarmTransferContext context)
        {
            _context = context;
        }

        public Task<int> CreateOrUpdateAsync(string agentId, string conferenceId)
        {
            if (_context.Calls.Any(c => c.AgentId == agentId))
            {
                var call = _context.Calls.First(c => c.AgentId == agentId);
                call.ConferenceId = conferenceId;

                _context.Entry(call).State = EntityState.Modified;
                return _context.SaveChangesAsync();
            }

            _context.Calls.Add(new Call(agentId, conferenceId));
            return _context.SaveChangesAsync();
        }

        public Task<Call> FindByAgentIdAsync(string agentId)
        {
            return _context.Calls.Where(c => c.AgentId == agentId).FirstOrDefaultAsync();
        }
    }
}