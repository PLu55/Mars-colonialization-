namespace PLu.Mars.AI.HTN
{
    public enum AIWorldState
    {
        HasJob,
        HasResourceInSight,
        HasEnemyInSight,
        HasEnemy,
        HasEnemyInMeleeRange,
        HasReceivedDamage,
        Stamina,
    }

    public enum AIDestinationTarget
    {
        None,
        PickupLocation,
        DropoffLocation,
        Resource,
        Workshop,
        Enemy
    }
}