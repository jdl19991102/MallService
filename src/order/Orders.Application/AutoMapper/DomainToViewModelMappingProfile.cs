namespace Orders.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<OrdersInfo, OrdersViewModel>();   
        }
    }
}
