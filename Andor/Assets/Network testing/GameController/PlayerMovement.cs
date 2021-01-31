using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class PlayerMovement : MonoBehaviour
{
    private PhotonView PV;
    private CharacterController CC;
    public float movementSpeed;
    public float rotationSpeed; 

    void Start(){
        PV = GetComponent<PhotonView>();
        CC = GetComponent<CharacterController>();
    }
    
    void Update(){
        if(PV.IsMine == true)
        {
            basicMovement();

        }


    }

    void basicMovement(){
        if(Input.GetKey(KeyCode.W)){
            CC.Move(transform.forward* Time.deltaTime * movementSpeed);
        }
        if(Input.GetKey(KeyCode.A)){
            CC.Move(transform.right* Time.deltaTime * movementSpeed);
        }
        
        if(Input.GetKey(KeyCode.S)){
            CC.Move( - transform.forward* Time.deltaTime * movementSpeed);
        }

        if(Input.GetKey(KeyCode.D)){
            CC.Move(-transform.right* Time.deltaTime * movementSpeed);
        }

    }
}
