using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour {
    CharacterController controller;
    public float speed = 2f;
    public float rotateSpeed;
    public float turnSmoothTime = 0.1f;
    public Transform cam;
    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        controller = GetComponent<CharacterController>();
    }
    private void Update() {
        //Gets input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        //Takes care of player's rotation while moving
        //Moving depends on the camera angle - changeable
        if (direction.magnitude > 0.05f) {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, 
                targetAngle, ref rotateSpeed, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir*speed*Time.deltaTime);
        }
    }
}
