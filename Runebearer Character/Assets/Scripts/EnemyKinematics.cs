using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyKinematics : MonoBehaviour {
    [SerializeField] Transform target;
    private void faceTarget() {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookDir = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDir, Time.deltaTime * 10f);
    }

    // Update is called once per frame
    void Update()
    {
        faceTarget();
    }
}
