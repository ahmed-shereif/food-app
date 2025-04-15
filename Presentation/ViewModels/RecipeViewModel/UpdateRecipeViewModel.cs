namespace Presentation.ViewModels.RecipeViewModel
{
    public record UpdateRecipeViewModel(int id, string Name, string ImageUrl, decimal Price, string Description, string Tag, string Category);
    
}
