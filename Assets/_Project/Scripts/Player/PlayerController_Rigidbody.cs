using Unity.VisualScripting;
using UnityEngine;

public class PlayerController_Rigidbody : MonoBehaviour
{
    private Rigidbody rb;
    private Vector2 input;

    private float force = 5f;
    private GameControls controls;



    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        controls = new GameControls();
    }


    // Le action vanno abilitate e disabilitate insieme al componente.
    private void OnEnable() => controls.Player.Enable();
    private void OnDisable() => controls.Player.Disable();
    private void OnDestroy() => controls.Dispose();



    private void Update()       // ogni frame: leggo
    {
        input = controls.Player.Move.ReadValue<Vector2>();
    }



    private void FixedUpdate()  // 50 volte al secondo: muovo
    {
        // dinamico: una spinta
        Vector3 input3 = new Vector3(input.x, 0f, input.y);  // non metto time.deltatime
        Vector3 velocity = input3 * force;
        //rb.AddForce(velocity); //AddForce con rigidbody non kinematic

        rb.MovePosition(rb.position + velocity * force * Time.fixedDeltaTime);//MovePosition con rigidbody kinematic
        
        
        
        
        
        // cinematico: decido io dove si trova (come Projectile)
        // rb.MovePosition(rb.position
        //     + velocity * Time.fixedDeltaTime);
    }




}
