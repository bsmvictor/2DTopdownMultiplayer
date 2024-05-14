using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;         // Componente Rigidbody2D do objeto
    [SerializeField] private float visionRange;            // Distância máxima que o inimigo pode enxergar
    [SerializeField] private float attackRange;       // Distância máxima que o inimigo pode atacar
    [SerializeField] private float speed;             // Velocidade de movimento do inimigo

    [SerializeField] private GameObject[] player;   // Referência ao jogador
    [SerializeField] private List<GameObject> PLAYER = new List<GameObject>(); //referencia aos jogadores no multiplayer
    [SerializeField] private LayerMask Raycast;       // LayerMask para o Raycast

    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Vector3 target;
    [SerializeField] private Vector3 direction;
    [SerializeField] private float fov;

    // Start is called before the first frame update
    void Start()
    {
        Raycast = 1 << LayerMask.NameToLayer("Player");
        rb = GetComponent<Rigidbody2D>();
        spawnPoint = transform.position;
        target = spawnPoint;
    }

    // Update is called once per frame
    void Update()
    {
        AttackArea();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void AttackArea()
    {
        player = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject play in player)
        {
            PLAYER.Add(play);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, play.transform.position - transform.position, visionRange, Raycast);
            Vector3 temp = transform.TransformDirection(play.transform.position - transform.position);
            Debug.DrawRay(transform.position, temp, Color.cyan);

            if(hit.collider != null)
            {
                if (hit.collider.CompareTag("Player"))
                {
                    target = play.transform.position;
                    Debug.Log("Player detected");
                }
            }
            else
            {
                target = spawnPoint;
            }

            float distanceTemp = Vector3.Distance(transform.position, target);
            direction = (target - transform.position).normalized;

            if(target != spawnPoint && distanceTemp < attackRange)
            {
                
            }
            else
            {
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            }

            if(target == spawnPoint)
            {
                rb.MovePosition(transform.position + direction * speed * Time.deltaTime);
            }
        }
    }
}
