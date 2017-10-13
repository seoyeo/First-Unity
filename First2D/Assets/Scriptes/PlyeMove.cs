using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlyeMove : MonoBehaviour {

    //动画控制器
    private Animator playerAnimator;
    private Rigidbody2D rb;
    // 行进速度
    public float forceVlue ;
    //跳跃高度
    public float jumpVlue ;
    //确定按钮被按下
    private bool Btn_left_bool;
    private bool Btn_right_bool;
    private bool Btn_jump_bool;
    //动画变量值
    private bool isGround;
    private bool isRun;
    private bool isJump;
    //private bool isWall;
    //角色方向
    public bool playerDeriction;

    private AudioClip Clip;
    //死亡次数
    private float dethNumber;
    // Use this for initialization
    void Start () {
        forceVlue = 7000;
        playerAnimator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        playerDeriction = true;
	}

    private void Update()
    {
# region 确定按钮被按下
        if (Input.GetKeyDown(KeyCode.D)){
            Btn_right_bool = true;
            isRun = true;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            Btn_right_bool = false;
            isRun = false;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            Btn_left_bool = true;
            isRun = true;
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            Btn_left_bool = false;
            isRun = false;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            Btn_jump_bool = true;
            
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Btn_jump_bool = false;
           
        }
        #endregion
    }
    // Update is called once per frame
    void FixedUpdate () {
        Move();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")//判断是否在地面
        {
            isGround = true;
        }else if(collision.collider.tag == "money")//判断是否与金币发生碰撞
        {
            Clip = Resources.Load("Audios/menoy") as AudioClip;
            AudioSource.PlayClipAtPoint(Clip,Camera.main.transform.position);
            Destroy(collision.gameObject);
        }else if(collision.collider.tag == "wu")//判断与小怪的碰撞
        {
            Clip = Resources.Load("Audios/deth") as AudioClip;
            AudioSource.PlayClipAtPoint(Clip, Camera.main.transform.position);
            dethNumber += 1;
            if (dethNumber <= 3)
            {
                this.transform.position = new Vector3(this.transform.position.x - 3, this.transform.position.y + 10, this.transform.position.z);
            }else if (dethNumber > 3)
            {
            Destroy(this.gameObject);
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.collider.tag == "ground")
        {
            isGround = false;
        }
    }

    private void ChongSheng()
    {
        if (dethNumber > 3)
        {

        }
        if (dethNumber <= 3)
        {

        }
    }

    public void Move()
    {
        if (Btn_right_bool && Btn_left_bool == false && isRun)
        {
            if (playerDeriction == false)
            {
                playerDeriction = true;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            rb.AddForce(new Vector2(forceVlue * Time.deltaTime, 0f));
        }

        if (Btn_left_bool && Btn_right_bool == false && isRun)
        {
            if (playerDeriction == true)
            {
                playerDeriction = false;
                transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            rb.AddForce(new Vector2(-forceVlue * Time.deltaTime, 0f));
        }
        if (Btn_jump_bool == true && isGround == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVlue);
            Clip = Resources.Load("Audios/Jump") as AudioClip;
            AudioSource.PlayClipAtPoint(Clip, Camera.main.transform.position);
        }

        playerAnimator.SetFloat("HorizontalSpeed", Mathf.Abs(rb.velocity.x));
        playerAnimator.SetFloat("VerticalSpeed", rb.velocity.y);
        playerAnimator.SetBool("isGround", isGround);
        playerAnimator.SetBool("isRun", isRun);
    }

    public void LefeMove()
    {
        if (playerDeriction == false)
        {
            playerDeriction = true;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        rb.AddForce(new Vector2(forceVlue * Time.deltaTime, 0f));
    }

    public void RightMove()
    {
        if (playerDeriction == true)
        {
            playerDeriction = false;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        rb.AddForce(new Vector2(-forceVlue * Time.deltaTime, 0f));
    }

    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpVlue);
        Clip = Resources.Load("Audios/Jump") as AudioClip;
        AudioSource.PlayClipAtPoint(Clip, Camera.main.transform.position);
    }

}