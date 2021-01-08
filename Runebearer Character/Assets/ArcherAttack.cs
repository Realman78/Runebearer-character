using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ArcherAttack : MonoBehaviour
{
    [SerializeField] Transform player;
    bool shooting = false;
    NavMeshAgent agent;
    [SerializeField] GameObject arrow;
    public float shootForce = 50f;
    [SerializeField] Transform bowPosition;
    bool canShoot = true;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    IEnumerator Shoot() {
        while (Vector3.Distance(player.position, transform.position) < 10f && canShoot) {
            GameObject projectile = Instantiate(arrow, bowPosition.position, Quaternion.identity);
            projectile.GetComponent<Rigidbody>().AddForce(bowPosition.forward * shootForce);
            canShoot = false;
            yield return new WaitForSeconds(3f);
            Destroy(projectile);
            canShoot = true;
        }
        
    }

    private void faceTarget() {
        Vector3 direction = (player.position - transform.position).normalized;
        Quaternion lookDir = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, Time.deltaTime * 10f);
    }
    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, transform.position) < 10f && !shooting) {
            shooting = true;
            agent.enabled = false;
            StartCoroutine(Shoot());
        }else if (Vector3.Distance(player.position, transform.position) < 20f) {
            shooting = false;
            agent.enabled = true;
            agent.destination = player.position;
        } else {
            agent.enabled = false;
        }
        faceTarget();
    }
}
