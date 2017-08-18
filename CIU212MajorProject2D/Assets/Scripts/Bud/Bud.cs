using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bud : MonoBehaviour {

    private GameObject sen;
    private float budSpeed;
    private float maxDistanceFromSen = 2.0f;

    //private Animator anim;

    private void Awake()
    {
        sen = GameObject.Find("Sen");
        //anim = gameobject.GetComponent<Animator>();
    }

    private void Update()
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
        budSpeed = distance;
		if (distance > maxDistanceFromSen)
		{
			BudFollowSen();
		}
        else if (distance < maxDistanceFromSen)
        {
            //anim.SetBool ("Float", true);
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            UseActiveAbility();
        }
    }

    public void BudSpawn ()
    {
        
    }

    public void BudFollowSen ()
    {
		transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), sen.transform.position, budSpeed * Time.deltaTime);
        //anim.SetBool ("Fly", true);
    }

	public void SetActiveAbility()
	{

	}

    public void UseActiveAbility ()
    {
        Debug.Log("Bud's active ability not set!"); // For now. 
    }

    public void BudDespawn ()
    {
        
    }
}
