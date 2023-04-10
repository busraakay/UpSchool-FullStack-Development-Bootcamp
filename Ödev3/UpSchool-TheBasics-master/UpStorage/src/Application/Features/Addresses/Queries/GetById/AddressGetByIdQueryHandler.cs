
using Application.Common.Interfaces;
using Application.Features.Addresses.Queries.GetAll;
using Domain.Entities;
using Domain.Enums;
using Domain.Identity;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Application.Features.Addresses.Queries.GetById
{
    public class AddressGetByIdQueryHandler : IRequestHandler<AddressGetByIdQuery, AddressGetByIdDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public AddressGetByIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<AddressGetByIdDto> Handle(AddressGetByIdQuery request, CancellationToken cancellationToken)
        {
            
            var dbQuery = _applicationDbContext.Addresses.AsQueryable();

            dbQuery = dbQuery.Where(x => x.Id == request.Id);

            if (dbQuery == null)
            {
                throw new Exception();
            }

            if (request.IsDeleted.HasValue) dbQuery = dbQuery.Where(x => x.IsDeleted == request.IsDeleted.Value);

            dbQuery = dbQuery.Include(x => x.Country);
            dbQuery = dbQuery.Include(x => x.City);

            var addresses = await dbQuery.FirstAsync(cancellationToken);

            
            var addressDtos = MapAddressesToGetAllDtos(addresses);

            return addressDtos;
        }

        private AddressGetByIdDto MapAddressesToGetAllDtos(Address address)
        {
            AddressGetByIdDto addressGetAllDtos = new AddressGetByIdDto();

            addressGetAllDtos.Id = address.Id;
            addressGetAllDtos.CountryId = address.CountryId;
            addressGetAllDtos.CountryName = address.Country.Name;
            addressGetAllDtos.CityId = address.CityId;
            addressGetAllDtos.CityName = address.City.Name;
            addressGetAllDtos.District = address.District;
            addressGetAllDtos.PostCode = address.PostCode;
            addressGetAllDtos.AddressLine1 = address.AddressLine1;
            addressGetAllDtos.AddressLine2 = address.AddressLine2;
            addressGetAllDtos.AddressType = address.AddressType;
            addressGetAllDtos.Name = address.Name;
            addressGetAllDtos.UserId = address.UserId;
            addressGetAllDtos.IsDeleted = address.IsDeleted;
            return addressGetAllDtos;
        }
    }
}
