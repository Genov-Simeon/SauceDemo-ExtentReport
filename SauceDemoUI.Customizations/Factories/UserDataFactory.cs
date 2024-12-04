using SauceDemoUI.Customizations.Faker;
using SauceDemoUI.Customizations.Models;

namespace SauceDemoUI.Customizations.Factories
{
    public static class UserDataFactory
    {
        public static UserData Generate()
        {
            return new UserDataFaker().Generate();
        }
    }
}
