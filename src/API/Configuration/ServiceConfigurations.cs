using Application.BusinessLogic.CRM.Interfaces;
using Application.BusinessLogic.CRM.Services;
using Application.BusinessLogic.CurrentAssetModules.ChecksModule.Services;
using Application.BusinessLogic.CurrentAssetModules.Inventory.Interfaces;
using Application.BusinessLogic.CurrentAssetModules.Inventory.Services;
using Application.BusinessLogic.CurrentLiabilitiesModules.NotesPayableModule.Services;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Interfaces;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Services;
using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Interfaces;
using Application.BusinessLogic.GeneralLedgerModule.JournalModeule.Services;
using Application.BusinessLogic.HR.Services;
using Application.BusinessLogic.PurchasesModule.Interfaces;
using Application.BusinessLogic.PurchasesModule.Services;
using Application.BusinessLogic.SalesModule.Service;
using Application.Common.Interfaces;
using Application.Common.Services;
using Domain.Entities.Auth;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;

namespace API.Configuration
{
	public static class ServiceConfigurations
	{
		public static void AddServices(this IServiceCollection services)
		{
			services.AddAutoMapper(typeof(Program));

			//var mapperConfig = new MapperConfiguration(mc =>
			//{
			//	mc.ShouldMapMethod = (m) =>
			//	{
			//		return m.ContainsGenericParameters && m.GetGenericMethodDefinition()
			//			.GetGenericArguments()
			//			.Any(i => i.GetGenericParameterConstraints().Length == 0);
			//	};
			//	mc.AddMaps(Assembly.GetExecutingAssembly());
			//});
			//IMapper mapper = mapperConfig.CreateMapper();
			//services.AddSingleton(mapper);

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

			services.AddHttpContextAccessor();
			services.AddIdentity<ApplicationUser, IdentityRole>(o => o.Password = new PasswordOptions
			{
				RequireDigit = true,
				RequiredLength = 8,
				RequireLowercase = true,
				RequireUppercase = true,
				RequireNonAlphanumeric = false
			}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

		}
	}
}
