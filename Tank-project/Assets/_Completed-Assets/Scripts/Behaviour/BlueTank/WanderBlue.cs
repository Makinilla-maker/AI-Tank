using Pada1.BBCore.Tasks;
using Pada1.BBCore;
using UnityEngine;

namespace BBUnity.Actions
{
     [Action("Wander Blue")]
    [Help("Moves with wander")]
    public class WanderBlue : GOAction
    {
        

        [InParam("agent")]
        [Help("agent")]
        private UnityEngine.AI.NavMeshAgent agent;

        [InParam("destPoint")]
        [Help("destPoint")]
        private int destPoint = 0;

        public GameObject[] waypoints = new GameObject[10];

        public override void OnStart()
        {
            if (GameObject.Find("PatrolList").GetComponent<PatrolList>().patrolList == null)
            {
                Debug.LogError("The movement target of this game object is null", gameObject);
                return;
            }
            for(int i = 0; i< GameObject.Find("PatrolList").GetComponent<PatrolList>().patrolList.Length; ++i)
            {
                waypoints[i] = GameObject.Find("PatrolList").GetComponent<PatrolList>().patrolList[i];
            }

            agent = gameObject.GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent == null)
            {
                Debug.LogWarning("The " + gameObject.name + " game object does not have a Nav Mesh Agent component to navigate. One with default values has been added", gameObject);
                agent = gameObject.AddComponent<UnityEngine.AI.NavMeshAgent>();
            }
			agent.autoBraking = false;

            
            #if UNITY_5_6_OR_NEWER
                agent.isStopped = false;
            #else
                navAgent.Resume();
            #endif
        }

        void GotoNextPoint() {
                       
            agent.destination = waypoints[destPoint].transform.position;
            destPoint = (destPoint + 1) % waypoints.Length;
            //Debug.Log(GameObject.Find("PatrolList").GetComponent<PatrolList>().patrolList[destPoint].position);
        }
        public override TaskStatus OnUpdate()
        {
            Debug.Log(waypoints.Length);
            Debug.Log(waypoints[0].transform.position);
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                GotoNextPoint();
            }

            return TaskStatus.RUNNING;
        }
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
