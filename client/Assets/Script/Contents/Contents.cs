public static class Contents {
    public const int MAX_RECIPE_CUP = 2;
    public const int MAX_RECIPE_STRAW = 3;

    public const int MAX_RECIPE_OREDER = 3;

    public const int MAX_CUSTOMER_COUNT = 3;
}

public static class RecipeColor {
    public const int RED = 0;
    public const int GREEN = 1;
    public const int BLUE = 2;
    public const int WHITE = 3;
    public const int BLACK = 4;

    // can return color
    public static int TotalColor(int color1, int color2) {
        if (color1 == color2) {
            return color1;
        }

        return color1 + color2;
    }

    public static int MaxColor(int addColor) {
        int maxColor = WHITE + addColor;
        return maxColor + maxColor - 1;
    }
}
