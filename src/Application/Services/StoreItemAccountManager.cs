using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
	public class StoreItemAccountManager : IStoreItemAccountManager
	{
		private readonly IAccountGenerator _accountGenerator;

		public StoreItemAccountManager(IAccountGenerator accountGenerator)
		{
			_accountGenerator = accountGenerator;
		}

		public async Task<StoreAccountsHelper> GenerateStoreItemAccountsAsync(StoreItemCreationVM vm)
		{
			var Accounts = new StoreAccountsHelper();

			// Store SupplierAccNum
			var storeAcc = new CreateAccountVM();
			storeAcc.AccountName = "Store- " + vm.Name;
			storeAcc.AccountNameAr = "مخزون- " + vm.NameAr;
			storeAcc.AccTypeId = 8;
			storeAcc.BranchId = 1;
			storeAcc.CurrencyId = 1;
			Accounts.StoreAccNum = await _accountGenerator.CreateNewAccountAsync(storeAcc);

			// PurchaseAccNum
			var purchaseAcc = new CreateAccountVM();
			purchaseAcc.AccountName = "Purchase- " + vm.Name;
			purchaseAcc.AccountNameAr = "مشتريات- " + vm.NameAr;
			purchaseAcc.AccTypeId = 21;
			purchaseAcc.BranchId = 1;
			purchaseAcc.CurrencyId = 1;
			Accounts.PurchaseAccNum = await _accountGenerator.CreateNewAccountAsync(purchaseAcc);
			return Accounts;
		}

	}
}
