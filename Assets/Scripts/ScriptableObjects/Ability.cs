using UnityEngine;

public abstract class Ability : ScriptableObject {
    public float EnergyCost = 1f;
    public virtual void Initialize() {}
    public virtual bool CanRun(FloatVariable energyPool) {
        return energyPool.RuntimeValue > EnergyCost * Time.deltaTime;
    }

}