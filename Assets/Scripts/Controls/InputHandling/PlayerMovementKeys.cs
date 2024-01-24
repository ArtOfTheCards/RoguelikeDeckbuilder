using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovementKeys : MonoBehaviour
{

    // Update is called once per frame
    private void Update()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W)) moveY = +1f;
        if (Input.GetKey(KeyCode.S)) moveY = -1f;
        if (Input.GetKey(KeyCode.A)) moveX = -1f;
        if (Input.GetKey(KeyCode.D)) moveX = +1f;

        Vector3 moveVector = new Vector3(moveX, moveY).normalized;
        GetComponent<IMoveVelocity>().SetVelocity(moveVector);


    }
}
