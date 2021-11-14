using System;
using System.Collections.Generic;
using System.Text;

namespace LandingRocket.BusinessLogic.DataStorage
{
    public static class GlobalData
    {
        public static int[,] _landingArea;

        public static int landingArea_X;
        public static int landingArea_Y;
        public static int landingPlatform_X;
        public static int landingPlatform_Y;
        public static int startingPosition_X = 5;
        public static int startingPosition_Y = 5;

        public static void SetInitialValues()
        {
            for (int x = 0; x < landingArea_X; x++)
                for (int y = 0; y < landingArea_Y; y++)
                {
                    if (x >= startingPosition_X && x < startingPosition_X + landingPlatform_X &&
                        y >= startingPosition_Y && y < startingPosition_Y + landingPlatform_Y)
                        _landingArea[x, y] = 1;
                    else if (x < startingPosition_X || x >= startingPosition_X + landingPlatform_X
                        || y < startingPosition_Y || y >= startingPosition_Y + landingPlatform_Y)
                        _landingArea[x, y] = 2;
                    else
                        _landingArea[x, y] = 0;
                }
        }
    }
}
