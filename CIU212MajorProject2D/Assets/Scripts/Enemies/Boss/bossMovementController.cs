using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossMovementController : MonoBehaviour
{
    public Transform[] teleportPoints;
    int currentPoint;
    public float speed = 0.5f;
    public float timestill = 2f;
    public float sight = 3f;
    Animator anim;
    public float force;

    private GameObject target;
    private AudioSource source;
    //public AudioClip[] sfx;

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine("Teleport");
        anim.SetBool("walking", true);
        Physics2D.queriesStartInColliders = false;
        target = GameObject.FindWithTag("Player");

        currentPoint = Random.Range(0, teleportPoints.Length);
    }

    void Awake()
    {
        source = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.localScale.x * Vector2.right, sight);
        if (hit.collider != null && hit.collider.tag == "Player")
            GetComponent<Rigidbody2D>().AddForce(Vector3.up * force + (hit.collider.transform.position - transform.position) * force);

        float distance = Vector2.Distance(transform.position, target.transform.position);
    }


    IEnumerator Teleport()
    {
        while (true)
        {

            if (transform.position.x == teleportPoints[currentPoint].position.x)
            {
                currentPoint = (Random.Range(0, teleportPoints.Length));
                anim.SetTrigger("Teleport");
                yield return new WaitForSeconds(timestill);
                anim.SetTrigger("Teleport");
            }


            if (currentPoint >= teleportPoints.Length)
            {
                currentPoint = 0;
            }
            
            //transform.position = Vector2.MoveTowards(transform.position, new Vector2(teleportPoints[currentPoint].position.x, transform.position.y), speed);
            transform.position = Vector3.MoveTowards(transform.position, teleportPoints[currentPoint].position , speed);

            if (transform.position.x > teleportPoints[currentPoint].position.x)
                transform.localScale = new Vector3(-1, 1, 1);
            else if (transform.position.x < teleportPoints[currentPoint].position.x)
                transform.localScale = Vector3.one;


              yield return null;


        }
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.position + transform.localScale.x * Vector3.right * sight);

    }

}
