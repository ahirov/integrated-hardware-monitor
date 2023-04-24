using System.Threading;
using System.Windows;

using FluentAssertions;

using IntegratedHardwareMonitor.Bar.IoC;
using IntegratedHardwareMonitor.Core.Entities;
using IntegratedHardwareMonitor.Windows;

using NSubstitute;

namespace IntegratedHardwareMonitor.Tests
{
    [Apartment(ApartmentState.STA)]
    public class Tests
    {
        private Window _window;

        [SetUp]
        public void Setup()
        {
            IBarWindowDependencies dependencies = Substitute.For<IBarWindowDependencies>();
            dependencies.SettingProvider.Setting.Returns(Setting.GetDefault());
            _window = new MainWindow(null, dependencies);
        }

        [Test]
        public void MainWindow_Initialization_HasContent()
        {
            _window.HasContent.Should().BeTrue();
        }
    }
}
