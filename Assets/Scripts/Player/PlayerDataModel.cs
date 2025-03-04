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
        SetDamage(damage);
        SetHealthPoints(healthPoints);
        SetDefense(defense);
        SetLifeSteal(lifeSteal);
        SetCriticalStrikeChance(criticalStrikeChance);
        SetAttackSpeed(attackSpeed);
        SetMovementSpeed(movementSpeed);
        SetLuck(luck);
    }

    public void SetDamage(int newDamage) => this.Damage = newDamage;
    public void SetHealthPoints(int newHealthPoints) => this.HealthPoints = newHealthPoints;
    public void SetDefense(int newDefense) => this.Defense = newDefense;
    public void SetLifeSteal(float newLifeSteal) => this.LifeSteal = newLifeSteal;
    public void SetCriticalStrikeChance(float newCriticalStrikeChance) => this.CriticalStrikeChance = newCriticalStrikeChance;
    public void SetAttackSpeed(float newAttackSpeed) => this.AttackSpeed = newAttackSpeed;
    public void SetMovementSpeed(float newMovementSpeed) => this.MovementSpeed = newMovementSpeed;
    public void SetLuck(float newLuck) => this.Luck = newLuck;

    public void ModifyDamage(int newDamage) => this.Damage += newDamage;
    public void ModifyHealthPoints(int newHealthPoints) => this.HealthPoints += newHealthPoints;
    public void ModifyDefense(int newDefense) => this.Defense += newDefense;
    public void ModifyLifeSteal(float newLifeSteal) => this.LifeSteal += newLifeSteal;
    public void ModifyCriticalStrikeChance(float newCriticalStrikeChance) => this.CriticalStrikeChance += newCriticalStrikeChance;
    public void ModifyAttackSpeed(float newAttackSpeed) => this.AttackSpeed += newAttackSpeed;
    public void ModifyMovementSpeed(float newMovementSpeed) => this.MovementSpeed += newMovementSpeed;
    public void ModifyLuck(float newLuck) => this.Luck += newLuck;
}
