using UnityEngine;

public static class Contents {
    public const int MAX_RECIPE_CUP = 3;

    public const int MAX_COLOR = 5;
    public const int MIN_COLOR = 2;

    public const int MAX_RECIPE_DECO = 3;

    public const int MAX_RECIPE_OREDER = 3;

    public const int MAX_CUSTOMER_COUNT = 3;

    public const int MIN_ENEMY_HP = 3;
    public const int MAX_ENEMY_HP = 7;

    public const int PENALTY_ENEMY_HP = 40;

    public const int MAX_SPAWN_ENEMY_COUNT = 4;

    public static int GetScoreByTime(float percent) {
        if (percent >= 0.8f) {
            return 250;
        }
        else if (percent >= 0.5f) {
            return 100;
        }
        else if (percent >= 0.1f) {
            return 25;
        }
        else if (percent >= 0f) {
            return 10;
        }
        return 0;
    }

    public static int GetMoneyByTime(float percent) {
        if (percent >= 0.8f) {
            return 10;
        }
        else if (percent >= 0.5f) {
            return 5;
        }
        else if (percent >= 0.1f) {
            return 3;
        }
        else if (percent >= 0f) {
            return 1;
        }
        return 0;
    }

    public static void ShuffleList<T>(System.Collections.Generic.List<T> list) {
        int random1;
        int random2;

        T tmp;

        for (int index = 0; index < list.Count; ++index) {
            random1 = Random.Range(0, list.Count);
            random2 = Random.Range(0, list.Count);

            tmp = list[random1];
            list[random1] = list[random2];
            list[random2] = tmp;
        }
    }
}

public static class ColorCode {
    public const int RED = 1;
    public const int BLUE = 2;
    public const int YELLOW = 4;
    public const int WHITE = 8;
    public const int BLACK = 12;

    public static int TotalColor(int color1, int color2) {
        if (color1 == color2) {
            return color1;
        }

        return color1 + color2;
    }

    public static int RandomColor(int colorCount = 2) {
        return TotalColor(IndexOf(Random.Range(0, colorCount)), IndexOf(Random.Range(0, colorCount)));
    }

    public static int IndexOf(int index) {
        switch (index) {
            case 0:
                return RED;
            case 1:
                return BLUE;
            case 2:
                return YELLOW;
            case 3:
                return WHITE;
            case 4:
                return BLACK;
        }
        return 0;
    }

    public static int GetIndex(int color) {
        switch (color) {
            case RED:
                return 0;
            case BLUE:
                return 1;
            case YELLOW:
                return 2;
            case WHITE:
                return 3;
            case BLACK:
                return 4;
        }
        return 0;
    }
}

public static class LevelValue {
    public static int GetMaxCupAndDecoCount(int count) {
        if (count >= 16)
            return Contents.MAX_RECIPE_CUP;
        else
            return Contents.MAX_RECIPE_CUP - 1;
    }

    public static int GetMaxColorCount(int count) {
        if (count >= 51)
            return Contents.MAX_COLOR;
        else if (count >= 31)
            return Contents.MAX_COLOR - 1;
        else if (count >= 6)
            return Contents.MAX_COLOR - 2;
        else
            return Contents.MAX_COLOR - 3;
    }

    public static int GetMaxCustomerCount(float second) {
        if (second >= 61f)
            return Contents.MAX_CUSTOMER_COUNT;
        else if (second >= 31f)
            return Contents.MAX_CUSTOMER_COUNT - 1;
        else
            return Contents.MAX_CUSTOMER_COUNT - 2;
    }

}
