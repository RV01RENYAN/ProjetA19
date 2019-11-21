using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishFollow : MonoBehaviour
{
    public Transform B;   //A要跟随的B    
    public float smoothTime = 0.01f;  //A平滑移动的时间    
    private Vector3 AVelocity = Vector3.zero;
    private GameObject mainA;  //A 
    private bool collisionFish=false;
    private bool collisionSkewerTop = false;

    void Update()
    {
        if (collisionFish)
        {
            transform.GetComponentInParent<Transform>().position = Vector3.SmoothDamp(transform.GetComponentInParent<Transform>().position, B.position, ref AVelocity, smoothTime);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject);
        Debug.Log(collision.gameObject.GetComponentInChildren<Rigidbody>());
        if (collision.gameObject.CompareTag("Skewer"))
        {
            Debug.Log("Top");
            collisionSkewerTop = true;
            //collision.gameObject.GetComponent<Collider>().enabled = false;
            Debug.Log(transform.GetComponentInParent<Transform>());
            //Debug.Log(Vector3.SmoothDamp(transform.position, B.position, ref AVelocity, smoothTime));
            collisionFish = true;
            transform.GetComponentInParent<Rigidbody>().isKinematic = true;
            Debug.Log(transform.GetComponentInParent<Rigidbody>().isKinematic);
            transform.GetComponentInParent<Transform>().position = Vector3.SmoothDamp(transform.GetComponentInParent<Transform>().position, B.position + new Vector3(0, 0, -1), ref AVelocity, smoothTime);
        }
        if (collision.gameObject.CompareTag("Window"))
        {
            Debug.Log("window");
            
            transform.GetComponentInParent<Rigidbody>().isKinematic = true;
            Debug.Log(transform.GetComponentInParent<Rigidbody>().isKinematic);
            //transform.GetComponentInParent<Transform>().position = Vector3.SmoothDamp(transform.GetComponentInParent<Transform>().position, B.position + new Vector3(0, 0, -1), ref AVelocity, smoothTime);
        }
    }

    //void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.CompareTag("Skewer"))
    //    {
    //        Debug.Log("skewer");
    //        transform.position = Vector3.SmoothDamp(transform.position, B.position + new Vector3(0, 0, 1), ref AVelocity, smoothTime);
    //    }
    //}
}

