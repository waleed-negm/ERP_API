using Application.BusinessLogic.CRM.Interfaces;
using Application.BusinessLogic.CRM.Model;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.Interfaces;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;
using Application.Common.DTOs;
using AutoMapper;
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

		public new async Task<ResponseDto> GetAllAsync() =>
			new ResponseDto()
			{
				Body = _mapper.Map<List<SupplierDto>>(await _db.Contacts.Where(x => x.IsSupplier == true).ToListAsync()),
				Status = true,
				message = "تم تسجيل البيانات بنجاح"
			};

		public async Task<ResponseDto> AddAsync(SupplierDto model)
		{
			using (var transaction = _db.Database.BeginTransaction())
			{
				try
				{
					var NewSupplier = _mapper.Map<Contacts>(model);
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
				catch
				{
					transaction.Rollback();
					return new ResponseDto() { Status = false, message = "خطأ في تسجيل البيانات" };
				}
			}

		}


	}
}
