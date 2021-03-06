using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float _baseSpeed = 20.0f;
    float _gravidade = 9.8f;

    public static int pontos;

    CharacterController characterController;

    //Referência usada para a câmera filha do jogador
    GameObject playerCamera;
    //Utilizada para poder travar a rotação no angulo que quisermos.
    float cameraRotation;

    // public bool isGrounded;
    // Rigidbody rb;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        playerCamera = GameObject.Find("Main Camera");
        cameraRotation = 0.0f;

        pontos = 0;

        // rb = GetComponent<Rigidbody>();
        // jump = new Vector3(0.0f, 2.0f, 0.0f);
    }

    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        //Verificando se é preciso aplicar a gravidade
        float y = 0;
        if(!characterController.isGrounded){
            y = -_gravidade;
        } 
        
        //Tratando movimentação do mouse
        float mouse_dX = Input.GetAxis("Mouse X");
        float mouse_dY = -Input.GetAxis("Mouse Y");
        
        Vector3 direction = transform.right * x + transform.up * y + transform.forward * z;

        //Tratando a rotação da câmera
        cameraRotation += 3f * mouse_dY;
        Mathf.Clamp(cameraRotation, -75.0f, 75.0f);

        Vector3 velocity = new Vector3(direction.x * _baseSpeed, direction.y, direction.z * _baseSpeed);
        if (Input.GetKeyDown ("space")){
            velocity.y = 3 * _baseSpeed;
        } 
        characterController.Move(velocity * Time.deltaTime);
        transform.Rotate(Vector3.up, 3f * mouse_dX);

        playerCamera.transform.localRotation = Quaternion.Euler(cameraRotation, 0.0f, 0.0f);
    }

    void LateUpdate()
    {
        RaycastHit hit;
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward*10.0f, Color.magenta);
        if(Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, 100.0f))
        {
            Debug.Log(hit.collider.name);
        }
    }

    // void OnCollisionEnter (Collision col) 
    // {
    //     if (col.gameObject.CompareTag("bau"))
    //     {
    //         Debug.Log ("bau");
    //     }
    // }

    void OnTriggerEnter(Collider col)
    {
        if(col.tag == "bau")
        {
            pontos++;
            Destroy(col.gameObject);    
        }
    }
}