using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.DTOs
{
	public class ExpenseVM : IValidatableObject
	{
		public ExpenseDetailsVM ExpenseDetails { get; set; } = new ExpenseDetailsVM();
		public PaymentDetails PaymentDetails { get; set; } = new PaymentDetails();
		public bool IsWaitingAreaVisible { get; set; } // Upload data => load view
		public bool IsDetailAreaVisible { get; set; } // Normal View
		public bool IsMessageAreaVisible { get; set; } // Show In case an error
		public string SaveURL { get; set; }
		public List<string> Messages { get; set; } = new List<string>();

		public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			var errors = new List<ValidationResult>();
			if (PaymentDetails.PaymentMethod == SupplierPaymentMethod.Credit)
			{
				if (!ExpenseDetails.SupplierId.HasValue)
					errors.Add(new ValidationResult("برجاء اختيار المورد"));
			}
			if (PaymentDetails.PaymentAmount < ExpenseDetails.Amount)
			{
				if (!ExpenseDetails.SupplierId.HasValue)
					errors.Add(new ValidationResult("برجاء اختيار المورد"));
			}

			return errors;
		}
	}
}
