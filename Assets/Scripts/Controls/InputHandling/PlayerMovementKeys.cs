using UnityEngine;
using UnityEngine.InputSystem;
using FMOD.Studio;

public class PlayerMovementKeys : MonoBehaviour
{

    [SerializeField] private KeyCode up;
    [SerializeField] private KeyCode down;
    [SerializeField] private KeyCode left;
    [SerializeField] private KeyCode right;
    [SerializeField] private Animator animator;

    private EventInstance playerFootsteps;

    private void Update()
    {
        // Get the movement input
        Vector2 input = GetInput();

        // Set animator parameters
        animator.SetFloat("X", input.x);
        animator.SetFloat("Y", input.y);

        // Set velocity
        GetComponent<IMoveVelocity>().SetVelocity(input);

        UpdateSound(input.magnitude);
    }

    private void Start()
    {
        playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.PlayerFootsteps);
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


    public void Rebindkeys(SettingData setting)
    {
        this.up = setting.up;
        this.down = setting.down;
        this.left = setting.left;
        this.right = setting.right;
    }

    private void UpdateSound(float speed)
    {

        if (speed != 0)
        {
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
        else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
}
