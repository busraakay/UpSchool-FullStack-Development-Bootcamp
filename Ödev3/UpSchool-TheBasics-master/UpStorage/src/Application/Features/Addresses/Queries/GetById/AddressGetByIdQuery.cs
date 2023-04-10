
using Domain.Identity;
using MediatR;

namespace Application.Features.Addresses.Queries.GetById
{
    public class AddressGetByIdQuery:IRequest<AddressGetByIdDto>
    {
        public Guid Id { get; set; }
        public bool? IsDeleted { get; set; }

        public AddressGetByIdQuery(Guid id, bool? ısDeleted)
        {
            Id = id;
            IsDeleted = ısDeleted;  
        }

    }
}
