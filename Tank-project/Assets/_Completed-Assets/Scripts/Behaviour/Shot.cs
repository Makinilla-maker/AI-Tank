using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to move towards the given goal using a NavMeshAgent.
    /// </summary>
    [Action("Shot")]
    [Help("Moves with wander")]
    public class Shot : GOAction
    {
        ///<value>Input target game object towards this game object will be moved Parameter.</value>
        [InParam("blueCannon")]
        [Help("blueCannon")]
        [SerializeField] private Transform blueCannon;

        [InParam("redCannon")]
        [Help("redCannon")]
        [SerializeField] private Transform redCannon;
    
        [InParam("m_Shell")]
        [Help("m_Shell")]
        public Rigidbody m_Shell;   
        [InParam("range")]
        [Help("range")]
        public float range;
        [InParam("frq")]
        [Help("frq")]
        public float frq;
        [InParam("count")]
        [Help("count")]
        int count;

        /// <summary>Initialization Method of MoveToGameObject.</summary>
        /// <remarks>Check if GameObject object exists and NavMeshAgent, if there is no NavMeshAgent, the default one is added.</remarks>
        public override void OnStart()
        {
            count = 0;
            frq = 2;
        }

        /// <summary>Method of Update of MoveToGameObject.</summary>
        /// <remarks>Verify the status of the task, if there is no objective fails, if it has traveled the road or is near the goal it is completed
        /// y, the task is running, if it is still moving to the target.</remarks>
        Vector3 wanderTarget = Vector3.zero;
        public override TaskStatus OnUpdate()
        {
             if(count == 0)
        {
            frq -= 0.5f * Time.deltaTime;
            if(frq <= 0)
            {
                frq += 2;
                Reload();
            }    
                
        }
        else{
            if(gameObject.tag == "RedCannon")
            {
                RaycastHit hit;
                if(Physics.Raycast(redCannon.position, redCannon.forward, out hit, range))
                {
                   Debug.Log(hit.transform.tag);

                   if(hit.transform.tag == "BlueTank" || hit.transform.tag == "BlueCannon")
                   {
                       Debug.DrawLine(gameObject.transform.position,hit.transform.position,Color.red);
                     ShootBullet();
                     count = 0;
                 }
            }
        
            }
            else
            {
                RaycastHit hit2;
                if(Physics.Raycast(blueCannon.position, blueCannon.forward, out hit2, range))
                {
                    Debug.Log(hit2.transform.tag);

                    if(hit2.transform.tag == "RedTank" || hit2.transform.tag == "RedCannon")
                    {
                        Debug.DrawLine(gameObject.transform.position,hit2.transform.position,Color.blue);
                        ShootBullet();
                        count = 0;
                    }
                }
            }
        }
            return TaskStatus.RUNNING;
        }
        /// <summary>Abort method of MoveToGameObject </summary>
        /// <remarks>When the task is aborted, it stops the navAgentMesh.</remarks>
        public override void OnAbort()
        {

        }
    
    void ShootBullet()
    {
         Rigidbody shellInstance =
                GameObject.Instantiate (m_Shell, gameObject.transform.position, gameObject.transform.rotation) as Rigidbody;

            // Set the shell's velocity to the launch force in the fire position's forward direction.
            //shellInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward; 
    }
    void Reload()
    {
        count = 1;
    }
    }

}
