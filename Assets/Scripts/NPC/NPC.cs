using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIType
{
    Passive,
    Scared,
    Aggressive
}

public enum AIState
{
    Idle,
    Wandering,
    Attacking,
    Fleeing
}
public class NPC : MonoBehaviour
{

    [Header("stats")]
    public int health;
    public float walkSpeed;
    public float runSpeed;
    public ItemData[] dropOnDeath;

    [Header("AI")]
    public AIType aiType;
    private AIState aiState;
    public float detectDistance;
    public float safeDistance;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    [Header("Combat")]
    public int damage;
    public float attackRate;
    public float lastAttackTime;
    public float attackDistance;

    private float playerDistance;

    // components
    private NavMeshAgent agent;
    private Animator anim;
    private SkinnedMeshRenderer[] meshRenderers;

    private void Awake()
    {
        //get components
        agent = GetComponent<NavMeshAgent>();
        //anim = GetComponentInChildren<Animator>();
        //meshRenderers = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    private void Update()
    {

        // get player distance
        playerDistance = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
        switch (aiState)
        {
            case AIState.Idle:
                PassiveUpdate(); 
                break;
            case AIState.Wandering:
                PassiveUpdate();
                break;
            case AIState.Attacking:
                AttackingUpdate();
                break;
            case AIState.Fleeing:
                FleeingUpdate();
                break;
        }
    }

    void PassiveUpdate()
    {

    }

    void AttackingUpdate()
    {

    }

    void FleeingUpdate()
    {

    }

    void SetState(AIState newState)
    {
        aiState = newState;

        switch(aiState)
        {
            case AIState.Idle:
                {
                    agent.speed = walkSpeed;
                    agent.isStopped = true;
                    break;
                }
            case AIState.Wandering:
                {
                    agent.speed = walkSpeed;
                    agent.isStopped = false;
                    WanderToNewLocation();
                    break;
                }
            case AIState.Attacking:
                {
                    agent.speed = runSpeed;
                    break;
                }
            case AIState.Fleeing:
                {
                    agent.speed = runSpeed;
                    break;
                }
        }
    }

    void WanderToNewLocation()
    {
        if(aiState != AIState.Idle)
        {
            return;
        }

    }

}
