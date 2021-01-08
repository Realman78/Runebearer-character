using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanScript : MonoBehaviour {
    public float health = 100;
    public float damage = 10;
    public float attack_speed = 0.5f;
    public float armor = 5;
    public float mana = 100;

    public void takeDamage(float damage) {
        float actualDamage = damage - armor;
        health -= actualDamage;
        if (health <= 0) {
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {

    }
}
