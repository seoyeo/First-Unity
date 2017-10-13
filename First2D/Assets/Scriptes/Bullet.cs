using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    //本地子弹预制体
    private GameObject localBullet;
    //实例化的预制体
    private GameObject newBullet;
    private Rigidbody2D rb;
    public float bulletSpeed = 100;
    public PlyeMove plyeMove;
    //子弹方向
    private bool bulletDeriction;
    //子弹发间隔时间
    private float fireTime = 0.3f;
    private float nextFireTime = 0f;
    private float derictionValue;

    // Use this for initialization
    void Start () {
        localBullet = Resources.Load("Prefab/Bullet") as GameObject;
    } 
	
	// Update is called once per frame
	void Update () {
    }
    public void BulletDeriction(Vector2 postion)
    {
        derictionValue = postion.x;
    }

    public void FireBullet()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireTime;

            if (localBullet != null)
            {
                AudioClip clip = Resources.Load("Audios/Bullet") as AudioClip;
                AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
                newBullet = Instantiate(localBullet);
                rb = newBullet.GetComponent<Rigidbody2D>();

                if (derictionValue > 0)
                {
                    newBullet.transform.position = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y + 0.06f, this.transform.position.z);
                    newBullet.transform.localScale = new Vector3(newBullet.transform.localScale.x, newBullet.transform.localScale.y, newBullet.transform.localScale.z);
                    rb.velocity = newBullet.transform.TransformDirection(Vector2.right * bulletSpeed);
                }
                else if (derictionValue < 0)
                {
                    newBullet.transform.position = new Vector3(this.transform.position.x - 0.5f, this.transform.position.y + 0.06f, this.transform.position.z);
                    newBullet.transform.localScale = new Vector3(-newBullet.transform.localScale.x, newBullet.transform.localScale.y, newBullet.transform.localScale.z);
                    rb.velocity = newBullet.transform.TransformDirection(-Vector2.right * bulletSpeed);
                }
                Destroy(newBullet, 0.5f);
            }
        }
    }
}
