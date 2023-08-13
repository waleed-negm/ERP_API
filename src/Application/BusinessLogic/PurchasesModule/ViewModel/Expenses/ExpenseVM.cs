using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.BusinessLogic.PurchasesModule.ViewModel.Expenses
{
	public class ExpenseVM : IValidatableObject
	{
		public ExpenseVM()
		{
			ExpenseDetails = new ExpenseDetailsVM();
			PaymentDetails = new PaymentDetails();
			Messages = new List<string>();
		}
		public ExpenseDetailsVM ExpenseDetails { get; set; }
		public PaymentDetails PaymentDetails { get; set; }

		public bool IsWaitingAreaVisible { get; set; } // Upload data => load view
		public bool IsDetailAreaVisible { get; set; } // Normal View
		public bool IsMessageAreaVisible { get; set; } // Show In case an error

		public string SaveURL { get; set; }
		public List<string> Messages { get; set; }

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
