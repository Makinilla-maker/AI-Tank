using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to move towards the given goal using a NavMeshAgent.
    /// </summary>
    [Action("Shot Red")]
    [Help("Cannon shots")]
    public class ShotRed : GOAction
    {
        ///<value>Input target game object towards this game object will be moved Parameter.</value>
        [InParam("blueCannon")]
        [Help("blueCannon")]
        [SerializeField] private Transform blueCannon;

        [InParam("redCannon")]
        [Help("redCannon")]
        [SerializeField] private Transform redCannon;

        [InParam("enemyTank")]
        [Help("enemyTank")]
        [SerializeField] private Transform enemyTank;

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
        [SerializeField] private int count;

        /// <summary>Initialization Method of MoveToGameObject.</summary>
        /// <remarks>Check if GameObject object exists and NavMeshAgent, if there is no NavMeshAgent, the default one is added.</remarks>
        public override void OnStart()
        {
            frq = 10f;
        }

        /// <summary>Method of Update of MoveToGameObject.</summary>
        /// <remarks>Verify the status of the task, if there is no objective fails, if it has traveled the road or is near the goal it is completed
        /// y, the task is running, if it is still moving to the target.</remarks>
        public override TaskStatus OnUpdate()
        {
            count = GameObject.Find("Soviet Tank").GetComponent<BulletManager>().bullets.Count;
            if(count > 0)
            {
                RaycastHit hit;
                if(Physics.Raycast(redCannon.position, redCannon.forward, out hit, range))
                {
                    if(hit.transform.tag == "BlueTank" || hit.transform.tag == "BlueCannon")
                    {
                        frq -= 0.1f;
                        if (frq <= 0)
                        {
                            ShootBulletRed();
                            GameObject.Find("Soviet Tank").GetComponent<BulletManager>().bullets.RemoveAt(0);
                            frq += 10f;
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
    
        void ShootBulletRed()
        {
            float distX = Mathf.Abs((enemyTank.position.x - redCannon.position.x));
            float distY = enemyTank.position.y - redCannon.position.y;
            float distZ = Mathf.Abs((enemyTank.position.z - redCannon.position.z));

            Vector2 distXZ = new Vector2(distX, distZ);
            float norm = distXZ.magnitude;
            Debug.Log("Red distance " + norm);

            float vel = 18f;

            float insideRoot = Mathf.Abs(Mathf.Pow(vel, 4) - (Physics.gravity.y * (Physics.gravity.y * Mathf.Pow(norm, 2)) + (2 * distY * Mathf.Pow(vel, 2))));
            float tan = (Mathf.Pow(vel, 2) + Mathf.Sqrt(insideRoot)) / (Physics.gravity.y * norm);
            float angle = Mathf.Atan(Mathf.Abs(tan));
            Debug.Log("Red " + angle * Mathf.Rad2Deg);

            float velY = vel * Mathf.Sin(angle);
            float velZ = vel * Mathf.Cos(angle);

            Vector3 launchVel = new Vector3(0f, velY, velZ);

            Rigidbody shellInstance = GameObject.Instantiate(m_Shell, redCannon.transform.position, redCannon.transform.rotation) as Rigidbody;
            launchVel = shellInstance.transform.TransformDirection(launchVel);
            shellInstance.AddForce(launchVel, ForceMode.Impulse);
            
        }
    }
}
