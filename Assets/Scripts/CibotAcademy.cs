using System.Collections;
using System.Collections.Generic;
using MLAgents;
using UnityEngine;

public class CibotAcademy : Academy
{
    GameObject[] enemies;
    GameObject player;

    GameObject[] resettables;

    public override void InitializeAcademy(){
        enemies = GameObject.FindGameObjectsWithTag("enemy");
        player = GameObject.FindGameObjectWithTag("player");
    }


    public override void AcademyReset()
    {
        Debug.Log("AcademyReset");
        foreach(GameObject enemy in enemies) { enemy.GetComponent<IResettable>().Reset(); }
        player.GetComponent<IResettable>().Reset();
    }
}
