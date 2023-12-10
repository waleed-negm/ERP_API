using System.Reflection;
using Application.Interfaces;
using Application.Services;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application.DIExtensions
{
	public static class DependanciesExtension
	{
		public static IServiceCollection AddApplicationDependancies(this IServiceCollection services)
		{
			var mapper = new MapperConfiguration(mc =>
			{
				mc.ShouldMapMethod = (m) => m.ContainsGenericParameters && m.GetGenericMethodDefinition().GetGenericArguments().Any(i => i.GetGenericParameterConstraints().Length == 0);
				mc.AddMaps(Assembly.GetExecutingAssembly());
			}).CreateMapper();
			services.AddSingleton(mapper);

			services.AddHttpContextAccessor();

			services.AddScoped<ClientTransactionManager>();
			services.AddScoped<ClientReports>();
			services.AddScoped<ClientBalanceManager>();
			services.AddScoped<SupplierReport>();
			services.AddScoped<TaxManager>();
			services.AddScoped<TrailBalanceManager>();
			services.AddScoped<StatmentManager>();
			services.AddScoped<ISupplierTransactionManager, SupplierTransactionManager>();
			services.AddScoped<ISupplierBalanceManager, SupplierBalanceManager>();
			services.AddScoped<IOpeningBalanceManager, OpeningBalanceManager>();
			services.AddScoped<IJournalManager, JournalManager>();
			services.AddScoped<IAccountUpdateChecksManager, AccountUpdateChecksManager>();
			services.AddScoped<IAccountGenerator, AccountGenerator>();
			services.AddScoped<IAccountListManager, AccountListManager>();
			services.AddScoped<IUploadManager, UploadManager>();

			services.AddScoped<ClientGenerationManager>();
			services.AddScoped<NRManager>();
			services.AddScoped<NotesPayableManager>();
			services.AddScoped<SalaryBatchManager>();
			services.AddScoped<ClientJournalManager>();
			services.AddScoped<ISupplierGenerationManager, SupplierGenerationManager>();
			services.AddScoped<IStoreItemAccountManager, StoreItemAccountManager>();
			services.AddScoped<IStoreItemManager, StoreItemManager>();
			services.AddScoped<ISupplierJournalsManager, SupplierJournalsManager>();

			services.AddScoped<SalesManager>();
			services.AddScoped<ClientPayamentManager>();
			services.AddScoped<SupplierPaymentsManager>();
			services.AddScoped<ExpensesManager>();
			services.AddScoped<IPurchaseManager, PurchaseManager>();

			services.AddScoped<IUserService, UserService>();
			services.AddScoped<IDataSeedingService, DataSeedingService>();

			return services;
		}
	}
}
