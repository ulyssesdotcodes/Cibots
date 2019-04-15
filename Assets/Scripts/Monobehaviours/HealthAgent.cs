using UnityEngine;

public class HealthAgent : MonoBehaviour {
    public FloatVariable Health;

    public void Damage(float damage) {
        Health.RuntimeValue -= damage;
    }
}