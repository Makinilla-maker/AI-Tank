using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to move towards the given goal using a NavMeshAgent.
    /// </summary>
    [Action("Reload Trip Red")]
    [Help("Moves to a specifical position to perform the reload")]
    public class ReloadTripRed : GOAction
    {
        ///<value>Input target game object towards this game object will be moved Parameter.</value>
        [InParam("target")]
        [Help("Target game object towards this game object will be moved")]
        public GameObject target;

        [InParam("agent")]
        [Help("agent")]
        private UnityEngine.AI.NavMeshAgent agent;

        float freq = 3f;
        /// <summary>Initialization Method of MoveToGameObject.</summary>
        /// <remarks>Check if GameObject object exists and NavMeshAgent, if there is no NavMeshAgent, the default one is added.</remarks>
        public override void OnStart()
        {
            if (target == null)
            {
                Debug.LogError("The movement target of this game object is null", gameObject);
                return;
            }

            agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent == null)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                agent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
            }

            #if UNITY_5_6_OR_NEWER
                        agent.isStopped = false;
            #else
                            navAgent.Resume();
            #endif
        }

        /// <summary>Method of Update of MoveToGameObject.</summary>
        /// <remarks>Verify the status of the task, if there is no objective fails, if it has traveled the road or is near the goal it is completed
        /// y, the task is running, if it is still moving to the target.</remarks>
        public override TaskStatus OnUpdate()
        {
            agent.SetDestination(target.transform.position);

            Vector2 margin = new Vector2((target.transform.position.x - agent.transform.position.x), (target.transform.position.z - agent.transform.position.z));
            float norm = margin.magnitude;

            if(norm <5f)
            {
                freq -= (0.5f * Time.deltaTime);
                if(freq <= 0f)
                {
                    GameObject.Find("Soviet Tank").GetComponent<BulletManager>().AddBullets();
                    freq += 3f;
                }
            }

            return TaskStatus.RUNNING;
        }
        /// <summary>Abort method of MoveToGameObject </summary>
        /// <remarks>When the task is aborted, it stops the navAgentMesh.</remarks>
        public override void OnAbort()
        {

            #if UNITY_5_6_OR_NEWER
                        if (agent != null)
                            agent.isStopped = true;
            #else
                        if (agent!=null)
                            agent.Stop();
            #endif

        }
    }
}
