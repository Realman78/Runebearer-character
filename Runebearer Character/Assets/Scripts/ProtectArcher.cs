using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProtectArcher : MonoBehaviour
{
    public Transform player;
    public Transform archer;
    private Vector3 newPos;
    [SerializeField] float speed = 1f;
    public Transform nose;
    private bool followPlayer = false;
    private bool charging = false;
    float archerHealth, maxArcherHealth; 

    public float imp = 1f;

    private void Start() {
        archerHealth = archer.GetComponent<HumanScript>().health;
        maxArcherHealth = archerHealth;
    }

    IEnumerator Protect(float multiplier) {
        while (Vector3.Distance(newPos, transform.position) > Mathf.Epsilon) {
            transform.position = Vector3.MoveTowards(transform.position, newPos, Time.deltaTime*speed*multiplier);
            yield return new WaitForEndOfFrame();
        }
        followPlayer = true;
    }
    IEnumerator Charge(float charge_speed) {
        charging = true;
        while (Vector3.Distance(player.position, transform.position) > 1f) {
            transform.position = Vector3.MoveTowards(transform.position, player.position, Time.deltaTime * charge_speed);
            yield return new WaitForEndOfFrame();
        }
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<Rigidbody>().AddForce((transform.forward + new Vector3(0, .0001f, 0)) * imp * Time.deltaTime);
        followPlayer = true;
    }

 /*   private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player")) {
            other.GetComponent<CharacterController>().enabled = false;
            other.GetComponent<Rigidbody>().AddForce((transform.forward + new Vector3(0,.0001f,0)) * imp);
            //StartCoroutine(LaunchPlayer());
            //When the anim is over, turn char c back on
        }
    }*/

    IEnumerator LaunchPlayer() {
        int i = 0;
        while (i < 100) {
            player.Translate(transform.forward*Time.deltaTime*20f);
            yield return new WaitForEndOfFrame();
            i++;
        }
        player.GetComponent<CharacterController>().enabled = true;
    }

    private bool willCharge() {
        archerHealth = archer.GetComponent<HumanScript>().health;
        int num = Random.Range(1, 101);
        if (archerHealth < maxArcherHealth * 0.8 && archerHealth > maxArcherHealth * 0.5) {
            return num <= 40;
        }else if (archerHealth < maxArcherHealth * 0.5 && archerHealth > maxArcherHealth * 0.25) {
            return num <= 80;
        } else if (archerHealth < maxArcherHealth * 0.25) {
            return num <= 95;
        }
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && !followPlayer) {
            float x = (archer.position.x + player.position.x) / 2;
            float z = (archer.position.z + player.position.z) / 2;

            newPos = new Vector3(x, transform.position.y, z);

            //Make angry animation before running up to protect

            //80 -40 ; 50 - 80; 25 - 95

            //melee range - charge push
            if (Vector3.Distance(player.position,archer.position) < 5f && willCharge()) {
                StartCoroutine(Charge(5f));
            } else {
                if (Vector3.Distance(newPos, transform.position) > Mathf.Epsilon && willCharge())
                    StartCoroutine(Protect(.5f));
            }
        }

        if (followPlayer) {
            float x = (archer.position.x + player.position.x) / 2;
            float z = (archer.position.z + player.position.z) / 2;

            newPos = new Vector3(x, transform.position.y, z);
            StartCoroutine(Protect(.002f));
        }
    }
}
