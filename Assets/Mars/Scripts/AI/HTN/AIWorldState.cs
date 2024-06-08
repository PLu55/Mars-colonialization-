namespace PLu.Mars.AI.HTN
{
    public enum AIWorldState
    {
        HasIngredient,
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
        Resource,
        Workshop,
        Enemy
    }
}