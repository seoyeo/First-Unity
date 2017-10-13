using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlay : MonoBehaviour {
    private AudioSource asDeth;
    private bool isPlay;
	// Use this for initialization
	void Start () {
        asDeth = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
          if (isPlay)
          {
              asDeth.Play();
              isPlay = false;
          }
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "bullet")
        {
           //asDeth.Play();
            isPlay = true;
            print(isPlay);
        }
    }
}
