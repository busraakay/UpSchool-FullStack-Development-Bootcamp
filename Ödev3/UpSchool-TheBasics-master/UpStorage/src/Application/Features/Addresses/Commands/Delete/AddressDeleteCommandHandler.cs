using Application.Common.Interfaces;
using Domain.Common;
using MediatR;

namespace Application.Features.Addresses.Commands.Delete
{
    public class AddressDeleteCommandHandler : IRequestHandler<AddressDeleteCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressDeleteCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<Guid>> Handle(AddressDeleteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Addresses
            .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new Exception();
            }

            entity.IsDeleted = true;
            entity.DeletedOn= DateTimeOffset.Now;
            entity.DeletedByUserId = null;//aslında olmalı
            entity.ModifiedByUserId= null;
            entity.ModifiedOn = null;

            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return new Response<Guid>($"The address \"{entity.Name}\" was successfully deleted.", entity.Id);

        }
    }
}
