﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Account> Accounts { get; set; }
        DbSet<Country> Countries { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<AccountCategory> AccountCategories { get; set; }
        DbSet<Address> Addresses { get; set; }
        DbSet<Note> Notes { get; set; }
        DbSet<NoteCategory> NoteCategories { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        int SaveChanges();

    }
}
