using Application.BusinessLogic.CurrentAssetModules.Inventory.Interfaces;
using Application.BusinessLogic.CurrentAssetModules.Inventory.ViewModel;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Interfaces;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;

namespace Application.BusinessLogic.CurrentAssetModules.Inventory.Services
{
	public class StoreItemAccountManager : IStoreItemAccountManager
	{
		private readonly IAccountGenerator _accountGenerator;

		public StoreItemAccountManager(IAccountGenerator accountGenerator)
		{
			_accountGenerator = accountGenerator;
		}

		public StoreAccountsHelper GenerateStoreItemAccounts(StoreItemCreationVM vm)
		{
			var Accounts = new StoreAccountsHelper();

			// Store SupplierAccNum
			var storeAcc = new CreateAccountVM();
			storeAcc.AccountName = "Store- " + vm.Name;
			storeAcc.AccountNameAr = "مخزون- " + vm.NameAr;
			storeAcc.AccTypeId = 8;
			storeAcc.BranchId = 1;
			storeAcc.CurrencyId = 1;
			Accounts.StoreAccNum = _accountGenerator.CreateNewAccount(storeAcc);

			// PurchaseAccNum
			var purchaseAcc = new CreateAccountVM();
			purchaseAcc.AccountName = "Purchase- " + vm.Name;
			purchaseAcc.AccountNameAr = "مشتريات- " + vm.NameAr;
			purchaseAcc.AccTypeId = 21;
			purchaseAcc.BranchId = 1;
			purchaseAcc.CurrencyId = 1;
			Accounts.PurchaseAccNum = _accountGenerator.CreateNewAccount(purchaseAcc);
			return Accounts;
		}

	}
}
