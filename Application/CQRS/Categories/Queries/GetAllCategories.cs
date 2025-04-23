using Application.DTOS.CategoriesDto;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using AutoMapper.QueryableExtensions;
using Domain.Enums;
using Domain.Models;
using Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Categories.Queries
{

    public record GetAllCategoriesQuery() : IRequest<ResponseViewModel<IEnumerable<GetAllCategoriesDto>>>;
    public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, ResponseViewModel<IEnumerable<GetAllCategoriesDto>>>
    {
        private readonly IGeneralRepository<Category> _generalRepo;

        public GetAllCategoriesQueryHandler(IGeneralRepository<Category> generalRepo)
        {
            _generalRepo = generalRepo;
        }

        public Task<ResponseViewModel<IEnumerable<GetAllCategoriesDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _generalRepo.GetAll();
            if (categories is null)
            {
                return Task.FromResult(ResponseViewModel<IEnumerable<GetAllCategoriesDto>>.Failure(
                    null,
                    "Categories not found.",
                    ErrorCodeEnum.NotFound));
            }

            var mappedCategories = categories
                .ProjectTo<GetAllCategoriesDto>(AutoMapperService.Mapper.ConfigurationProvider)
                .ToList();

            return Task.FromResult(ResponseViewModel<IEnumerable<GetAllCategoriesDto>>.Success(
                mappedCategories,
                "Categories retrieved successfully."));
        }
    }
}