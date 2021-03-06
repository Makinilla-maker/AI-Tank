using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a perception condition to check if the objective is close depending on a given distance.
    /// </summary>
    [Condition("Is Tank Near Blue")]
    [Help("Checks whether the enemy tank is near")]
    public class IsTankNearBlue : GOCondition
    {
        ///<value>Input Target Parameter to to check the distance.</value>
        [InParam("Tank")]
        [Help("Target to check the distance")]
        public GameObject tank;
 
        ///<value>Input maximun distance Parameter to consider that the target is close.</value>
        [InParam("closeDistance")]
        [Help("The maximun distance to consider that the target is close")]
        float closeDistance;

        [InParam("RedCannon")]
        [Help("RedCannon")]
        public Transform redCannon;

        [InParam("BlueCannon")]
        [Help("BlueCannon")]
        public Transform blueCannon;

        /// <summary>
        /// Checks whether a target is close depending on a given distance,
        /// calculates the magnitude between the gameobject and the target and then compares with the given distance.
        /// </summary>
        /// <returns>True if the magnitude between the gameobject and de target is lower that the given distance.</returns>
        public override bool Check()
        {
            RaycastHit hit;
            if(Physics.Raycast(blueCannon.position, blueCannon.forward, out hit, closeDistance))
            {
                //Debug.Log(hit.transform.tag);
                Debug.DrawLine(blueCannon.position, hit.point, Color.blue);

                if(hit.transform.tag == "RedTank" || hit.transform.tag == "RedCannon")
                {
                    //Debug.Log("I gocha u homie");
                    return true;
                }
                else
                {
                    return false;
                }
                
		    }
            else
            {
                return false;
            }
        }
    }
}

