using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class CameraSight : MonoBehaviour
    {
        public NavMeshAgent agent;
        public ThirdPersonCharacter character;

        public enum State
        {
            PATROL,
            CHASE
        }

        public State state;
        private bool alive;

        //Variables for Patrolling
        public GameObject[] waypoints;
        private int waypointInd;
        public float patrolSpeed = 0.5f;

        //Variables for Chasing
        public float chaseSpeed = 1f;
        public GameObject target;

        //Variables for camera sight
        public GameObject player;
        public Collider playerColl;
        public Camera myCam;
        private Plane[] planes;

        public float sightDist = 10;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            waypointInd = Random.Range(0, waypoints.Length);

            playerColl = player.GetComponent<Collider>();

            state = CameraSight.State.PATROL;

            alive = true;

            StartCoroutine("FSM");
        }

        IEnumerator FSM()
        {
            while (alive)
            {
                switch (state)
                {
                    case State.PATROL:
                        Patrol();
                        break;
                    case State.CHASE:
                        Chase();
                        break;
                }
                yield return null;
            }
        }

        void Patrol()
        {
            agent.speed = patrolSpeed;
            if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) >= 2)
            {
                agent.SetDestination(waypoints[waypointInd].transform.position);
                character.Move(agent.desiredVelocity, false, false);
            }
            else if (Vector3.Distance(this.transform.position, waypoints[waypointInd].transform.position) <= 2)
            {
                waypointInd = Random.Range(0, waypoints.Length);
            }
            else
            {
                character.Move(Vector3.zero, false, false);
            }
        }

        void Chase()
        {
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
        }

        void Update()
        {
            planes = GeometryUtility.CalculateFrustumPlanes(myCam);
            if (GeometryUtility.TestPlanesAABB(planes, playerColl.bounds))
            {
                Debug.Log("Player Sighted!");
                CheckForPlayer();
            }
            else
            {  }
        }

        void CheckForPlayer()
        {
            RaycastHit hit;
            Debug.DrawRay(myCam.transform.position, transform.forward * 10, Color.green);
            if (Physics.Raycast (myCam.transform.position, transform.forward, out hit, 10))
            {
                state = CameraSight.State.CHASE;
                target = hit.collider.gameObject;
            }
        }
    }
}