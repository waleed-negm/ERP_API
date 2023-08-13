﻿using Domain.Enums;

namespace Application.BusinessLogic.SalesModule.ViewModel.Payment
{
	public class ClientPaymentContainer
	{
		public ClientPaymentContainer()
		{
			ClientData = new ClientData();
			ClientBalanceDetails = new List<ClientBalanceDetails>();
			SelectedBalance = new ClientBalanceDetails();
			PaymentDetails = new ClientPaymentDetails();
			PaymentDetails.PaymentMethod = ClientPaymentMethod.Safe;
			PaymentDetails.IsSafe = true;
		}
		public ClientData ClientData { get; set; }
		public List<ClientBalanceDetails> ClientBalanceDetails { get; set; }
		public ClientBalanceDetails SelectedBalance { get; set; }
		public ClientPaymentDetails PaymentDetails { get; set; }

	}
}
