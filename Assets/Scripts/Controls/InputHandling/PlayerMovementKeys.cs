using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementKeys : MonoBehaviour
{

    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private Animator animator;

    private void Update()
    {
        // Get the movement input
        Vector2 input = GetInput();

        // Set animator parameters
        animator.SetFloat("X", input.x);
        animator.SetFloat("Y", input.y);        

        // Set velocity
        GetComponent<IMoveVelocity>().SetVelocity(input);
    }

    private Vector2 GetInput()
    {
        Vector2 input = Vector2.zero;

        if (Input.GetKey(up))
            input.y = 1f;
        else if (Input.GetKey(down))
            input.y = -1f;

        if (Input.GetKey(left))
            input.x = -1f;
        else if (Input.GetKey(right))
            input.x = 1f;

        return input.normalized;
    }


    public void Rebindkeys(SettingData setting){
        this.up = setting.up;
        this.down = setting.down;
        this.left =  setting.left;
        this.right = setting.right;
    }
}
