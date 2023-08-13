using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.BusinessLogic.GeneralLedgerModule.JournalModeule.ViewModel
{
	public class JournalVM : IValidatableObject
	{
		public JournalVM()
		{
			TransactionDetails = new List<JournalDetailsVM>();
			Messages = new List<string>();
		}

		[Required(ErrorMessage = "برجاء ادخال تاريخ العملية")]
		public string TransDate { get; set; }
		[Required(ErrorMessage = "برجاء ادخال وصف العملية")]
		public string TransDesc { get; set; }
		public SystemModulesEnum SystemModule { get; set; }
		public string UserName { get; set; }
		public List<string> Messages { get; set; }

		public string DocName { get; set; }
		public List<JournalDetailsVM> TransactionDetails { get; set; }

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var errors = new List<ValidationResult>();
			var TotalDebit = TransactionDetails.Sum(x => x.Debit * x.UsedRate);
			var TotalCredit = TransactionDetails.Sum(x => x.Credit * x.UsedRate);
			if (TotalDebit != TotalCredit)
				errors.Add(new ValidationResult("القيد غير متوازن"));
			DateTime TransactionDate;
			if (!DateTime.TryParse(TransDate, out TransactionDate))
			{
				errors.Add(new ValidationResult("ادخل تاريخ صحيح"));
			}

			return errors;
		}
	}
}
