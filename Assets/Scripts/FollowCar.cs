using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCar : MonoBehaviour {

    public GameObject car;
	// Use this for initialization
	void Start ()
    {

	}

	// Update is called once per frame
	void Update ()
    {
        transform.position.Set(car.transform.position.x, car.transform.position.y, -10);
	}
}
