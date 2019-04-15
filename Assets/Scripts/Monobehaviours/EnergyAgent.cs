using UnityEngine;

public class EnergyAgent : MonoBehaviour {
    public FloatVariable EnergyPool;
    public float RegenAmount = 1f;

    void Update() {
        EnergyPool.RuntimeValue += RegenAmount * Time.deltaTime;
    }

    public void UseAbility(Ability ability) {
        EnergyPool.RuntimeValue -= ability.EnergyCost * Time.deltaTime;
        Debug.Log(EnergyPool.RuntimeValue);
    }
}