using Microsoft.EntityFrameworkCore;
using System;
using Core.Models;

namespace DataStore.EF
{
	//context reprensent the database
	public class BugsContext : DbContext
	{
		public BugsContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Project> Projects { get; set; }
		public DbSet<Ticket> Tickets{ get; set; }

		/// <summary>
		/// Create database schema 
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Project>()
				.HasMany(p => p.Tickets)
				.WithOne(t => t.Project)
				.HasForeignKey(t => t.ProjectId);

			//seeding
			modelBuilder.Entity<Project>().HasData(
				new Project() { ProjectId = 1, Name = "Project 1"},
				new Project() { ProjectId = 2, Name = "Project 3"}
				);

			modelBuilder.Entity<Ticket>().HasData(
				new Ticket() { TicketId = 1, Title = "Bug #1", ProjectId= 1, Owner="JJ", ReportDate = new DateTime(2021,11,11), DueDate = new DateTime(2021,12,12) },
				new Ticket() { TicketId = 2, Title = "Bug #2", ProjectId= 1, Owner = "JJ", ReportDate = new DateTime(2021, 11, 11), DueDate = new DateTime(2021, 12, 12) },
				new Ticket() { TicketId = 3, Title = "Bug #3", ProjectId= 2 },
				new Ticket() { TicketId = 4, Title = "New Bug #4", Description="this is a new bug", ProjectId = 2 }
				);
		}
	}
}
