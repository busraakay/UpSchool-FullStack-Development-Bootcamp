using Application.Common.Interfaces;
using Domain.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Addresses.Commands.Delete
{
    public class AddressHardDeleteCommandHandler : IRequestHandler<AddressHardDeleteCommand, Response<string>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressHardDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<string>> Handle(AddressHardDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Addresses
            .Where(x => x.Id == request.Id)
            .SingleOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new Exception();
            }

            _applicationDbContext.Addresses.Remove(entity);

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<string>($"The address was successfully deleted.");

        }
    }
}
