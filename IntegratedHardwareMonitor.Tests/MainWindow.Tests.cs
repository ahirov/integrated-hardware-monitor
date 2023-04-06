using System.Windows;

using FluentAssertions;

namespace IntegratedHardwareMonitor.Tests
{
    [Apartment(ApartmentState.STA)]
    public class Tests
    {
        private Window _window;

        [SetUp]
        public void Setup()
        {
            _window = new MainWindow();
        }

        [Test]
        public void MainWindow_Initialization_HasContent()
        {
            _window.HasContent.Should().BeTrue();
        }
    }
}
