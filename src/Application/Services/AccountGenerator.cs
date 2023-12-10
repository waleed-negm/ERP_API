using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{
	public class AccountGenerator : IAccountGenerator
	{
		private readonly IMapper _map;
		private readonly IApplicationDbContext _db;

		public AccountGenerator(IApplicationDbContext db, IMapper map)
		{
			_map = map;
			_db = db;
		}

		public async Task<string> CreateNewAccountAsync(CreateAccountVM newAccount)
		{
			var account = _map.Map<AccountChart>(newAccount);
			var currentcounter = _db.AccountChartCounter.Find(newAccount.AccTypeId);

			// Creating Account Number
			account.AccNum = (decimal.Parse(currentcounter.ParentAccNum) + currentcounter.Count + 1).ToString();

			// Parent Account
			account.ParentAcNum = currentcounter.ParentAccNum;

			//Account Nature
			var ParentAccount = _db.AccountChart.FirstOrDefault(x => x.AccNum.Trim()
										== currentcounter.ParentAccNum.Trim());
			account.AccountNature = ParentAccount.AccountNature;

			// Other filed
			account.Balance = 0;
			account.StartingBalance = 0;
			account.IsActive = true;
			account.IsParent = false;

			// Update AccountChartCounter
			currentcounter.Count = currentcounter.Count + 1;

			// Save New Account
			_db.AccountChart.Add(account);
			// Update Account Chart Counter
			_db.AccountChartCounter.Update(currentcounter);

			await _db.SaveChangesAsync();

			return account.AccNum;
		}


		public async Task<string> GenerateAccountAsync(CreateAccountVM newAccount)
		{
			var account = _map.Map<AccountChart>(newAccount);
			var currentcounter = _db.AccountChartCounter.Find(newAccount.AccTypeId);

			// Creating Account Number
			account.AccNum = (decimal.Parse(currentcounter.ParentAccNum) + currentcounter.Count + 1).ToString();

			// Parent Account
			account.ParentAcNum = currentcounter.ParentAccNum;

			//Account Nature
			var ParentAccount = _db.AccountChart.FirstOrDefault(x => x.AccNum.Trim()
										== currentcounter.ParentAccNum.Trim());
			account.AccountNature = ParentAccount.AccountNature;

			// Other filed
			account.Balance = 0;
			account.StartingBalance = 0;
			account.IsActive = true;
			account.IsParent = false;

			// Update AccountChartCounter

			currentcounter.Count = currentcounter.Count + 1;

			// Save New Account
			_db.AccountChart.Add(account);
			// Update Account Chart Counter
			_db.AccountChartCounter.Update(currentcounter);
			await _db.SaveChangesAsync();

			return account.AccNum;
		}




	}
}
