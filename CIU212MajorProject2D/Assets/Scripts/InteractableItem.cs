using UnityEngine;

public class InteractableItem : MonoBehaviour {
    
    protected GameObject sen;
    public float activationRange = 0.8f;
    private GameObject itemCanvas;

    private void Awake()
    {
        sen = GameObject.Find("Sen");
        itemCanvas = transform.Find("ItemCanvas").gameObject;
        itemCanvas.SetActive(false);
    }

    private void Update()
    {
		float distance = Vector2.Distance(transform.position, sen.transform.position);
		if (distance < activationRange)
		{
			itemCanvas.SetActive(true);
			if (Input.GetButtonDown("Interact"))
			{
                Interact();
			}
		}
		else
		{
			itemCanvas.SetActive(false);
		}
    }

    public virtual void Interact()
    {
		
	}

    public virtual void UseItem()
    {
        
    }
}
