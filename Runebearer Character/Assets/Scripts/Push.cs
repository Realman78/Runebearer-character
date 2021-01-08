using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Push : MonoBehaviour
{
    public float multiplier = 1f;
    public Transform body;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<CharacterController>().enabled = false;
            other.gameObject.GetComponent<Rigidbody>().AddForce(body.forward * multiplier, ForceMode.Impulse);
        }
    }
}
