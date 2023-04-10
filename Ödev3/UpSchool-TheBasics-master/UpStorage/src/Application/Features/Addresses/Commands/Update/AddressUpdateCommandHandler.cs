using Application.Common.Interfaces;
using Application.Features.Addresses.Commands.Add;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Commands.Update
{
    public class AddressUpdateCommandHandler : IRequestHandler<AddressUpdateCommand, Response<Guid>>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressUpdateCommandHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Response<Guid>> Handle(AddressUpdateCommand request, CancellationToken cancellationToken)
        {
            var entity = await _applicationDbContext.Addresses
            .FindAsync(new object[] { request.Id }, cancellationToken);

            if (entity == null)
            {
                throw new Exception();
            }

            entity.Name = request.Name;
            entity.UserId= request.UserId;
            entity.CountryId = request.CountryId;
            entity.CityId = request.CityId;
            entity.District = request.District;
            entity.PostCode= request.PostCode;
            entity.AddressLine1= request.AddressLine1;
            entity.AddressLine2= request.AddressLine2;

            entity.ModifiedByUserId = null;//aslında olmalı
            entity.ModifiedOn = DateTimeOffset.Now;


            await _applicationDbContext.SaveChangesAsync(cancellationToken);
            return new Response<Guid>($"The new address named \"{entity.Name}\" was successfully updated.", entity.Id);

        }
    }
}
