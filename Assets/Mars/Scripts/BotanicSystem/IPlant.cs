using PLu.Mars.AtmosphereSystem;
 
namespace PLu.Mars.BotanicSystem
{
    public interface IPlant : IGasNode
    {
        float LeafArea { get; }
        void UpdatePlant(float updateInterval);
    }
}
