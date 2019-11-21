using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chilli_Controller : MonoBehaviour
{
	public GameObject Boite_chilli;
	public Vector3 Position;
	public GameObject chilli;	// Le prefab chilli
    public int count=0;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Position=Boite_chilli.transform.GetChild(0).position;
    	if(count<1000)
    		{
        	if(Input.GetKey("q")){
        		Instantiate(chilli, Position, Quaternion.identity);
        		count++;
        	}
    	}
    }
}
