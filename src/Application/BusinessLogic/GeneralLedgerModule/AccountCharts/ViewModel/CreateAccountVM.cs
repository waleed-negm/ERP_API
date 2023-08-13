using System.ComponentModel.DataAnnotations;

namespace Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel
{
	public class CreateAccountVM
	{
		[Required(ErrorMessage = "برجاء اضافة اسم الحساب"), StringLength(150)]
		[Display(Name = "اسم الحساب")]
		public string AccountName { get; set; }

		[Required(ErrorMessage = "برجاء اضافة اسم الحساب باللغة العربي"), StringLength(150)]
		[Display(Name = "اسم الحساب (ع)")]
		public string AccountNameAr { get; set; }
		[Required(ErrorMessage = "برجاء اختيار نوع الحساب")]
		[Display(Name = "نوع الحساب")]
		public int AccTypeId { get; set; }

		[Required(ErrorMessage = "برجاء اختيار العملة")]
		[Display(Name = "العملة")]
		public int CurrencyId { get; set; }

		[Required(ErrorMessage = "برجاء اختيار الفرع")]
		[Display(Name = "الفرع")]
		public int BranchId { get; set; }
	}
}
