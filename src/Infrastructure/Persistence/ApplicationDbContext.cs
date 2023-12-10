using System.Reflection;
using Application.Interfaces;
using Domain.Entities;
using Domain.Entities.Interfaces;
using Infrastructure.Persistence.Auth;
using Infrastructure.Persistence.Extenstions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Infrastructure.Persistence
{
	public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
	{
		private readonly IUserService _userService;
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IUserService userService) : base(options) { }

		#region entities
		public DbSet<Department> Departments => Set<Department>();
		public DbSet<Employee> Employees => Set<Employee>();
		public DbSet<SalaryBatch> SalaryBatches => Set<SalaryBatch>();
		public DbSet<SalaryDetails> SalaryDetails => Set<SalaryDetails>();
		public DbSet<Contacts> Contacts => Set<Contacts>();
		public DbSet<ContactBalanceInCurrency> ContactBalanceInCurrency => Set<ContactBalanceInCurrency>();
		public DbSet<NotesPayable> NotesPayables => Set<NotesPayable>();
		public DbSet<NotesPayableTransactionHistory> NotesPayableTransactionHistory => Set<NotesPayableTransactionHistory>();
		public DbSet<Check> Check => Set<Check>();
		public DbSet<CheckHafza> CheckHafza => Set<CheckHafza>();
		public DbSet<CheckHistory> CheckHistory => Set<CheckHistory>();
		public DbSet<CheckLocation> CheckLocation => Set<CheckLocation>();
		public DbSet<CheckStatus> CheckStatus => Set<CheckStatus>();
		public DbSet<Brand> Brands => Set<Brand>();
		public DbSet<ProductType> ProductTypes => Set<ProductType>();
		public DbSet<UnitMeasure> UnitMeasures => Set<UnitMeasure>();
		public DbSet<StoreItem> StoreItems => Set<StoreItem>();
		public DbSet<StoreItemWithSN> StoreItemWithSN => Set<StoreItemWithSN>();
		public DbSet<StoreTransaction> StoreTransactions => Set<StoreTransaction>();
		public DbSet<StoreItemBalanceDetails> StoreItemBalanceDetails => Set<StoreItemBalanceDetails>();
		public DbSet<Purchase> Purchases => Set<Purchase>();
		public DbSet<SupplierTransaction> SupplierTransactions => Set<SupplierTransaction>();
		public DbSet<ExpenseType> expenseTypes => Set<ExpenseType>();
		public DbSet<ExpenseItem> ExpenseItems => Set<ExpenseItem>();
		public DbSet<ExpenseSummary> ExpenseSummary => Set<ExpenseSummary>();
		public DbSet<Invoices> Invoices => Set<Invoices>();
		public DbSet<ClientTransaction> ClientTransactions => Set<ClientTransaction>();
		public DbSet<AccountChart> AccountChart => Set<AccountChart>();
		public DbSet<AccountChartCounter> AccountChartCounter => Set<AccountChartCounter>();
		public DbSet<Journal> Journal => Set<Journal>();
		public DbSet<JournalDetails> JournalDetails => Set<JournalDetails>();
		public DbSet<FiniacialPeriod> FiniacialPeriods => Set<FiniacialPeriod>();
		public DbSet<HistoricalBalance> HistoricalBalances => Set<HistoricalBalance>();
		public DbSet<Currency> Currency => Set<Currency>();
		public DbSet<Branch> Branch => Set<Branch>();
		#endregion

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			#region configuration
			builder.Entity<ClientTransaction>()
				.HasOne(ct => ct.InvoiceDetails)
				.WithMany() // Assuming there is a one-to-many relationship
				.HasForeignKey(ct => ct.InvoiceNum) // Specify the foreign key property
				.HasPrincipalKey(inv => inv.InoviceNum);

			builder.Entity<JournalDetails>()
				.HasOne(ct => ct.Trans)
				.WithMany() // Assuming there is a one-to-many relationship
				.HasForeignKey(ct => ct.TransId) // Specify the foreign key property
				.HasPrincipalKey(inv => inv.TransId);

			builder.Entity<NotesPayableTransactionHistory>()
				.HasOne(ct => ct.ChkDetails)
				.WithMany() // Assuming there is a one-to-many relationship
				.HasForeignKey(ct => ct.ChkNum) // Specify the foreign key property
				.HasPrincipalKey(inv => inv.ChkNum);

			builder.Entity<Department>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Employee>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<SalaryBatch>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<SalaryDetails>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Contacts>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ContactBalanceInCurrency>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<NotesPayable>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<NotesPayableTransactionHistory>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Check>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<CheckHafza>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<CheckHistory>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<CheckLocation>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<CheckStatus>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Brand>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ProductType>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<UnitMeasure>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<StoreItem>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<StoreItemWithSN>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<StoreTransaction>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<StoreItemBalanceDetails>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Purchase>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<SupplierTransaction>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ExpenseType>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ExpenseItem>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ExpenseSummary>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Invoices>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<ClientTransaction>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<AccountChart>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<AccountChartCounter>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Journal>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Journal>().Property(a => a.TransCount).ValueGeneratedOnAdd();
			builder.Entity<JournalDetails>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<FiniacialPeriod>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<HistoricalBalance>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Currency>().Property(a => a.Id).ValueGeneratedOnAdd();
			builder.Entity<Branch>().Property(a => a.Id).ValueGeneratedOnAdd();
			#endregion

			foreach (var entityType in builder.Model.GetEntityTypes())
			{
				if (typeof(ISoftDeletedEntity).IsAssignableFrom(entityType.ClrType))
				{
					entityType.AddSoftDeleteQueryFilter();
				}

				foreach (var property in entityType.GetProperties())
				{
					if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
					{
						property.SetColumnType("timestamp with time zone");
					}
				}
			}

			foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
			{
				relationship.DeleteBehavior = DeleteBehavior.ClientCascade;
			}

			base.OnModelCreating(builder);
		}
		public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (EntityEntry<IAuditableEntity> entry in ChangeTracker.Entries<IAuditableEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedAt = DateTime.UtcNow;
						entry.Entity.CreatedById = _userService.UserId.ToString() ?? "Manual";
						break;
					case EntityState.Modified:
						entry.Entity.UpdatedAt = DateTime.UtcNow;
						entry.Entity.UpdatedById = _userService.UserId.ToString() ?? "Manual";
						break;
				}
			}
			foreach (EntityEntry<ISoftDeletedEntity> entry in ChangeTracker.Entries<ISoftDeletedEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Deleted:
						entry.State = EntityState.Detached;
						entry.State = EntityState.Modified;
						entry.Entity.DeletedAt = DateTime.UtcNow;
						entry.Entity.DeletedById = _userService.UserId.ToString() ?? "Manual";
						break;
				}
			}
			return await base.SaveChangesAsync(cancellationToken);

		}
	}
}
