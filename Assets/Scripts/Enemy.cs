using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class Enemy : Agent
{   
    [SerializeReference] private Transform target;
    //enemy parameters
    [SerializeField]private int healt = 100;
    [SerializeField] private int currentHealt;
    [SerializeField]private int damage = 10;
    public float shootRange = 80f;
    public float shootRate = 1f;
    [SerializeField]private float shootTimer;
    [SerializeField]private float maxSpeed;
    public Transform shootOrigin;
    [SerializeReference] LineRenderer shootLine;

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.position);
        sensor.AddObservation(target.transform.position);
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveZ = actions.ContinuousActions[1];
        float moveVelocity= actions.ContinuousActions[2];

        transform.position += new Vector3(moveX, 0, moveZ).normalized * Time.deltaTime * (maxSpeed * moveVelocity);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("wall"))SetReward(-1f);
        else if (collision.transform.CompareTag("obstacle")) SetReward(-2f);
        else if (collision.transform.CompareTag("Player")) SetReward(-10f);
    }
    public int GetHealt()
    {
        return currentHealt;
    }
}
