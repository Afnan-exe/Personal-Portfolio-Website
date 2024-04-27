using Microsoft.EntityFrameworkCore;
using Portfolio.Models;
namespace Portfolio.Models
{
	public class _context:DbContext
	{
		public _context(DbContextOptions<_context> x) :base(x) { }

		public DbSet<Contact> Contacts { get; set; }
		public DbSet<Projects> Project { get; set; }
	    public DbSet<LoginRegister> LoginRegister { get; set; }
	}
}
