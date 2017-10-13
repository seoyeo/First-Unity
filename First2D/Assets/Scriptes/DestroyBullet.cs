using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBullet : MonoBehaviour {

    private AudioSource asMusic;
    private AudioClip clip;
    // Use this for initialization
    void Start () {
        asMusic = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "ground"||collision.gameObject.tag =="wall")
        {
            Destroy(this.gameObject);
        }else if(collision.gameObject.tag == "cat")
        {
            clip = Resources.Load("Audios/deth") as AudioClip;
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }else if(collision.gameObject.tag == "wu")
        {
            clip = Resources.Load("Audios/deth") as AudioClip;
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
            Destroy(this.gameObject);
            Destroy(collision.gameObject);
        }
    }
}
