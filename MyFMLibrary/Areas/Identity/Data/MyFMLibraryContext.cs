using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFMLibrary.Areas.Identity.Data;
using MyFMLibrary.Models;
using System;

namespace MyFMLibrary.Data;

public class MyFMLibraryContext : IdentityDbContext<User>
{
    public MyFMLibraryContext(DbContextOptions<MyFMLibraryContext> options)
        : base(options)
    {
    }
    public DbSet<Favourite> Favourites => Set<Favourite>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
