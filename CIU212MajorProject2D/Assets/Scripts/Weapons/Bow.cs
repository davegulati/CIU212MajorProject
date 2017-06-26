using UnityEngine;

public class Bow : MonoBehaviour {

    private GameObject sen;

    public GameObject projectile;

    private float arrowForce = 200.0f;
    public float damage = 10.0f;

    private float fireRate = 1.0f;
    private float nextTimeToFire = 0.0f;

    private void Awake()
    {
        sen = GameObject.Find("Sen");
    }

    // Update is called once per frame
    void Update () 
    {
        if (Input.GetMouseButton(0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
	}

    private void Shoot ()
    {
        float arrowRotation = 0;
        float arrowForceOrientation = 0;
        if (sen.transform.localScale.x > 0)
        {
            arrowRotation = -90.0f;
            arrowForceOrientation = arrowForce;
        }
        else if (sen.transform.localScale.x < 0)
        {
            arrowRotation = 90.0f;
            arrowForceOrientation = arrowForce * -1;
        }

        GameObject spawnedArrow = Instantiate(projectile, new Vector2(gameObject.transform.position.x, gameObject.transform.position.y), Quaternion.Euler(0, 0, arrowRotation));
        spawnedArrow.GetComponent<Rigidbody2D>().AddForce(new Vector2(arrowForce * arrowForceOrientation, 0) * Time.deltaTime);
    }
}
