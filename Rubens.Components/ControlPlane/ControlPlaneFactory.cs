using Rubens.Components.Configuration;

namespace Rubens.Components.ControlPlane
{
    public static class ControlPlaneFactory
    {
        public static IControlPlane Create(RubensConfiguration configuration)
        {
            if (configuration.UseInMemoryBus)
            {
                return new InMemoryControlPlane();
            }

            return new ControlPlane(configuration);
        }
    }
}