using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.SharedModels;
using TicketsHandling.Application.Features.Tickets.Command.EditTicket;

namespace TicketsHandling.Application.Features.Tickets.Command.CreateTicket
{
    public class CreateTicketCommandValidator : AbstractValidator<CreateTicketCommand>
    {
        private readonly LookupsData _lookupsData;

        public CreateTicketCommandValidator(IOptions<LookupsData> lookupsData)
        {
            _lookupsData = lookupsData.Value;

            // Id must be greater than 0
         
            RuleFor(x => x.Governorate)
                        .Must(BeValidGovernorate)
                        .WithMessage("No Selected Governorate or Invalid Governorate.");

            RuleFor(x => x.City)
                .Must(BeValidCity)
                .WithMessage("No Selected City or Invalid City.");

            RuleFor(x => x.District)
                .Must(BeValidDistrict)
                .WithMessage("No Selected District or Invalid District.");

            // Add role For Phone Number prop to be valid as egyptian phone number
            RuleFor(x => x.PhoneNumber)
                .Matches(@"^01[0125][0-9]{8}$")
                .WithMessage("Egyptian Phone Number is Invalid.");
        }
        private bool BeValidGovernorate(int governorateId)
        {
            return _lookupsData.Governorates.Any(g => g.Id == governorateId);
        }

        private bool BeValidCity(int cityId)
        {
            return _lookupsData.Cities.Any(c => c.Id == cityId);
        }

        private bool BeValidDistrict(int districtId)
        {
            return _lookupsData.Districts.Any(d => d.Id == districtId);
        }
    }
}
