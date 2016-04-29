using System;
using System.Data.Entity;
using System.Linq;

namespace WarmTransfer.Web.Models.Repository
{
    public interface ICallsRepository
    {
        int CreateIfNotExists(string agentId, string conferenceId);

        Call FindByAgentId(string agentId);
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
                var call = _context.Calls.First(c => c.AgentId == agentId);
                call.ConferenceId = conferenceId;

                _context.Entry(call).State = EntityState.Modified;
                return _context.SaveChanges();
            }

            _context.Calls.Add(new Call(agentId, conferenceId));
            return _context.SaveChanges();
        }

        public Call FindByAgentId(string agentId)
        {
            return _context.Calls.Where(c => c.AgentId == agentId).FirstOrDefault();
        }
    }
}