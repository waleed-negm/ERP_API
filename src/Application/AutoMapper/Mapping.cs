using Application.BusinessLogic.CRM.ViewModel;
using Application.BusinessLogic.CurrentAssetModules.Inventory.ViewModel;
using Application.BusinessLogic.CurrentLiabilitiesModules.NotesPayableModule.ViewModel;
using Application.BusinessLogic.GeneralLedgerModule.AccountCharts.ViewModel;
using Application.BusinessLogic.PurchasesModule.ViewModel;
using Application.BusinessLogic.PurchasesModule.ViewModel.Expenses;
using Application.Common.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Persistence.Extenstions;

namespace Application.AutoMapper
{
	public class Mapping : Profile
	{
		public Mapping()
		{
			CreateMap<CreateAccountVM, AccountChart>();

			CreateMap<AccountChart, AccountListVM>()
				.ForMember(dest => dest.AccTypeName, src => src.MapFrom(x => x.AccType.AccountType))
				.ForMember(dest => dest.CurrencyAbbr, src => src.MapFrom(x => x.Currency.CurrencyAbbrev))
				.ForMember(dest => dest.BranchName, src => src.MapFrom(x => x.Branch.BranchName));

			CreateMap<AccountChart, UpdateAccountVM>();

			CreateMap<ContactCreatingViewModel, Contacts>();

			CreateMap<SupplierDto, Contacts>().ReverseMap();

			CreateMap<StoreItemCreationVM, StoreItem>();

			CreateMap<PurchaseSummary, Purchase>()
				.ForMember(dest => dest.PurchaseDate, src => src.MapFrom(y => y.PurchaseDate.ToEgyptionDate()));

			CreateMap<NotesPayableCreationVM, NotesPayable>()
				.ForMember(dest => dest.WritingDate, src => src.MapFrom(y => y.WritingDate.ToEgyptionDate()))
				.ForMember(dest => dest.DueDate, src => src.MapFrom(y => y.DueDate.ToEgyptionDate()));

			CreateMap<NotesPayable, NPDetails>()
				.ForMember(dest => dest.CurrencyAbbrev, src => src.MapFrom(y => y.Currency.CurrencyAbbrev))
				.ForMember(dest => dest.SupplierName, src => src.MapFrom(y => y.Supplier.Name))
				.ForMember(dest => dest.DueDate, src => src.MapFrom(y => y.DueDate.ToString("dd/MM/yyyy")))
				.ForMember(dest => dest.WritingDate, src => src.MapFrom(y => y.WritingDate.ToString("dd/MM/yyyy")))
				.ForMember(dest => dest.BankAccountName, src => src.MapFrom(y => y.BankAccount.AccountName));

			CreateMap<ExpenseDetailsVM, ExpenseSummary>()
				.ForMember(dest => dest.ExpenseDate, src => src.MapFrom(y => y.ExpenseDate.ToEgyptionDate()));
		}
	}
}
