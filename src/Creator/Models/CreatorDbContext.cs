using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Creator.Models
{
    public class CreatorDbContext : IdentityDbContext<ApplicationUser>
    {
        public CreatorDbContext(DbContextOptions options) : base(options)
        { }
    }
}
