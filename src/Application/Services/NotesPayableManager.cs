using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Persistence.Extenstions;
using Domain.Enums;
using Domain.Entities;
using Application.Interfaces;
using Application.DTOs;

namespace Application.Services
{
	public class NotesPayableManager
	{
		private readonly IApplicationDbContext _db;
		private readonly IMapper _mapper;
		private readonly IJournalManager _journalManager;

		public NotesPayableManager(IApplicationDbContext db, IMapper mapper,
			IJournalManager journalManager)
		{
			_db = db;
			_mapper = mapper;
			_journalManager = journalManager;
		}

		public async Task SaveNewNPAsync(NotesPayableCreationVM vm)
		{
			var notes = _mapper.Map<NotesPayable>(vm);
			notes.CheckStatus = NotesPayableStatusEnum.UnderCollection;
			_db.NotesPayables.Add(notes);
			await _db.SaveChangesAsync();
		}


		public async Task SaveNewNPHistoryAsync(NotesPayableCreationVM vm, string TransId)
		{
			var His = new NotesPayableTransactionHistory()
			{
				ActionDate = vm.WritingDate.ToEgyptionDate(),
				ChkNum = vm.ChkNum,
				Description = $"اضافة شيك جديد رقم {vm.ChkNum}",
				PaidAmount = 0,
				StatusAfterAction = NotesPayableStatusEnum.UnderCollection,
				TransId = TransId
			};
			_db.NotesPayableTransactionHistory.Add(His);
			await _db.SaveChangesAsync();
		}

		public NPContainer GetAllNP()
		{
			var vm = new NPContainer();
			vm.CheckUnderCollection = GetNPWithStatus(NotesPayableStatusEnum.UnderCollection);
			vm.CheckCashCollection = GetNPWithStatus(NotesPayableStatusEnum.CashCollection);
			return vm;
		}


		public List<NPDetails> GetNPWithStatus(NotesPayableStatusEnum status)
		{
			var vm = _mapper.Map<List<NPDetails>>(_db.NotesPayables.Include(x => x.Currency)
				  .Include(x => x.Supplier).Include(x => x.BankAccount)
					.Where(x => x.CheckStatus == status).ToList());
			return vm;
		}

		public async Task MoveCheckToCashPaymentAsync(NPDetails np)
		{
			var check = _db.NotesPayables.Where(x => x.ChkNum == np.ChkNum).FirstOrDefault();
			check.CheckStatus = NotesPayableStatusEnum.CashCollection;
			_db.NotesPayables.Update(check);

			var His = new NotesPayableTransactionHistory()
			{
				ActionDate = DateTime.Now,
				ChkNum = np.ChkNum,
				Description = $"تحصيل نقدي لشيك رقم {np.ChkNum}",
				PaidAmount = 0,
				StatusAfterAction = NotesPayableStatusEnum.CashCollection,
				TransId = string.Empty
			};
			_db.NotesPayableTransactionHistory.Add(His);
			await _db.SaveChangesAsync();
		}

		public async Task CollectNPAsync(NPDetails np)
		{
			// update Status => collected
			// history => collected
			// journal transaction debit NP credit bankAccount
			var check = _db.NotesPayables.Where(x => x.ChkNum == np.ChkNum).FirstOrDefault();
			check.CheckStatus = NotesPayableStatusEnum.Collected;
			_db.NotesPayables.Update(check);

			var journal = new JournalVM();
			journal.TransDate = DateTime.Now.ToString("dd/MM/yyyy");
			journal.TransDesc = $"تحصيل شيك رقم {np.ChkNum} من حساب بنك {np.BankAccountName}";
			var JD_Debit = new JournalDetailsVM();
			JD_Debit.AccNum = "2170000001";
			JD_Debit.Side = TransactionSidesEnum.Debit;
			JD_Debit.Debit = np.AmountLocal;
			JD_Debit.CurrencyId = np.CurrencyId;
			journal.TransactionDetails.Add(JD_Debit);

			var JD_Credit = new JournalDetailsVM();
			JD_Credit.AccNum = np.BankAccountNum;
			JD_Credit.Side = TransactionSidesEnum.Credit;
			JD_Credit.Debit = np.AmountLocal;
			JD_Credit.CurrencyId = np.CurrencyId;
			journal.TransactionDetails.Add(JD_Credit);

			var TransId = await _journalManager.SaveJournalAsync(journal);

			var His = new NotesPayableTransactionHistory()
			{
				ActionDate = DateTime.Now,
				ChkNum = np.ChkNum,
				Description = $"تحصيل لشيك رقم {np.ChkNum}",
				PaidAmount = 0,
				StatusAfterAction = NotesPayableStatusEnum.CashCollection,
				TransId = TransId
			};
			_db.NotesPayableTransactionHistory.Add(His);
			await _db.SaveChangesAsync();
		}

