using Microsoft.EntityFrameworkCore;
using Studieforeningskalender_Backend.Domain.EmailReputations;
using Studieforeningskalender_Backend.Domain.Events;
using Studieforeningskalender_Backend.Domain.EventTags;
using Studieforeningskalender_Backend.Domain.EventUsers;
using Studieforeningskalender_Backend.Domain.Roles;
using Studieforeningskalender_Backend.Domain.UserRoles;
using Studieforeningskalender_Backend.Domain.Users;

namespace Studieforeningskalender_Backend.Infrastructure
{
	public class AppDbContext : DbContext
	{
		public AppDbContext(DbContextOptions<AppDbContext> options)
			: base(options)
		{
		}

		public DbSet<Role> Role { get; set; }
		public DbSet<UserRole> UserRole { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<EventUser> EventUser { get; set; }
		public DbSet<Event> Event { get; set; }
		public DbSet<EventTag> EventTag { get; set; }
		public DbSet<Domain.Tags.Tag> Tag { get; set; }
		public DbSet<EmailReputation> EmailReputation { get; set; }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			// UserRole
			modelBuilder.Entity<UserRole>()
				.HasKey(ur => new { ur.UserId, ur.RoleId });

			modelBuilder.Entity<UserRole>()
				.HasOne(ur => ur.User)
				.WithMany(u => u.UserRoles)
				.HasForeignKey(ur => ur.UserId);

			modelBuilder.Entity<UserRole>()
				.HasOne(ur => ur.Role)
				.WithMany(r => r.UserRoles)
				.HasForeignKey(ur => ur.RoleId);

			// EventUser
			modelBuilder.Entity<EventUser>()
			.HasKey(ep => new { ep.EventId, ep.UserId });

			modelBuilder.Entity<EventUser>()
				.HasOne(ep => ep.Event)
				.WithMany(e => e.EventUsers)
				.HasForeignKey(ep => ep.EventId);

			modelBuilder.Entity<EventUser>()
				.HasOne(ep => ep.User)
				.WithMany(u => u.EventUsers)
				.HasForeignKey(ep => ep.UserId);

			// EventTag
			modelBuilder.Entity<EventTag>()
				.HasKey(et => new { et.EventId, et.TagId });

			modelBuilder.Entity<EventTag>()
				.HasOne(et => et.Event)
				.WithMany(e => e.EventTags)
				.HasForeignKey(et => et.EventId);

			modelBuilder.Entity<EventTag>()
				.HasOne(et => et.Tag)
				.WithMany(t => t.EventTags)
				.HasForeignKey(et => et.TagId);
		}

	}
}
