using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;

[RequireComponent(typeof(RaycastShooter))]
[RequireComponent(typeof(EnergyAgent))]
[RequireComponent(typeof(HealthAgent))]
[RequireComponent(typeof(RayPerception3D))]
[RequireComponent(typeof(Rigidbody))]
public class D3TAgent : Agent
{
    public float InitialHealth;
    public float InitialEnergy;
    public float MoveSpeed = 3f;
    public float TurnSpeed = 30f;
    Rigidbody agentRb;
    HealthAgent HealthAgent;
    EnergyAgent EnergyAgent;
    RaycastShooter RaycastShooter;
    private RayPerception3D rayPer;
    // Start is called before the first frame update
    public override void InitializeAgent()
    {
        base.InitializeAgent();
        agentRb = GetComponent<Rigidbody>();
        rayPer = GetComponent<RayPerception3D>();
        HealthAgent = GetComponent<HealthAgent>();
        EnergyAgent = GetComponent<EnergyAgent>();
        RaycastShooter = GetComponent<RaycastShooter>();

        FloatVariable health = ScriptableObject.CreateInstance<FloatVariable>();
        health.InitialValue = InitialHealth;
        health.RuntimeValue = InitialHealth;
        HealthAgent.Health = health;

        FloatVariable energy = ScriptableObject.CreateInstance<FloatVariable>();
        energy.InitialValue = InitialEnergy;
        energy.RuntimeValue = InitialEnergy;
        EnergyAgent.EnergyPool = energy;
    }

    public override void CollectObservations() {
        float rayDistance = 50f;
        float[] rayAngles = { 20f, 90f, 160f, 45f, 135f, 70f, 110f };
        string[] detectableObjects = { "agent", "wall", "badBanana", "frozenAgent" };
        AddVectorObs(rayPer.Perceive(rayDistance, rayAngles, detectableObjects, 0f, 0f));
        Vector3 localVelocity = transform.InverseTransformDirection(agentRb.velocity);
        AddVectorObs(localVelocity.x);
        AddVectorObs(localVelocity.z);
        AddVectorObs(HealthAgent.Health.RuntimeValue);
        AddVectorObs(EnergyAgent.EnergyPool.RuntimeValue);
    }

    public override void AgentAction(float[] vectorAction, string textAction) {
        if(Mathf.Clamp(vectorAction[2], -1, 1) > 0.5f) {
            RaycastShooter.Fire();
        }

        Vector3 dirToGo = Vector3.zero;
        Vector3 rotateDir = Vector3.zero;
        if (brain.brainParameters.vectorActionSpaceType == SpaceType.continuous)
        {
            dirToGo = transform.forward * Mathf.Clamp(vectorAction[0], 0f, 1f);
            rotateDir = transform.up * Mathf.Clamp(vectorAction[1], -1f, 1f);
        } else {
            var forwardAxis = (int)vectorAction[0];
            var rotateAxis = (int)vectorAction[1];
            var shootAxis = (int)vectorAction[2];
            
            switch (forwardAxis)
            {
                case 1:
                    dirToGo = transform.forward;
                    break;
                case 2:
                    dirToGo = -transform.forward;
                    break;
            }

            switch (rotateAxis)
            {
                case 1:
                    rotateDir = -transform.up;
                    break;
                case 2:
                    rotateDir = transform.up;
                    break; 
            }
            switch (shootAxis)
            {
                case 1:
                    RaycastShooter.Fire();
                    break;
            }
        }

        agentRb.AddForce(dirToGo * MoveSpeed, ForceMode.VelocityChange);
        transform.Rotate(rotateDir, Time.fixedDeltaTime * TurnSpeed);
    }
}
