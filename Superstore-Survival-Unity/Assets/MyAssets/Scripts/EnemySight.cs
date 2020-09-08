using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class EnemySight : MonoBehaviour
    {
        [SerializeField] Animator myAnimatorController;
        
        public GameObject Player;

        public NavMeshAgent agent;
        public ThirdPersonCharacter character;
        private bool pauseGame = false;

        //What state the enemy AI can be in
        public enum State
        {
            PATROL,
            CHASE,
            INVESTIGATE
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
        private float chaseTime = 10f;

        //Variables for Investigating
        private Vector3 investigateSpot;
        private float timer = 0;
        public float investigateWait = 10;

        //Variables for Sight
        public float heightMultiplier;
        public float sightDist = 10;

        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            character = GetComponent<ThirdPersonCharacter>();

            agent.updatePosition = true;
            agent.updateRotation = false;

            //Find waypoints within the level
            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            waypointInd = Random.Range(0, waypoints.Length);

            state = EnemySight.State.PATROL;

            alive = true;

            heightMultiplier = 1.36f;

            StartCoroutine("FSM");
        }

        //Enemy switches between states
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
                    case State.INVESTIGATE:
                        Investigate();
                        break;
                }
                yield return null;
            }
        }

        //Enemy wanders around the level, randomly picking one of the waypoints to walk to
        void Patrol()
        {
            myAnimatorController.SetBool("investigating", false);
            myAnimatorController.SetBool("walking", true);
            myAnimatorController.SetBool("running", false);

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

        //Enemy moves directly to the player's position, wherever they are within the level
        void Chase()
        {
            myAnimatorController.SetBool("investigating", false);
            myAnimatorController.SetBool("walking", false);
            myAnimatorController.SetBool("running", true);

            //Chase cooldown starts
            chaseTime -= Time.deltaTime;
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
            
            //Once chase cooldown reaches zero enemy switches back to investigate state
            if (chaseTime <= 0)
            {
                chaseTime = 10f;
                state = EnemySight.State.INVESTIGATE;
            }
        }

        //Enemy looks around for player
        void Investigate()
        {
            myAnimatorController.SetBool("investigating", true);
            myAnimatorController.SetBool("walking", false);
            myAnimatorController.SetBool("running", false);

            //Investigate cooldown time starts
            timer += Time.deltaTime;
            agent.SetDestination(this.transform.position);
            character.Move(Vector3.zero, false, false);
            transform.LookAt(investigateSpot);

            //Once investigate cooldown time reaches zero enemy goes back to patrol state
            if (timer >= investigateWait)
            {
                state = EnemySight.State.PATROL;
            }
        }

        //If player gets to close enemy switches to investigate state
        void OnTriggerEnter(Collider coll)
        {
            if (coll.CompareTag("Player"))
            {
                state = EnemySight.State.INVESTIGATE;
                investigateSpot = coll.gameObject.transform.position;
            }
        }

        //Determine enemies sight radius. If they spot the player enemy enters chase state
        void FixedUpdate()
        {
            RaycastHit hit;
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, transform.forward * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + (transform.right * 0.7f)).normalized * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - (transform.right * 0.7f)).normalized * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + (transform.right * 0.5f)).normalized * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - (transform.right * 0.5f)).normalized * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward + (transform.right * 0.2f)).normalized * sightDist, Color.green);
            Debug.DrawRay(transform.position + Vector3.up * heightMultiplier, (transform.forward - (transform.right * 0.2f)).normalized * sightDist, Color.green);
            
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
            {
                //Enemy does not see player while they are in a hiding spot if they were patrolling
                if (hit.collider.gameObject.CompareTag("HidingSpot") && state == State.PATROL)
                {
                    state = EnemySight.State.PATROL;
                }
                //Enemy investigates hiding spot area if they were chasing
                else if (hit.collider.gameObject.CompareTag("HidingSpot") && state == State.CHASE)
                {
                    state = EnemySight.State.INVESTIGATE;
                }
                //Enemy sees player and enters chase state
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("HidingSpot"))
                {
                    state = EnemySight.State.PATROL;
                }
                else if (hit.collider.gameObject.CompareTag("HidingSpot") && state == State.CHASE)
                {
                    state = EnemySight.State.INVESTIGATE;
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("HidingSpot"))
                {
                    state = EnemySight.State.PATROL;
                }
                else if (hit.collider.gameObject.CompareTag("HidingSpot") && state == State.CHASE)
                {
                    state = EnemySight.State.INVESTIGATE;
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + (transform.right * 0.7f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("HidingSpot"))
                {
                    state = EnemySight.State.PATROL;
                }
                else if (hit.collider.gameObject.CompareTag("HidingSpot") && state == State.CHASE)
                {
                    state = EnemySight.State.INVESTIGATE;
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - (transform.right * 0.7f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("HidingSpot"))
                {
                    state = EnemySight.State.PATROL;
                }
                else if (hit.collider.gameObject.CompareTag("HidingSpot") && state == State.CHASE)
                {
                    state = EnemySight.State.INVESTIGATE;
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + (transform.right * 0.5f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("HidingSpot"))
                {
                    state = EnemySight.State.PATROL;
                }
                else if (hit.collider.gameObject.CompareTag("HidingSpot") && state == State.CHASE)
                {
                    state = EnemySight.State.INVESTIGATE;
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - (transform.right * 0.5f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("HidingSpot"))
                {
                    state = EnemySight.State.PATROL;
                }
                else if (hit.collider.gameObject.CompareTag("HidingSpot") && state == State.CHASE)
                {
                    state = EnemySight.State.INVESTIGATE;
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + (transform.right * 0.2f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("HidingSpot"))
                {
                    state = EnemySight.State.PATROL;
                }
                else if (hit.collider.gameObject.CompareTag("HidingSpot") && state == State.CHASE)
                {
                    state = EnemySight.State.INVESTIGATE;
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - (transform.right * 0.2f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("HidingSpot"))
                {
                    state = EnemySight.State.PATROL;
                }
                else if (hit.collider.gameObject.CompareTag("HidingSpot") && state == State.CHASE)
                {
                    state = EnemySight.State.INVESTIGATE;
                }
                else if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
        }

        //Enemy has caught player and trigger You Lose screen
        void OnCollisionEnter(Collision coll)
        {
            if (coll.collider.CompareTag("Player"))
            {
                YouLose.Instance.Show();
                ToggleTime();
            }
        }

        //Game stops and cursor appears
        private void ToggleTime()
        {
            pauseGame = !pauseGame;

            if (pauseGame)
            {
                Player.GetComponent<FirstPersonController>().enabled = false;
                Time.timeScale = 0;
                showCursor();
            }
            else
            {
                Time.timeScale = 1;
            }
        }
        public void showCursor()
        {
            Player.GetComponent<FirstPersonController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}

