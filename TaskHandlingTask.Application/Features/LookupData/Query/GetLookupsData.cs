using AutoMapper;
using Azure;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketsHandling.Application.Common.SharedModels;
using TicketsHandling.Application.Features.Tickets.Query;

namespace TicketsHandling.Application.Features.LookupData.Query
{
    //DTO
    public class LookupItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


    // Mapping
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<LookupItem, LookupItemDto>();
        }
    }
}
