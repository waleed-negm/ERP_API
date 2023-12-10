using Application.Common.DTOs;
using Application.DTOs;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
	public class SupplierGenerationManager : ISupplierGenerationManager
	{
		private readonly IApplicationDbContext _db;
		private readonly IMapper _mapper;
		private readonly IAccountGenerator _accountGenerator;

		public SupplierGenerationManager(IApplicationDbContext db, IMapper mapper, IAccountGenerator accountGenerator)
		{
			_db = db;
			_mapper = mapper;
			_accountGenerator = accountGenerator;
		}

		public async Task<ResponseDto> GetAllAsync()
		{
			var suppliers = await _db.Contacts.Where(x => x != null && x.IsSupplier).ToListAsync();
			return new ResponseDto()
			{
				Body = suppliers,
				Status = true,
				message = "تم تسجيل البيانات بنجاح"
			};
		}
		public async Task<ResponseDto> AddAsync(ContactCreatingViewModel model)
		{
			Contacts NewSupplier = new()
			{
				Name = model.Name,
				NameAR = model.NameAR,
				Phone1 = model.Phone1,
				Phone2 = model.Phone2,
				Email = model.Email,
				IsSupplier = true,
			};
			NewSupplier.IsSupplier = true;
			if (model.CreateAccount)
			{
				var account = new CreateAccountVM();
				account.AccountName = model.Name;
				account.AccountNameAr = model.NameAR;
				account.AccTypeId = 14;
				account.BranchId = 1;
				account.CurrencyId = 1;
				NewSupplier.SupplierAccNum = await _accountGenerator.CreateNewAccountAsync(account);
			}
			_db.Contacts.Add(NewSupplier);
			await _db.SaveChangesAsync();
			return new ResponseDto() { Status = true, Body = _mapper.Map<SupplierDto>(NewSupplier), message = "تم تسجيل البيانات بنجاح" };
		}
	}
}
