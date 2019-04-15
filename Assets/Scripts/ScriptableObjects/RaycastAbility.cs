using UnityEngine;
using System.Collections;

[CreateAssetMenu (menuName = "Abilities/RaycastAbility")]
public class RaycastAbility : Ability {

    public int gunDamage = 1;
    public float weaponRange = 50f;
    public float hitForce = 100f;
    public Color laserColor = Color.white;

    public override void RunAbility(Transform rayOrigin) {
        Debug.DrawRay (rayOrigin.position, rayOrigin.forward * weaponRange, Color.green);
        
        //Declare a raycast hit to store information about what our raycast has hit.
        RaycastHit hit;

        //Start our ShotEffect coroutine to turn our laser line on and off
        StartCoroutine(ShotEffect());
        
        //Set the start position for our visual effect for our laser to the position of gunEnd
        laserLine.SetPosition(0, gunEnd.position);
        
        //Check if our raycast has hit anything
        if (Physics.Raycast(rayOrigin,fpsCam.transform.forward, out hit, weaponRange))
        {
            //Set the end position for our laser line 
            laserLine.SetPosition(1, hit.point);
            
            //Get a reference to a health script attached to the collider we hit
            Shootable health = hit.collider.GetComponent<Shootable>();
            
            //If there was a health script attached
            if (health != null)
            {
                //Call the damage function of that script, passing in our gunDamage variable
                health.Damage (gunDamage);
            }
            
            //Check if the object we hit has a rigidbody attached
            if (hit.rigidbody != null)
            {
                //Add force to the rigidbody we hit, in the direction it was hit from
                hit.rigidbody.AddForce (-hit.normal * hitForce);
            }
        }
        else
        {
            //if we did not hit anything, set the end of the line to a position directly away from
            laserLine.SetPosition(1, fpsCam.transform.forward * weaponRange);
        }
    }
}