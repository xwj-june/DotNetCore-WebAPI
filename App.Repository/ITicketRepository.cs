﻿using Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Repository
{
    public interface ITicketRepository
    {
        Task<int> CreateAsync(Ticket ticekt);
        Task DeleteAsync(int id);
        Task<IEnumerable<Ticket>> GetAsync(string filter = null);
        Task<Ticket> GetByIdAsync(int id);
        Task UpdateAsync(Ticket ticekt);
    }
}