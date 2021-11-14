using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LandingRocket.BusinessLogic.Interfaces
{
    public interface ILandingRocketService
    {
        public Task<string> LandingAvailability(int landing_X, int landing_Y);
    }
}
