using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to move towards the given goal using a NavMeshAgent.
    /// </summary>
    [Action("Shot Blue")]
    [Help("Cannon shots")]
    public class ShotBlue : GOAction
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

        [InParam("count")]
        [Help("count")]
        [SerializeField] private int count;

        
        public override void OnStart()
        {
            
        }

       
        public override TaskStatus OnUpdate()
        {
            count = GameObject.Find("Dictator Tank").GetComponent<BulletManager>().bullets.Count;
            if(count > 0)
            {
                RaycastHit hit;
                if(Physics.Raycast(blueCannon.position, blueCannon.forward, out hit, range))
                {
                    if(hit.transform.tag == "RedTank" || hit.transform.tag == "RedCannon")
                    {
                        if (!GameObject.Find("CompleteShell Blue 1(Clone)"))
                        {
                            ShootBulletBlue();
                            GameObject.Find("Dictator Tank").GetComponent<BulletManager>().bullets.RemoveAt(0);
                        }
                    }
                }
            }
            return TaskStatus.RUNNING;
        }
       
        public override void OnAbort()
        {

        }
    
        void ShootBulletBlue()
        {
            float distX = Mathf.Abs((enemyTank.position.x - blueCannon.position.x));
            float distY = enemyTank.position.y - blueCannon.position.y;
            float distZ = Mathf.Abs((enemyTank.position.z - blueCannon.position.z));

            Vector2 distXZ = new Vector2(distX, distZ);
            float norm = distXZ.magnitude;
            Debug.Log("Blue distance " + norm);

            float vel = 18f;

            float insideRoot = Mathf.Abs(Mathf.Pow(vel, 4) - (Physics.gravity.y * (Physics.gravity.y * Mathf.Pow(norm, 2)) + (2 * distY * Mathf.Pow(vel, 2))));
            float tan = (Mathf.Pow(vel, 2) + Mathf.Sqrt(insideRoot)) / (Physics.gravity.y * norm);
            float angle = Mathf.Atan(Mathf.Abs(tan));
            Debug.Log("Blue " + angle * Mathf.Rad2Deg);

            float velY = vel * Mathf.Sin(angle);
            float velZ = vel * Mathf.Cos(angle);

            Vector3 launchVel = new Vector3(0f, velY, velZ);

            Rigidbody shellInstance = GameObject.Instantiate(m_Shell, blueCannon.transform.position, blueCannon.transform.rotation) as Rigidbody;
            launchVel = shellInstance.transform.TransformDirection(launchVel);
            shellInstance.AddForce(launchVel, ForceMode.Impulse);
            
        }
    }
}
