namespace Application.BusinessLogic.CurrentAssetModules.ChecksModule.ViewModel.CheckInBank
{
	public class CheckInBankContainer
	{
		public CheckInBankContainer()
		{
			CheckDetails = new List<CheckInBankDetails>();
		}
		public List<CheckInBankDetails> CheckDetails { get; set; }
	}
}
