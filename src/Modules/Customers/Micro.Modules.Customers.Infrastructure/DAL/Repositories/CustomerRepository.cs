using Micro.Modules.Customers.Core.Customers.Entities;
using Micro.Modules.Customers.Core.Customers.Repositories;
using Micro.Modules.Customers.Core.Customers.ValueObjects;
using Micro.Modules.Customers.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace Micro.Modules.Customers.Infrastructure.DAL.Repositories;

internal class CustomerRepository : ICustomerRepository
{
    private readonly CustomersDbContext _context;
    private readonly DbSet<Customer> _customers;

    public CustomerRepository(CustomersDbContext context)
    {
        _context = context;
        _customers = _context.Customers;
    }

    public Task<Customer> GetAsync(CustomerId id)
        => _customers
           .SingleOrDefaultAsync(x => x.Id == id);



    public async Task AddAsync(Customer wallet)
    {
        await _customers.AddAsync(wallet);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer wallet)
    {
        _customers.Update(wallet);
        await _context.SaveChangesAsync();
    }
}