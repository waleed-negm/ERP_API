using Application.BusinessLogic.CRM.Interfaces;
using Application.BusinessLogic.CRM.ViewModel;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Interfaces;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;
using Application.Common.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Application.BusinessLogic.CRM.Services
{
	public class SupplierGenerationManager : ISupplierGenerationManager
	{
		private readonly ApplicationDbContext _db;
		private readonly IMapper _mapper;
		private readonly IAccountGenerator _accountGenerator;

		public SupplierGenerationManager(ApplicationDbContext db, IMapper mapper, IAccountGenerator accountGenerator)
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
			using (var transaction = _db.Database.BeginTransaction())
			{
				try
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
					//if (model.CreateAccount)
					{
						var account = new CreateAccountVM();
						account.AccountName = model.Name;
						account.AccountNameAr = model.NameAR;
						account.AccTypeId = 14;
						account.BranchId = 1;
						account.CurrencyId = 1;
						NewSupplier.SupplierAccNum = _accountGenerator.CreateNewAccount(account);
					}
					_db.Contacts.Add(NewSupplier);
					await _db.SaveChangesAsync();
					transaction.Commit();
					return new ResponseDto() { Status = true, Body = _mapper.Map<SupplierDto>(NewSupplier), message = "تم تسجيل البيانات بنجاح" };
				}
				catch (Exception ex)
				{
					transaction.Rollback();
					return new ResponseDto() { Status = false, message = "خطأ في تسجيل البيانات", Body = ex };
				}
			}

		}


	}
}
