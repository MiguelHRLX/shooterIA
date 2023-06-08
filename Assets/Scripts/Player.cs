using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Shooters
{
    // Start is called before the first frame update
    RaycastHit screenHit;
    private Vector3 moveDirection;
    [SerializeReference] Rigidbody playerRigidBody;
    [SerializeField]private GameObject marc;
    private void Awake()
    {
        shootTimer += shootRate;
    }

    void Start()
    {
       
    }
    // Update is called once per frame
    void Update()
    {
        Movement();
        TrigerObjetive();
        Debug.DrawRay(shootOrigin.position, shootOrigin.right * 100f, Color.black);
        if (shootTimer<shootRate)shootTimer += Time.deltaTime;
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shot();
            shootTimer = 0;
        }
    }
    private void TrigerObjetive()
    {   Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(cameraRay.origin, (cameraRay.direction * 100), Color.cyan);
        if (Physics.Raycast(cameraRay,out screenHit,200f))
        {
            if(marc)marc.transform.position = screenHit.point;
            Vector3 DistancePlayerObjetive = screenHit.point-transform.position;
            Vector3 shootDirection = new Vector3(DistancePlayerObjetive.x, 0, DistancePlayerObjetive.z);
            transform.right = shootDirection;
        }
    }
    private void Movement()
    {
        moveDirection = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical")).normalized;
        if (moveDirection.magnitude >= 0.5) playerRigidBody.velocity = moveDirection * velocity;
        else if (moveDirection.magnitude < 0.5) playerRigidBody.velocity = Vector3.zero;
    }

}
