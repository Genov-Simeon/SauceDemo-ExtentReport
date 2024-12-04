using SauceDemoUI.Customizations.Models;

namespace SauceDemoUI.Customizations.Factories
{
    public static class UserInfoFactory
    {
        public static UserInfo BuildUserCredentials(string username, string password)
        {
            var userinfo = new UserInfo()
            {
                UserName = Environment.GetEnvironmentVariable(username)
            ?? throw new InvalidOperationException($"{username} is not set in environment variables."),

                Password = Environment.GetEnvironmentVariable(password)
            ?? throw new InvalidOperationException($"{password} is not set in environment variables.")
            };

            return userinfo;
        }
    }
}
