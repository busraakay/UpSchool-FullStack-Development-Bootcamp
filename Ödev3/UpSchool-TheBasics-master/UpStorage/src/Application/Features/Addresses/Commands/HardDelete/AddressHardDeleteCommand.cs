using Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Addresses.Commands.Delete
{
    public class AddressHardDeleteCommand : IRequest<Response<string>>
    {
        public Guid Id { get; set; }
    }
}
