public class PlayerDataModel
{
    public int Damage { get; private set; }
    public int HealthPoints { get; private set; }
    public int Defense { get; private set; }
    public float LifeSteal { get; private set; }
    public float CriticalStrikeChance { get; private set; }
    public float AttackSpeed { get; private set; }
    public float MovementSpeed { get; private set; }
    public float Luck { get; private set; }

    public PlayerDataModel(int damage,
        int healthPoints,
        int defense,
        float lifeSteal,
        float criticalStrikeChance,
        float attackSpeed,
        float movementSpeed,
        float luck)
    {
        ChangeDamage(damage);
        ChangeHealthPoints(healthPoints);
        ChangeDefense(defense);
        ChangeLifeSteal(lifeSteal);
        ChangeCriticalStrikeChance(criticalStrikeChance);
        ChangeAttackSpeed(attackSpeed);
        ChangeMovementSpeed(movementSpeed);
        ChangeLuck(luck);
    }

    public void ChangeDamage(int newDamage) => this.Damage = newDamage;
    public void ChangeHealthPoints(int newHealthPoints) => this.HealthPoints = newHealthPoints;
    public void ChangeDefense(int newDefense) => this.Defense = newDefense;
    public void ChangeLifeSteal(float newLifeSteal) => this.LifeSteal = newLifeSteal;
    public void ChangeCriticalStrikeChance(float newCriticalStrikeChance) => this.CriticalStrikeChance = newCriticalStrikeChance;
    public void ChangeAttackSpeed(float newAttackSpeed) => this.AttackSpeed = newAttackSpeed;
    public void ChangeMovementSpeed(float newMovementSpeed) => this.MovementSpeed = newMovementSpeed;
    public void ChangeLuck(float newLuck) => this.Luck = newLuck;
}
