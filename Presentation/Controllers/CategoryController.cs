using Application.CQRS.Categories.Queries;
using Application.CQRS.Recipes.Queries;
using Application.Helpers;
using Application.Helpers.MappingProfile;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Presentation.ViewModels.CategoriesViewModel;
using Presentation.ViewModels.RecipeViewModel;

namespace Presentation.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseViewModel<IEnumerable<GetAllCategroiesViewModel>>> GetAllCategories()
        {
            var result = await _mediator.Send(new GetAllCategoriesQuery());

            if (!result.IsSuccess || result.Data is null)
            {

                return ResponseViewModel<IEnumerable<GetAllCategroiesViewModel>>.Failure(null, result.Message, result.StatusCode);
            }



            var mappedData = result.Data.Map<IEnumerable<GetAllCategroiesViewModel>>();
            return ResponseViewModel<IEnumerable<GetAllCategroiesViewModel>>.Success(mappedData, "Success");
        }

    }
}
