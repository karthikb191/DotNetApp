using System;
using Microsoft.EntityFrameworkCore;

namespace Persistence;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    public required DbSet<Domain.Activity> Activities { get; set; }
}
