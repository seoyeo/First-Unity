using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {

    public GameObject PlayerTarget;
    public float FollowSpeed = 10;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (PlayerTarget != null)
        {
            //Vector3 PositionBefore = this.transform.position;
           // Vector3 NewPosition = Vector3.Lerp(this.transform.position, PlayerTarget.transform.position, FollowSpeed * Time.deltaTime);
            this.transform.position = new Vector3(PlayerTarget.transform.position.x, PlayerTarget.transform.position.y, this.transform.position.z);
        }
       // Invoke("BackToBusiness", 0.2f);
	}
}