		public async Task CollectCashNPAsync(NPDetails np, PaymentDetails paymentDetails)
		{
			// update Status => collected
			// history => collected
			// journal transaction debit NP credit bankAccount

			var curreny = _db.Currency.Find(np.CurrencyId);
			var check = _db.NotesPayables.Where(x => x.ChkNum == np.ChkNum).FirstOrDefault();
			check.Paid = check.Paid + paymentDetails.PaymentAmount;
			if (check.Paid == check.AmountForgin)
				check.CheckStatus = NotesPayableStatusEnum.Collected;
			else
				check.CheckStatus = NotesPayableStatusEnum.CashCollection;

			var journal = new JournalVM();
			journal.TransDate = DateTime.Now.ToString("dd/MM/yyyy");
			journal.TransDesc = $"تحصيل شيك رقم {np.ChkNum} من حساب بنك {np.BankAccountName}";
			var JD_Debit = new JournalDetailsVM();
			JD_Debit.AccNum = "2170000001";
			JD_Debit.Side = TransactionSidesEnum.Debit;
			JD_Debit.Debit = paymentDetails.PaymentAmount * curreny.Rate;
			JD_Debit.CurrencyId = np.CurrencyId;
			journal.TransactionDetails.Add(JD_Debit);

			var JD_Credit = new JournalDetailsVM();
			JD_Credit.AccNum = paymentDetails.PaymentMethod == SupplierPaymentMethod.Safe ? paymentDetails.SafeAccNum
							 : paymentDetails.PaymentMethod == SupplierPaymentMethod.Bank ? paymentDetails.BankAccNum
							 : "2170000001";
			JD_Credit.Side = TransactionSidesEnum.Credit;
			JD_Credit.Debit = paymentDetails.PaymentAmount * curreny.Rate;
			JD_Credit.CurrencyId = np.CurrencyId;
			journal.TransactionDetails.Add(JD_Credit);

			var TransId = await _journalManager.SaveJournalAsync(journal);

			if (check.CheckStatus == NotesPayableStatusEnum.Collected)
			{
				var His = new NotesPayableTransactionHistory()
				{
					ActionDate = DateTime.Now,
					ChkNum = np.ChkNum,
					Description = $"تحصيل لشيك رقم {np.ChkNum}",
					PaidAmount = 0,
					StatusAfterAction = NotesPayableStatusEnum.CashCollection,
					TransId = TransId
				};
				_db.NotesPayableTransactionHistory.Add(His);
			}
			else
			{
				var His = new NotesPayableTransactionHistory()
				{
					ActionDate = DateTime.Now,
					ChkNum = np.ChkNum,
					Description = $"تحصيل نقدي لشيك رقم {np.ChkNum} بمبلغ {paymentDetails.PaymentAmount}",
					PaidAmount = paymentDetails.PaymentAmount,
					StatusAfterAction = NotesPayableStatusEnum.CashCollection,
					TransId = TransId
				};
				_db.NotesPayableTransactionHistory.Add(His);
			}

			if (paymentDetails.PaymentMethod == SupplierPaymentMethod.Check)
			{
				var newCheck = new NotesPayableCreationVM()
				{
					AmountForgin = paymentDetails.PaymentAmount,
					AmountLocal = paymentDetails.PaymentAmount * curreny.Rate,
					BankAccountNum = paymentDetails.BankAccNum,
					ChkNum = paymentDetails.CheckNum,
					CurrencyId = np.CurrencyId,
					DueDate = paymentDetails.PaymentDueDate,
					WritingDate = paymentDetails.WritingDate,
					SupplierId = np.SupplierId

				};
				await SaveNewNPAsync(newCheck);
				await SaveNewNPHistoryAsync(newCheck, TransId);
			}
			await _db.SaveChangesAsync();

		}
	}
}
