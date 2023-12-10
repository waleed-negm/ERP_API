using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Application.Services
{

	public class ClientGenerationManager
	{
		private readonly IApplicationDbContext _db;
		private readonly IMapper _mapper;
		private readonly IAccountGenerator _accountGenerator;
		public ClientGenerationManager(IApplicationDbContext db, IMapper mapper, IAccountGenerator accountGenerator)
		{
			_db = db;
			_mapper = mapper;
			_accountGenerator = accountGenerator;
		}

		public IEnumerable<Contacts> GetAllClients() => _db.Contacts.Where(x => x.IsClient == true).ToList();

		public async Task AddNewClientAsync(ContactCreatingViewModel Client)
		{
			var NewClient = _mapper.Map<Contacts>(Client);
			NewClient.IsClient = true;
			if (Client.CreateAccount)
			{
				var account = new CreateAccountVM();
				account.AccountName = Client.Name;
				account.AccountNameAr = Client.NameAR;
				account.AccTypeId = 6;
				account.BranchId = 1;
				account.CurrencyId = 1;
				NewClient.SupplierAccNum = await _accountGenerator.CreateNewAccountAsync(account);
			}

			_db.Contacts.Add(NewClient);
			await _db.SaveChangesAsync();
		}

	}
}
