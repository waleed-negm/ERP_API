namespace Application.BusinessLogic.CurrentAssetModules.ChecksModule.ViewModel.ChecksInSafe
{
	public class CheckInSafeContainer
	{
		public CheckInSafeContainer()
		{
			HafzaDetails = new HafzaDetails();
			CheckDetails = new List<CheckInSafeDetails>();
		}
		public HafzaDetails HafzaDetails { get; set; }
		public List<CheckInSafeDetails> CheckDetails { get; set; }
	}
}
