using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementKeys : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Get the movement input
        Vector2 input = GetInput();

        // Set animator parameters
        animator.SetFloat("X", input.x);
        animator.SetFloat("Y", input.y);

        // Set walking parameter based on input
        animator.SetBool("isWalking", input != Vector2.zero);

        // Set velocity
        GetComponent<IMoveVelocity>().SetVelocity(input);
    }

    private Vector2 GetInput()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(KeyCode.W))
            input.y = 1f;
        else if (Input.GetKey(KeyCode.S))
            input.y = -1f;

        if (Input.GetKey(KeyCode.A))
            input.x = -1f;
        else if (Input.GetKey(KeyCode.D))
            input.x = 1f;

        return input.normalized;
    }
}
