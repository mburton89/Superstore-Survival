﻿using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.FirstPerson;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    public class EnemySight : MonoBehaviour
    {
        public NavMeshAgent agent;
        public ThirdPersonCharacter character;
        private bool pauseGame = false;

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

            waypoints = GameObject.FindGameObjectsWithTag("Waypoint");
            waypointInd = Random.Range(0, waypoints.Length);

            state = EnemySight.State.PATROL;

            alive = true;

            heightMultiplier = 1.36f;

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
                    case State.INVESTIGATE:
                        Investigate();
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
                character.Move (Vector3.zero, false, false);
            }
        }

        void Chase()
        {
            chaseTime -= Time.deltaTime;
            agent.speed = chaseSpeed;
            agent.SetDestination(target.transform.position);
            character.Move(agent.desiredVelocity, false, false);
            if (chaseTime <= 0)
            {
                state = EnemySight.State.INVESTIGATE;
            }
        }

        void Investigate()
        {
            timer += Time.deltaTime;
            agent.SetDestination(this.transform.position);
            character.Move(Vector3.zero, false, false);
            transform.LookAt(investigateSpot);
            if (timer >= investigateWait)
            {
                state = EnemySight.State.PATROL;
            }
        }

        void OnTriggerEnter(Collider coll)
        {
            if (coll.CompareTag("Player"))
            {
                state = EnemySight.State.INVESTIGATE;
                investigateSpot = coll.gameObject.transform.position;
            }
        }

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
            if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, transform.forward, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, (transform.forward + transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, (transform.forward - transform.right).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast (transform.position + Vector3.up * heightMultiplier, (transform.forward + (transform.right * 0.7f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - (transform.right * 0.7f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + (transform.right * 0.5f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - (transform.right * 0.5f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward + (transform.right * 0.2f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
            if (Physics.Raycast(transform.position + Vector3.up * heightMultiplier, (transform.forward - (transform.right * 0.2f)).normalized, out hit, sightDist))
            {
                if (hit.collider.gameObject.CompareTag("Player"))
                {
                    state = EnemySight.State.CHASE;
                    target = hit.collider.gameObject;
                }
            }
        }

        void OnCollisionEnter(Collision coll)
        {
            if (coll.collider.CompareTag("Player"))
            {
                YouLose.Instance.Show();
                ToggleTime();
            }
        }

        private void ToggleTime()
        {
            pauseGame = !pauseGame;

            if (pauseGame)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}

