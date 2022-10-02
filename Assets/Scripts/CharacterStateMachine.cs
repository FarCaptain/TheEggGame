using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateMachine : MonoBehaviour
{
    [SerializeField] private SimpleCharacterMovement characterMovement;

    private CharacterState currentState = CharacterState.IDLE;

    // Timer
    private float accumulatedTime = 0f;
    private float timeGoal = 0f;
    private bool timerStarted = false;

    private bool isMoving = false;

    private void Update()
    {
        UpdateTimer();

        switch (currentState)
        {
            case CharacterState.IDLE:
                if (!timerStarted)
                {
                    float idleTime = Random.Range(1.5f, 8.5f);
                    StartTimer(idleTime);
                }
                else if(HitTime())
                {
                    SwitchState(CharacterState.MOVING);
                }
                break;
            case CharacterState.MOVING:
                if(!isMoving)
                {
                    characterMovement.MoveToRandomPositon(12f);
                    isMoving = true;
                }
                else if(characterMovement.agent.velocity == Vector3.zero)
                {
                    SwitchState(CharacterState.IDLE);
                }
                break;
            case CharacterState.CLICKED:
                break;
            default:
                break;
        }
    }

    private void SwitchState(CharacterState state)
    {
        ResetTimer();
        isMoving = false;
        currentState = state;
    }

    #region Timer
    private void ResetTimer()
    {
        accumulatedTime = 0f;
        timeGoal = 0f;
        timerStarted = false;
    }

    private void StartTimer(float _timeGoal)
    {
        timerStarted = true;
        timeGoal = _timeGoal;
    }

    private void UpdateTimer()
    {
        if (timerStarted)
            accumulatedTime += Time.deltaTime;
    }

    private bool HitTime()
    {
        if (timerStarted && accumulatedTime > timeGoal)
            return true;

        return false;
    }
    #endregion
}

public enum CharacterState
{
    IDLE,
    MOVING,
    CLICKED
}
