using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Extenstions;
using Domain.Enums;
using Domain.Entities;
using Application.DTOs;
using Application.Interfaces;

namespace Application.Services
{
	public class NRManager
	{
		private readonly IJournalManager _journal;
		private readonly IApplicationDbContext _db;

		public NRManager(IApplicationDbContext db, IJournalManager journal)
		{
			_journal = journal;
			_db = db;
		}

		public async Task AddNewCheckAsync(ClientPaymentContainer vm, Currency Currency, string TransId)
		{
			var Check = new Check();
			Check.ChkNum = vm.PaymentDetails.CheckNum;
			Check.DueDate = DateTime.Parse(vm.PaymentDetails.DueDate);
			Check.CurrencyId = vm.SelectedBalance.CurrencyId;
			Check.AmountLocal = vm.PaymentDetails.PaymentAmount * Currency.Rate;
			Check.AmountForgin = vm.PaymentDetails.PaymentAmount;
			Check.ContactId = vm.ClientData.ClientId;
			Check.OrginalBank = vm.PaymentDetails.OriginalBank;
			Check.Paid = 0;
			Check.UnPaid = vm.PaymentDetails.PaymentAmount;
			Check.CheckStatusId = _db.CheckStatus.FirstOrDefault(x => x.IsDefault).Id;
			Check.CheckLocationId = _db.CheckLocation.FirstOrDefault(x => x.IsDefualt).Id;
			_db.Check.Add(Check);

			_db.CheckHistory.Add(new CheckHistory()
			{
				ChkNum = Check.ChkNum,
				Description = $" {vm.PaymentDetails.CheckNum} شيك جديد",
				TransDate = vm.PaymentDetails.PaymentDate.ToEgyptionDate(),
				TransID = TransId
			});
			await _db.SaveChangesAsync();
		}

		public CheckInSafeContainer GetChecksInSafe()
		{
			var vm = new CheckInSafeContainer();
			var CheckStatus = _db.CheckStatus.FirstOrDefault(x => x.IsDefault).Id;
			vm.CheckDetails = _db.Check.Include(x => x.Contact)
								.Include(x => x.CheckStatus).Include(x => x.Currency)
								.Where(x => x.CheckStatusId == CheckStatus && x.CheckLocationId == 1)
								.Select(x => new CheckInSafeDetails()
								{
									CheckAmount = x.AmountLocal,
									CheckAmountForiegn = x.AmountForgin,
									CurrencyAbbr = x.Currency.CurrencyAbbrev,
									CheckNum = x.ChkNum,
									ClientName = x.Contact.Name,
									CheckStatus = x.CheckStatus.CheckStatusAR,
									DueDate = x.DueDate.ToShortDateString(),
									OrginalBank = x.OrginalBank,
									Paid = x.Paid,
									Selected = false,
									UnPaid = x.UnPaid
								}).ToList();
			return vm;
		}

		public async Task MoveToBankAsync(CheckInSafeContainer vm)
		{
			// Create Hafza
			var Hafaza = await AddNewHafzaAsync(vm);
			// Select check Selected = true
			var SelectedChecks = vm.CheckDetails.Where(x => x.Selected);
			// Total selected Checks
			var CheckTotalAmount = SelectedChecks.Sum(x => x.CheckAmount);
			// Journal Transaction
			var TransactionId = await MoveToBankJournalAsync(vm, CheckTotalAmount);
			// Update Check Table
			UpdateMovedToBankChecks(vm, Hafaza, SelectedChecks, TransactionId);
			await _db.SaveChangesAsync();

		}

		private void UpdateMovedToBankChecks(CheckInSafeContainer vm, CheckHafza Hafaza,
			IEnumerable<CheckInSafeDetails> SelectedChecks, string TransactionId)
		{
			foreach (var item in SelectedChecks)
			{
				var chk = _db.Check.FirstOrDefault(x => x.ChkNum == item.CheckNum);
				chk.CheckLocationId = 2;
				chk.HafzaId = Hafaza.Id;
				chk.BankAccNum = vm.HafzaDetails.BankAccNum;
				_db.CheckHistory.Add(new CheckHistory()
				{
					ChkNum = item.CheckNum,
					TransDate = Hafaza.HafzaDate,
					TransID = TransactionId,
					Description = "Check Moved To Bank"
				});
			}
		}

