using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
    /// <summary>
    /// It is an action to move towards the given goal using a NavMeshAgent.
    /// </summary>
    [Action("Wander Blue")]
    [Help("Moves with wander")]
    public class WanderBlue : GOAction
    {
        ///<value>Input target game object towards this game object will be moved Parameter.</value>
        [InParam("target")]
        [Help("Target game object towards this game object will be moved")]
        public GameObject target;

        [InParam("agent")]
        [Help("agent")]
        private UnityEngine.AI.NavMeshAgent agent;

        [InParam("floor")]
        [Help("floor")]
        public Collider floor;

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
			agent.SetDestination(target.transform.position);
            
            #if UNITY_5_6_OR_NEWER
                agent.isStopped = false;
            #else
                navAgent.Resume();
            #endif
        }

        /// <summary>Method of Update of MoveToGameObject.</summary>
        /// <remarks>Verify the status of the task, if there is no objective fails, if it has traveled the road or is near the goal it is completed
        /// y, the task is running, if it is still moving to the target.</remarks>
        Vector3 wanderTarget = Vector3.zero;
        public override TaskStatus OnUpdate()
        {
            //Debug.Log("wanderiiiiiiiiiiiiiiiing");
            float wanderRadius = 10;
            float wanderDistance = 10;
            float wanderJitter = 1;
                wanderTarget += new Vector3(Random.Range(-1.0f, 1.0f) * wanderJitter,
                                            0,
                                            Random.Range(-1.0f, 1.0f) * wanderJitter);
            wanderTarget.Normalize();
            wanderTarget *= wanderRadius;

            Vector3 targetLocal = wanderTarget + new Vector3(0, 0, wanderDistance);
            Vector3 targetWorld = this.gameObject.transform.InverseTransformVector(targetLocal);

            if (!floor.bounds.Contains(targetWorld))
            {
                targetWorld = -agent.transform.position * 0.1f;
            };

            agent.SetDestination(targetWorld);
            
            return TaskStatus.RUNNING;
        }
        /// <summary>Abort method of MoveToGameObject </summary>
        /// <remarks>When the task is aborted, it stops the navAgentMesh.</remarks>
        public override void OnAbort()
        {

        #if UNITY_5_6_OR_NEWER
            if(agent!=null)
                agent.isStopped = true;
        #else
            if (agent!=null)
                agent.Stop();
        #endif

        }
    }
}
