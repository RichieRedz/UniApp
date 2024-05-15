

public static class StarRatingHelper
{
    // -------------------------- Star Rating Helper -------------------------//
    // StarRating extension method to generate a star rating from a Movies rating
    public static string StarRating(double averageRating)
    {
        // round the rating to the nearest 0.5
        double roundedRating = Math.Round(averageRating * 2, MidpointRounding.AwayFromZero) / 2;

        // create a string of stars to represent the rating
        string stars = "";
        for (int i = 0; i < 5; i++)
        {
            if (roundedRating - i >= 1)
            {
                stars += "<i class='bi bi-star-fill attention-text'></i>";
            }
            else if (roundedRating - i == 0.5)
            {
                stars += "<i class='bi bi-star-half attention-text'></i>";
            }
            else
            {
                stars += "<i class='bi bi-star text-muted'></i>";
            }
        }

        return stars;
    }
}