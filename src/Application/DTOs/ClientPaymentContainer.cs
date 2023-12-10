using Domain.Enums;

namespace Application.DTOs
{
	public class ClientPaymentContainer
	{
		public ClientData ClientData { get; set; } = new ClientData();
		public List<ClientBalanceDetails> ClientBalanceDetails { get; set; } = new List<ClientBalanceDetails>();
		public ClientBalanceDetails SelectedBalance { get; set; } = new ClientBalanceDetails();
		public ClientPaymentDetails PaymentDetails { get; set; } = new ClientPaymentDetails()
		{
			PaymentMethod = ClientPaymentMethod.Safe,
			IsSafe = true
		};

	}
}
