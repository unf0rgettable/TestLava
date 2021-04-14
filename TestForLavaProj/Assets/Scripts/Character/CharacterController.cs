using System.Collections.Generic;
using System.Linq;
using Controllers;
using Settings;
using Triggers;
using UnityEngine;
using UnityEngine.AI;

namespace Character
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Animator))]
    public class CharacterController : MonoBehaviour
    {
        [SerializeField]
        private CharacterSettings characterSettings;
        private NavMeshAgent _navMeshAgent;
        private Animator _animator;
        private AttackController _attackController;
        private List<CharacterController> _enemies;
        private CharacterState CurrentState { get; set; } = CharacterState.Idle;
        private float Speed => _navMeshAgent.velocity.magnitude;

        #region MonoBehavior

        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = characterSettings.SpeedCharacter;
            _animator = GetComponent<Animator>();
            _attackController = GetComponent<AttackController>();
            _enemies = FindObjectsOfType<CharacterController>().ToList();
            _enemies.Remove(this);
        }
        
        private void Update()
        {
            _animator.SetFloat("Speed", Speed);
            if (CurrentState == CharacterState.FindEnemy)
            {
                foreach (var enemy in _enemies)
                {
                    RaycastHit hit;
                    Vector3 direction = enemy.transform.position - transform.position;
                    Ray ray = new Ray(transform.position, direction);
                    if (Physics.Raycast(ray, out hit, 100f, ~(1 << LayerMask.NameToLayer("Trigger")),
                        QueryTriggerInteraction.Ignore))
                    {
                        CurrentState = CharacterState.Attack;
                    }
                    Debug.DrawRay(transform.position, direction, Color.red, Time.deltaTime);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Trigger") &&
                other.TryGetComponent(out TriggerAttacks triggerAttacks))
            {
                SetTargetPoint(triggerAttacks.transform.position);
                CurrentState = CharacterState.FindEnemy;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Trigger") &&
                other.TryGetComponent(out TriggerAttacks triggerAttacks))
            {
                CurrentState = CharacterState.Run;
            }
        }

        #endregion
        
        public void SetTargetPoint(Vector3 pos)
        {
           
            if(CurrentState != CharacterState.Attack)
                _navMeshAgent.SetDestination(pos);
            else if (CurrentState == CharacterState.Attack)
            {
                Vector3 dir = new Vector3(pos.x, transform.position.y, pos.z) - transform.position;
                transform.forward = dir;
                Attack(pos);
            }
        }

        private void Attack(Vector3 targetPosition)
        {
            _animator.SetFloat("AttackSpeed", characterSettings.SpeedAttack);
            _animator.Play("Attack");
            _animator.SetBool("isAttack", true);
            _animator.GetBehaviour<AttackStateMachine>().OnAttack = () =>
            {
                _attackController?.Attack(characterSettings.TypeBulletForce, targetPosition);
            };
        }
    }
}
