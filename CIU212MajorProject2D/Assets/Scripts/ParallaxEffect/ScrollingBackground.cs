using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour
{
    public bool scrolling = true;
    public bool parallax = true;
    public bool freezeVerticalAspect = true;

    public float backgroundSize;
    public float parallaxSpeed;

    private Transform cameraTransform;
    private Transform[] layers;
    private float viewZone = 10;
    private int leftIndex;
    private int rightIndex;
    private float lastCameraX;
    private float lastCameraY;


    private void Start()
    {
        cameraTransform = Camera.main.transform;
        lastCameraX = cameraTransform.position.x;
        lastCameraY = cameraTransform.position.y;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            layers[i] = transform.GetChild(i);
        }

        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }

    private void Update()
    {
        if (parallax)
        {
            float deltaX = cameraTransform.position.x - lastCameraX;
            transform.position += Vector3.right * (deltaX * parallaxSpeed);
        }

        lastCameraX = cameraTransform.position.x;
        //lastCameraY = cameraTransform.position.y;

        if (scrolling)
        {
            if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
            {
                ScrollLeft();
            }

            if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
            {
                ScrollRight();
            }
        }

        //     if (freezeVerticalAspect)
        //     {
        //         if (cameraTransform.position.y > (layers[rightIndex].transform.position.y) || cameraTransform.position.y < (layers[rightIndex].transform.position.y))
        //         {
        //             AlignVerticalAspect();
        //         }

        //if (cameraTransform.position.y > (layers[leftIndex].transform.position.y) || cameraTransform.position.y < (layers[leftIndex].transform.position.y))
        //{
        //	AlignVerticalAspect();
        //}
        //}
    }

    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if (rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
	}

    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }  
    }

    private void AlignVerticalAspect ()
    {
        float indexLeft = layers[leftIndex].position.y;
        indexLeft = cameraTransform.position.y;
        float indexRight = layers[rightIndex].position.y;
        indexRight = cameraTransform.position.y;
        Debug.Log("test");
	}
}
