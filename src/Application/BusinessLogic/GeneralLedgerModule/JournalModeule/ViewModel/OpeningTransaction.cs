using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel
{
	public class OpeningTransaction : IValidatableObject
	{
		public OpeningTransaction()
		{
			TransactionDetails = new List<OpeningTransactionDetails>();
		}
		public long CurrentFinaicialPeriodId { get; set; }
		[Required]
		public string TransDate { get; set; }
		[Required]
		public string TransDesc { get; set; }
		public SystemModulesEnum SystemModule { get; set; } = SystemModulesEnum.GL;
		public string UserName { get; set; }

		public List<OpeningTransactionDetails> TransactionDetails { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var errors = new List<ValidationResult>();
			var TotalDebit = TransactionDetails.Sum(x => x.Debit * x.UsedRate);
			var TotalCredit = TransactionDetails.Sum(x => x.Credit * x.UsedRate);
			if (TotalDebit != TotalCredit)
				errors.Add(new ValidationResult("القيد غير متوازن"));


			return errors;
		}
	}
}
