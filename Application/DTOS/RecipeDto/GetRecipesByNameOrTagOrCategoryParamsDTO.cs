namespace Application.DTOS.RecipeDto
{
    public class GetRecipesByNameOrTagOrCategoryParamsDTO
    {
        public string Name { get; set; }
        public string Tag { get; set; }
        public string Category { get; set; }
    }
}