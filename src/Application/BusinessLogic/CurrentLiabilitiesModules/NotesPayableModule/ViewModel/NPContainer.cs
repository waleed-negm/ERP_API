using Application.BusinessLogic.PurchasesModule.ViewModel;

namespace Application.BusinessLogic.CurrentLiabilitiesModules.NotesPayableModule.ViewModel
{
	public class NPContainer
	{
		public NPContainer()
		{
			CheckCashCollection = new List<NPDetails>();
			CheckUnderCollection = new List<NPDetails>();
			PaymentDetails = new PaymentDetails();
			SelectedNote = new NPDetails();
		}
		public List<NPDetails> CheckUnderCollection { get; set; }
		public List<NPDetails> CheckCashCollection { get; set; }

		public NPDetails SelectedNote { get; set; }
		public PaymentDetails PaymentDetails { get; set; }
	}
}
