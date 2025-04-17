namespace Application.DTOS.FavoriteDtos
{
    public class GetRecipeUserFavoritesDtoRecipeDto
    {
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string Category { get; set; }
    }
}