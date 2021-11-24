using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Conditions
{
    /// <summary>
    /// It is a perception condition to check if the objective is close depending on a given distance.
    /// </summary>
    [Condition("DoIHaveBullets?")]
    [Help("Checks the number of bullets and decides to go to the reload point or not")]
    public class BulletsCounter : GOCondition
    {
        /// <summary>
        /// Checks whether a target is close depending on a given distance,
        /// calculates the magnitude between the gameobject and the target and then compares with the given distance.
        /// </summary>
        /// <returns>True if the magnitude between the gameobject and de target is lower that the given distance.</returns>
        public override bool Check()
        {
            if(GameObject.Find("Soviet Tank").GetComponent<BulletManager>().bullets.Count > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}



