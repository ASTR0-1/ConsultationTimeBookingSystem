using CTBS.Domain.Models;
using CTBS.Infrastructure.Converters;
using CTBS.Infrastructure.Persistence.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CTBS.Infrastructure.Persistence;

public class ApplicationContext : IdentityDbContext<User, IdentityRole<int>, int>
{
	public ApplicationContext(DbContextOptions options)
		: base(options)
	{
	}

	public DbSet<Appointment> Appointments { get; set; } = null!;
	public DbSet<QuestionsCategory> QuestionsCategories { get; set; } = null!;
	public DbSet<User> Users { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder builder)
	{
		base.OnModelCreating(builder);

		builder.ApplyConfiguration(new UserConfiguration());
		builder.ApplyConfiguration(new AppointmentConfiguration());
		builder.ApplyConfiguration(new QuestionsCategoryConfiguration());
	}

	protected override void ConfigureConventions(ModelConfigurationBuilder builder)
	{
		base.ConfigureConventions(builder);

		builder.Properties<DateOnly>()
			.HaveConversion<DateOnlyConverter>();
	}
}