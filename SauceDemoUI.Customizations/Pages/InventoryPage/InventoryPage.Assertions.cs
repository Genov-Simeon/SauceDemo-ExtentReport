using NUnit.Framework;

namespace SauceDemoUI.Customizations.Pages
{
    public partial class InventoryPage
    {
        public void AssertLoggedSuccessfully()
        {
            Assert.IsTrue(Driver.Url.Contains("inventory.html"));
        }        
    }
}
