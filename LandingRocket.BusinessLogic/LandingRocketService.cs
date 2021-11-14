using LandingRocket.BusinessLogic.Interfaces;
using System;
using System.Threading.Tasks;

namespace LandingRocket.BusinessLogic
{
    public class LandingRocketService : ILandingRocketService
    {
        private async Task<bool> PlatformCheck(int x, int y)
        {
            if (x >= BusinessLogic.DataStorage.GlobalData.landingArea_X || x < 0 || y >= BusinessLogic.DataStorage.GlobalData.landingArea_Y || y < 0)
                return false;
            if (x >= BusinessLogic.DataStorage.GlobalData.startingPosition_X && x < BusinessLogic.DataStorage.GlobalData.startingPosition_X + BusinessLogic.DataStorage.GlobalData.landingPlatform_X &&
                       y >= BusinessLogic.DataStorage.GlobalData.startingPosition_Y && y < BusinessLogic.DataStorage.GlobalData.startingPosition_Y + BusinessLogic.DataStorage.GlobalData.landingPlatform_Y)
                return true;
            return false;
        }

        private async Task<bool> ClashCheck(int x, int y)
        {
            return BusinessLogic.DataStorage.GlobalData._landingArea[x, y] == 0 || BusinessLogic.DataStorage.GlobalData._landingArea[x,y] == 2 ||
               BusinessLogic.DataStorage.GlobalData._landingArea[x + 1, y] == 0 || BusinessLogic.DataStorage.GlobalData._landingArea[x - 1, y] == 0 || BusinessLogic.DataStorage.GlobalData._landingArea[x, y + 1] == 0 || BusinessLogic.DataStorage.GlobalData._landingArea[x, y - 1] == 0 ||
               BusinessLogic.DataStorage.GlobalData._landingArea[x - 1, y - 1] == 0 || BusinessLogic.DataStorage.GlobalData._landingArea[x + 1, y + 1] == 0;

        }

        public async Task<string> LandingAvailability(int landing_X, int landing_Y)
        {
            if (!await PlatformCheck(landing_X, landing_Y))
                return ReturnedModel.ReturnedOptions.outOfPlatform;

            if(!await ClashCheck(landing_X,landing_Y))
            {
                BusinessLogic.DataStorage.GlobalData._landingArea[landing_X, landing_Y] = 0;
                BusinessLogic.DataStorage.GlobalData._landingArea[landing_X+1, landing_Y] = 2;
                BusinessLogic.DataStorage.GlobalData._landingArea[landing_X-1, landing_Y] = 2;
                BusinessLogic.DataStorage.GlobalData._landingArea[landing_X, landing_Y+1] = 2;
                BusinessLogic.DataStorage.GlobalData._landingArea[landing_X, landing_Y-1] = 2;
                BusinessLogic.DataStorage.GlobalData._landingArea[landing_X+1, landing_Y+1] = 2;
                BusinessLogic.DataStorage.GlobalData._landingArea[landing_X-1, landing_Y-1] = 2;
                return ReturnedModel.ReturnedOptions.Ok;
            }
            return ReturnedModel.ReturnedOptions.clash;
        }
    }
}
