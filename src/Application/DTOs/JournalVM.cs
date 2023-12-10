using Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
	public class JournalVM : IValidatableObject
	{
		[Required]
		public string TransDate { get; set; }
		[Required]
		public string TransDesc { get; set; }
		public SystemModulesEnum SystemModule { get; set; }
		public string UserName { get; set; }
		public List<string> Messages { get; set; } = new List<string>();
		public string DocName { get; set; }
		public List<JournalDetailsVM> TransactionDetails { get; set; } = new List<JournalDetailsVM>();

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
