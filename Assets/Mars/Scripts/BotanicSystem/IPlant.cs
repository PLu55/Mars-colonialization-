using PLu.Mars.AtmosphereSystem;
 
namespace PLu.Mars.BotanicSystem
{
    public interface IPlant : IAtmosphereNode
    {
        float LeafArea { get; }
        void UpdatePlant(float updateInterval);
    }
}
