using AutoMapper;
using Haram.Remittance.Currencies;
using Haram.Remittance.Customers;
using Haram.Remittance.Remittances;

namespace Haram.Remittance.Blazor;

public class RemittanceBlazorAutoMapperProfile : Profile
{
    public RemittanceBlazorAutoMapperProfile()
    {
        //Define your AutoMapper configuration here for the Blazor project.
        CreateMap<CurrencyDto, CreateUpdateCurrencyDto>().ReverseMap();
        CreateMap<CustomerDto, UpdateCustomerDto>().ReverseMap();
        CreateMap<CustomerDto, CreateCustomerDto>().ReverseMap();
        CreateMap<CreateUpdateRemittanceDto, RemittanceDto>().ReverseMap();

    }
}
