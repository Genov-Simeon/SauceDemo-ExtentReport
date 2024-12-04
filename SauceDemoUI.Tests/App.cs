using Unity;

namespace SauceDemoUI.Tests
{
    public static class App
    {
        static App()
        {
            Container = new UnityContainer();
        }

        public static IUnityContainer Container { get; set; }
    }
}
