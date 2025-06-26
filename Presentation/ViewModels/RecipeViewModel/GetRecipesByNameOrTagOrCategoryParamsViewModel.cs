namespace Presentation.ViewModels.RecipeViewModel
{
    public class GetRecipesByNameOrTagOrCategoryParamsViewModel
    {
        public string? Name { get; set; }
        public string? Tag { get; set; }
        public string? Category { get; set; }
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
