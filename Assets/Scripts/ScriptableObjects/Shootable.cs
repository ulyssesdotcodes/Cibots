using UnityEngine;

public class Shootable : MonoBehaviour {
    public FloatVariable Health;

    public void Damage(float damage) {
        Health.RuntimeValue -= damage;
    }
}