		private async Task<string> MoveToBankJournalAsync(CheckInSafeContainer vm, decimal CheckTotalAmount)
		{
			var journal = new JournalVM();

			journal.TransDate = DateTime.Now.ToString("dd/MM/yyyy");
			journal.TransDesc = "Check Moved To bank by hazfa name: " + vm.HafzaDetails.HafzaName;

			var JD_DebitSide = new JournalDetailsVM();
			JD_DebitSide.AccNum = "1240000002";
			JD_DebitSide.Debit = CheckTotalAmount;
			JD_DebitSide.Side = TransactionSidesEnum.Debit;
			JD_DebitSide.CurrencyId = 1;
			journal.TransactionDetails.Add(JD_DebitSide);

			var JD_CreditSide = new JournalDetailsVM();
			JD_CreditSide.AccNum = "1240000001";
			JD_CreditSide.Credit = CheckTotalAmount;
			JD_CreditSide.Side = TransactionSidesEnum.Credit;
			JD_CreditSide.CurrencyId = 1;
			journal.TransactionDetails.Add(JD_CreditSide);
			var TransactionId = await _journal.SaveJournalAsync(journal);
			return TransactionId;
		}

		private async Task<CheckHafza> AddNewHafzaAsync(CheckInSafeContainer vm)
		{
			var Hafaza = new CheckHafza();
			Hafaza.BankAccNum = vm.HafzaDetails.BankAccNum;
			Hafaza.HafzaDate = vm.HafzaDetails.HafzaDate.ToEgyptionDate();
			Hafaza.HafzaName = vm.HafzaDetails.HafzaName;
			_db.CheckHafza.Add(Hafaza);
			await _db.SaveChangesAsync();
			return Hafaza;
		}

		public CheckInBankContainer GetCheckInBank()
		{
			var vm = new CheckInBankContainer();

			vm.CheckDetails = _db.Check.Include(x => x.Contact)
								.Include(x => x.CheckStatus)
								.Where(x => x.CheckStatusId == 1 && x.CheckLocationId == 2)
								.Select(x => new CheckInBankDetails()
								{
									CheckAmount = x.AmountLocal,
									CheckNum = x.ChkNum,
									ClientName = x.Contact.Name,
									CheckStatus = x.CheckStatus.CheckStatusAR,
									DueDate = x.DueDate.ToShortDateString(),
									OrginalBank = x.OrginalBank,
									Selected = false,
									BankAccountName = x.BankAcc.AccountName,
									BankAccountNumber = x.BankAccNum
								}).ToList();
			return vm;
		}

		public async Task CollectCheckAsync(CheckInBankContainer vm)
		{
			// Select check Selected = true
			var SelectedChecks = vm.CheckDetails.Where(x => x.Selected);
			// Total selected Checks
			var CheckTotalAmount = SelectedChecks.Sum(x => x.CheckAmount);
			// Journal Transaction
			var TransactionId = await CollectionJournalAsync(SelectedChecks);
			// Update Check Table
			UpdateCollectedCheckInfo(SelectedChecks, TransactionId);
			await _db.SaveChangesAsync();
		}

		private void UpdateCollectedCheckInfo(IEnumerable<CheckInBankDetails> SelectedChecks, string TransId)
		{
			foreach (var item in SelectedChecks)
			{
				var chk = _db.Check.FirstOrDefault(x => x.ChkNum == item.CheckNum);
				chk.CheckStatusId = 2;
				chk.CheckLocationId = 3;
				_db.CheckHistory.Add(new CheckHistory()
				{
					ChkNum = item.CheckNum,
					TransDate = DateTime.Now,
					TransID = TransId,
					Description = "Check Number " + item.CheckNum + " is collected"
				});

			}
		}

		private async Task<string> CollectionJournalAsync(IEnumerable<CheckInBankDetails> SelectedCheck)
		{
			var journal = new JournalVM();
			var CheckTotalAmount = SelectedCheck.Sum(x => x.CheckAmount);
			var SelectedByBank = SelectedCheck.GroupBy(x => new { x.BankAccountNumber })
									.Select(x => new
									{
										BankAccountNum = x.Key.BankAccountNumber,
										TotalAmount = x.Sum(y => y.CheckAmount)
									});

			journal.TransDate = DateTime.Now.ToString("dd/MM/yyyy");
			journal.TransDesc = "Check Colection ";

			foreach (var item in SelectedByBank)
			{
				var JD_DebitSide = new JournalDetailsVM();
				JD_DebitSide.AccNum = item.BankAccountNum;
				JD_DebitSide.Debit = item.TotalAmount;
				JD_DebitSide.Side = TransactionSidesEnum.Debit;
				JD_DebitSide.CurrencyId = 1;
				journal.TransactionDetails.Add(JD_DebitSide);
			}

			var JD_CreditSide = new JournalDetailsVM();
			JD_CreditSide.AccNum = "1240000002";
			JD_CreditSide.Credit = CheckTotalAmount;
			JD_CreditSide.Side = TransactionSidesEnum.Credit;
			JD_CreditSide.CurrencyId = 1;
			journal.TransactionDetails.Add(JD_CreditSide);
			var TransactionId = await _journal.SaveJournalAsync(journal);
			return TransactionId;
		}
	}
}
