using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyEasyTouch : MonoBehaviour {

    private Animator animator;

    private bool isGround;
    private AudioClip Clip;
    private Rigidbody2D rb;
    //死亡次数
    private float dethNumber;
    public float JumpNeb = 60;

    // Use this for initialization
    void Start()
    {
        animator = this.GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("isGround", isGround);
    }

    public void RunStart()
    {
        animator.SetBool("isRun", true);
        
    }

    public void RunEnd()
    {
        animator.SetBool("isRun", false);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            isGround = true;
        }
        else if (collision.collider.tag == "money")
        {
            Clip = Resources.Load("Audios/menoy") as AudioClip;
            AudioSource.PlayClipAtPoint(Clip, Camera.main.transform.position);
            Destroy(collision.gameObject);
        }
        else if (collision.collider.tag == "wu")
        {
            Clip = Resources.Load("Audios/deth") as AudioClip;
            AudioSource.PlayClipAtPoint(Clip, Camera.main.transform.position);
            dethNumber += 1;
            if (dethNumber <= 3)
            {
                this.transform.position = new Vector3(this.transform.position.x - 3, this.transform.position.y + 10, this.transform.position.z);
            }
            else if (dethNumber > 3)
            {
                Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            isGround = false;
        }
    }

    public void Deriction(Vector2 postion)
    {
        if (postion.x > 0)
        {
            this.transform.localScale = new Vector3(-4,4,4);
        }else if (postion.x < 0)
        {
            this.transform.localScale = new Vector3(4, 4, 4);
        }
        print(postion.x);
    }

    public void JumpStart()
    {
        if (isGround)
        {
            animator.SetBool("isJump", true);
            rb.velocity = new Vector2(rb.velocity.x, JumpNeb);
            Clip = Resources.Load("Audios/Jump") as AudioClip;
            AudioSource.PlayClipAtPoint(Clip, Camera.main.transform.position);
        }
    }

    public void JumpEnd()
    {
        animator.SetBool("isJump", false);
    }
}
