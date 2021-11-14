using LandingRocket.BusinessLogic.Interfaces;
using System;
using Xunit;

namespace LandingRocket.BusinessLogic.Tests
{
    public class LandingRocketServiceTests
    {
        private readonly ILandingRocketService _landingRocketService;

        public LandingRocketServiceTests()
        {
            _landingRocketService = new LandingRocketService();
            BusinessLogic.DataStorage.GlobalData.landingArea_X = 100;
            BusinessLogic.DataStorage.GlobalData.landingArea_Y = 100;
            BusinessLogic.DataStorage.GlobalData.landingPlatform_X = 10;
            BusinessLogic.DataStorage.GlobalData.landingPlatform_Y = 10;
            BusinessLogic.DataStorage.GlobalData.startingPosition_X = 5;
            BusinessLogic.DataStorage.GlobalData.startingPosition_Y = 5;
            BusinessLogic.DataStorage.GlobalData._landingArea = new int[BusinessLogic.DataStorage.GlobalData.landingArea_X, BusinessLogic.DataStorage.GlobalData.landingArea_Y];
            BusinessLogic.DataStorage.GlobalData.SetInitialValues();
        }

        [Fact]
        public async void CheckLandingArea_GivenCoordinationOutOfPlatform_ShouldReturnOutOfPlatform()
        {
            const string expectedValue = "out of platform";

            int x = 16, y = 10;

            var result = await _landingRocketService.LandingAvailability(x, y);

            Assert.Equal(expected: expectedValue,
                actual: result);
        }

        [Fact]
        public async void CheckLandingArea_GivenCoordinationWithinPlatform_ShouldReturnOkForLanding()
        {
            const string expectedValue = "ok for landing";

            int x = 5, y = 5;

            var result = await _landingRocketService.LandingAvailability(x, y);

            Assert.Equal(expected: expectedValue,
                actual: result);
        }

        [Fact]
        public async void CheckLandingArea_GivenCoordinationNextToPreviouoslyRequestedPosition_ShouldReturnClash()
        {
            const string expectedValue = "clash";
            await _landingRocketService.LandingAvailability(5, 5);
            int x = 5, y = 6;

            var result = await _landingRocketService.LandingAvailability(x, y);

            Assert.Equal(expected: expectedValue,
                actual: result);
        }

        [Fact]
        public async void CheckLandingArea_GivenCoordinationToPreviouslyRequestedPosition_ShouldReturnClash()
        {
            const string expectedValue = "clash";
            int x = 5, y = 5;
            await _landingRocketService.LandingAvailability(x, y);

            var result = await _landingRocketService.LandingAvailability(x, y);

            Assert.Equal(expected: expectedValue,
                actual: result);
        }

        [Fact]
        public async void CheckLandingArea_GivenCoordinationToPreviouslyRequestedPositionOneUniteSeparation_ShouldReturnOkForLanding()
        {
            const string expectedValue = "ok for landing";
            await _landingRocketService.LandingAvailability(5, 5);

            var result = await _landingRocketService.LandingAvailability(5, 7);

            Assert.Equal(expected: expectedValue,
                actual: result);
        }
    }
}
