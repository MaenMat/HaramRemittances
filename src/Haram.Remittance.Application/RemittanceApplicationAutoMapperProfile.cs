using AutoMapper;
using Haram.Remittance.Currencies;
using Haram.Remittance.Customers;
using Haram.Remittance.Remittances;
using Haram.Remittance.Remmitances;

namespace Haram.Remittances;

public class RemittanceApplicationAutoMapperProfile : Profile
{
    public RemittanceApplicationAutoMapperProfile()
    {
        CreateMap<Currency, CurrencyDto>();
        CreateMap<CreateUpdateCurrencyDto, Currency>();

        CreateMap<Customer, CustomerDto>();
        CreateMap<UpdateCustomerDto, Customer>();

        CreateMap<CreateUpdateRemittanceDto, RemittanceClass>();
        CreateMap<RemittanceClass,RemittanceDto>();
        CreateMap<RemittanceStatus, RemittanceStatusDto>().ReverseMap();
        CreateMap<ReportRemittance,ReportRemittanceDto>().ReverseMap();

    }
}
