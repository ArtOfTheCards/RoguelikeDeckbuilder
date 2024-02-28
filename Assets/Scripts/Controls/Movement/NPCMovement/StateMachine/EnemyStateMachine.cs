using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyStateMachine
{
    
    
    private enum States { IDLE, SEARCH, CHASE, ATTACK }
    private States currentState { get; set; }


    // Idle Components

    // Search Components


    // Chase Components

    // Attack Components



    public EnemyStateMachine()
    {
        
    }

    #region State Manipulation

    public void Initialize()
    {
        currentState = States.IDLE;
        EnterState();
    }
    public void EnterState()
    {
        Debug.Log("Entering the " + currentState + " State");
        switch (currentState)
        {
            case States.IDLE:
                
                break;
            case States.SEARCH:
                
                break;
            case States.CHASE:
                
                break;
            case States.ATTACK:
                
                break;
            default:
                break;
        }


    }

    public void ExitState()
    {
        switch (currentState)
        {
            case States.IDLE:
                break;
            case States.SEARCH:
                break;
            case States.CHASE:
                break;
            case States.ATTACK:
                break;
            default:
                break;
        }
    }

    public void FrameUpdate()
    {
        switch (currentState)
        {
            case States.IDLE:
                Debug.Log("Current State Idle");
                HandleIdleFrameUpdate();
                break;
            case States.SEARCH:
                Debug.Log("Current State Search");
                HandleSearchFrameUpdate();
                break;
            case States.CHASE:
                Debug.Log("Current State Chase");
                HandleChaseFrameUpdate();
                break;
            case States.ATTACK:
                Debug.Log("Game Over");
                break;
            default:
                break;
        }


    }


    public void AnimationTriggerEvent()
    {


    }

    #endregion

    #region HandleFrameUpdate Methods
    private void HandleIdleFrameUpdate()
    {
       
    }

    private void HandleSearchFrameUpdate()
    {
    
    }

    private void HandleChaseFrameUpdate()
    {

    }
    #endregion

    private void ChangeState(States newState)
    {
        ExitState();
        currentState = newState;
        EnterState();
    }

    private void DisplayTime(float timeInIdleState)
    {
        float minutes = Mathf.FloorToInt(timeInIdleState / 60);
        float seconds = Mathf.FloorToInt(timeInIdleState % 60);
        Debug.Log(minutes + ":" + seconds);
    }
}