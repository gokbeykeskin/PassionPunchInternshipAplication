using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private CharacterController characterController;
    private Animator animator;

    private Vector3 moveInput;
    private Vector3 moveVelocity;
    public Camera mainCamera;
    [SerializeField] private float moveSpeed = 300;
    [SerializeField] private float turnSpeed;
    public void Awake()
    {
       characterController = GetComponent<CharacterController>();
       animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        var movement = new Vector3(horizontal,0,vertical) * moveSpeed * Time.deltaTime;
        characterController.SimpleMove(movement);
        animator.SetFloat("Speed",movement.magnitude);

         moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
         moveVelocity = moveInput * moveSpeed;
 
         Ray cameraRay = mainCamera.ScreenPointToRay(Input.mousePosition);
         Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
         float rayLength;
 
         if (groundPlane.Raycast(cameraRay, out rayLength))
         {
             Vector3 pointToLook = cameraRay.GetPoint(rayLength); 
             transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
         }
         
         mainCamera.transform.position = new Vector3(transform.position.x,6.0f,transform.position.z); //kameranın oyuncuyu takip etmesi için

         

    }
}
