using MyApp.Repository.ApiClient;
using Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly IWebApiExecuter webApiExecuter;

        public TicketRepository(IWebApiExecuter webApiExecuter)
        {
            this.webApiExecuter = webApiExecuter;
        }

        public async Task<IEnumerable<Ticket>> GetAsync(string filter = null)
        {
            string uri = "api/tickets?api-version=2.0";
            if (!string.IsNullOrWhiteSpace(filter))
            {
                uri += $"&titleordescription={filter.Trim()}";
            }
            return await webApiExecuter.InvokeGet<IEnumerable<Ticket>>(uri);
        }

        public async Task<Ticket> GetByIdAsync(int id)
        {
            return await webApiExecuter.InvokeGet<Ticket>($"api/tickets/{id}?api-version=2.0");
        }

        public async Task<int> CreateAsync(Ticket ticekt)
        {
            ticekt = await webApiExecuter.InvokePost("api/tickets?api-version=2.0", ticekt);
            return ticekt.TicketId.Value;
        }

        public async Task UpdateAsync(Ticket ticekt)
        {
            await webApiExecuter.InvokePut($"api/tickets/{ticekt.TicketId}?api-version=2.0", ticekt);
        }

        public async Task DeleteAsync(int id)
        {
            await webApiExecuter.InvokeDelete($"api/tickets/{id}?api-version=2.0");
        }
    }
}
