namespace Application.BusinessLogic.CurrentLiabilitiesModules.NotesPayableModule.ViewModel
{
	public class NotesPayableCreationVM
	{
		public string ChkNum { get; set; }

		public string WritingDate { get; set; }

		public string DueDate { get; set; }

		public decimal AmountLocal { get; set; }


		public decimal AmountForgin { get; set; }

		public long CurrencyId { get; set; }

		public string BankAccountNum { get; set; }

		public int SupplierId { get; set; }


	}
}
