using UnityEngine;

public abstract class Ability : ScriptableObject {
    public FloatVariable EnergyPool;
    public float EnergyCost = 1f;
    public bool Active = false;

    public virtual void Initialize() {}

    public virtual void OnTick(Transform origin) {
        if(Active && EnergyPool.RuntimeValue > EnergyCost * Time.deltaTime) {
            EnergyPool.RuntimeValue -= EnergyCost * Time.deltaTime;
            RunAbility(origin);
        }
    }

    public abstract void RunAbility(Transform origin);
}