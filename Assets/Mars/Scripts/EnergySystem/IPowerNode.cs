namespace PLu.Mars.EnergySystem
{
    public enum PowerNodeType
    {
        PowerProducer,
        PowerConsumer,
        PowerStorage
    }
    public interface IPowerNode
    {
        PowerNodeType PowerNodeType { get; }
        float NominalEffect { get; }

        // Returns the amount of energy in kWh the node produce or if negative consumes.
        float UpdateEnergyBalance(float updateInterval, float energyBalance = 0f);

        // Returns the amount of energy in kWh the node will consume (positive).
        float RequestedEnergy(float requestedEnergy)
        {
            return 0f;
        }
    }
}
