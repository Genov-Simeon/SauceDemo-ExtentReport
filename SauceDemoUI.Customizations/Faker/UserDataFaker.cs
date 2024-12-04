using Bogus;
using SauceDemoUI.Customizations.Models;

namespace SauceDemoUI.Customizations.Faker
{
    public class UserDataFaker : Faker<UserData>
    {
        public UserDataFaker()
        {
            RuleFor(user => user.FirstName, faker => faker.Person.FirstName.Replace('\'', 'a'));
            RuleFor(user => user.LastName, faker => faker.Person.LastName.Replace('\'', 'a'));
            RuleFor(user => user.PostalCode, faker => faker.Address.ZipCode().Replace('\'', '0'));
        }
    }
}
