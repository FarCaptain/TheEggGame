using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

public class SimpleCharacterMovement : MonoBehaviour
{
    public bool movementEnabled = true;

    public Animator animator;
    public NavMeshAgent agent;
    public float inputHoldDelay = 0.5f;
    public float turnSpeedThreshold = 0.5f;
    public float speedDampTime = 0.1f;
    public float slowingSpeed = 0.175f;
    public float turnSmoothing = 15f;

    private Vector3 destinationPosition;
    private bool facingRight = true;
    private const float stopDistanceProportion = 0.1f;
    private const float navMeshSampleDistance = 4f;
    private readonly int hashSpeedPara = Animator.StringToHash("Speed");


    private void Start()
    {
        agent.updateRotation = false;

        //agent.Warp(transform.position);

        destinationPosition = transform.position;
    }

    private void OnAnimatorMove()
    {
        agent.velocity = animator.deltaPosition / Time.deltaTime;
    }

    private void Update()
    {
        float speed = 0f;

        if (agent.pathPending)
        {
            return;
        }

        speed = agent.desiredVelocity.magnitude;

        if (agent.remainingDistance <= agent.stoppingDistance * stopDistanceProportion)
        {
            Stopping(out speed);
        }
        else if (agent.remainingDistance <= agent.stoppingDistance)
        {
            Slowing(out speed, agent.remainingDistance);
        }
        else if (speed > turnSpeedThreshold)
        {
            Moving();
        }

        animator.SetFloat(hashSpeedPara, speed, speedDampTime, Time.deltaTime);
    }

    private void Stopping(out float speed)
    {
        agent.isStopped = true;
        //snap to the position
        var pos = transform.position;
        transform.position = new Vector3(destinationPosition.x, pos.y, destinationPosition.z);
        speed = 0f;
    }

    private void Slowing(out float speed, float distanceToDestination)
    {
        agent.isStopped = true;

        float proportionalDistance = 1f - distanceToDestination / agent.stoppingDistance;

        Quaternion targetRotation = transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, proportionalDistance);
        transform.position = Vector3.MoveTowards(transform.position, destinationPosition, slowingSpeed * Time.deltaTime);
        speed = Mathf.Lerp(slowingSpeed, 0f, proportionalDistance);
    }

    private void Moving()
    {
        var vel = agent.velocity;
        if((vel.x > 0.01f && !facingRight) || (vel.x < -0.01f && facingRight))
            Flip();
    }

    #region FunctionCalls
    public void OnGroundClick(BaseEventData data)
    {
        if (!movementEnabled)
            return;

        PointerEventData pData = (PointerEventData)data;
        MoveTo(pData.pointerCurrentRaycast.worldPosition);
    }

    public void MoveTo(Vector3 position)
    {
        if (!movementEnabled)
            return;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(position, out hit, navMeshSampleDistance, NavMesh.AllAreas))
        {
            destinationPosition = hit.position;
        }
        else
        {
            // donesn't hit mesh, try finding something nearby
            destinationPosition = position;
        }

        // only trigger pathfinding once, then the speed is mine to change!
        agent.SetDestination(destinationPosition);
        agent.isStopped = false;
    }

    public void SimpleMoveTo(Vector3 position)
    {
        Debug.Log($"position {position}");
        agent.SetDestination(position);
        Debug.Log($"destnation {agent.destination}");
        agent.isStopped = false;
    }
    
    public void CalculateSpeed(float distance, float time)
    {
        agent.speed = distance / time;
        //Debug.Log($"{distance}/{time}={navMeshAgent.speed}");
    }
    
    public void SetPosition(Vector3 position)
    {
        GetComponent<Transform>().position = position;
    }

    public void StopMovement()
    {
        movementEnabled = false;
    }

    public void ResumeMovement()
    {
        movementEnabled = true;
    }

    private void Flip()
    {
        // Switch the way the player is labelled as facing.
        facingRight = !facingRight;

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    public void MoveToRandomPositon(float radius)
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        MoveTo(finalPosition);
    }

    #endregion

}
