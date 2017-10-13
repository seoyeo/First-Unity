using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControl : MonoBehaviour
{

    public MyJoystick myJoystick;
    //动画控制器
    private Animator playerAnimator;
    private Rigidbody2D rbPlayer;
    private Rigidbody2D rbBullte;

    // 行进速度
    public float forceVlue;
    //跳跃高度
    public float jumpVlue;

    //动画变量值
    private bool isGround;
    private bool isRun;
    private bool isJump;
    private bool isRightMove;
    private bool isLeftMove;

    //角色方向
    public bool playerDeriction;
    //死亡次数
    private float dethNumber;
    //子弹预制体资源
    private GameObject Bullte;
    //子弹实体
    private GameObject LocalBullte;

    //子弹发间隔时间
    private float fireTime = 0.3f;
    private float nextFireTime = 0f;

    //游戏结束
    public GameObject endGame;

    //生命个数对象
    public GameObject lifeObj1;
    public GameObject lifeObj2;
    public GameObject lifeObj3;

    private AudioClip Clip;
    // Use this for initialization
    void Start()
    {
        forceVlue = 7000;
        jumpVlue = 50;
        playerAnimator = GetComponent<Animator>();
        rbPlayer = this.GetComponent<Rigidbody2D>();
        playerDeriction = true;
        myJoystick = GameObject.FindObjectOfType<MyJoystick>();
        myJoystick.OnJoyStickTouchBegin += OnJoyStickBegin;
        myJoystick.OnJoyStickTouchMove += OnJoyStickMove;
        myJoystick.OnJoyStickTouchEnd += OnJoyStickEnd;
        endGame.SetActive(false);

        //加载子弹预制体
        Bullte = Resources.Load("Prefab/Bullet") as GameObject;

    }

    // Update is called once per frame
    void Update()
    {
        playerAnimator.SetBool("isRun", isRun);
        playerAnimator.SetBool("isGround", isGround);
        if (isRightMove)
        {
            transform.localScale = new Vector3(-4, 4, 4);
            rbPlayer.AddForce(new Vector2(forceVlue * Time.deltaTime, 0f));
            playerDeriction = true;
            
        }
         if (isLeftMove)
        {
            transform.localScale = new Vector3(4, 4, 4);
            rbPlayer.AddForce(new Vector2(-forceVlue * Time.deltaTime, 0f));
            playerDeriction = false;
            
        }
        DethPlayer();
        //playerAnimator.SetBool()
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }
    #region 子弹发射事件
    public void FireBullet()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + fireTime;
            if (Bullte != null)
            {
                LocalBullte = Instantiate(Bullte);
                rbBullte = LocalBullte.GetComponent<Rigidbody2D>();
                if (playerDeriction)
                {
                    LocalBullte.transform.position = new Vector3(this.transform.position.x + 0.5f, this.transform.position.y + 0.06f, this.transform.position.z);
                    LocalBullte.transform.localScale = new Vector3(LocalBullte.transform.localScale.x, LocalBullte.transform.localScale.y, LocalBullte.transform.localScale.z);
                    rbBullte.velocity = LocalBullte.transform.TransformDirection(Vector2.right * 100);
                }
                else if (playerDeriction == false)
                {
                    LocalBullte.transform.position = new Vector3(this.transform.position.x - 0.5f, this.transform.position.y + 0.06f, this.transform.position.z);
                    LocalBullte.transform.localScale = new Vector3(-LocalBullte.transform.localScale.x, LocalBullte.transform.localScale.y, LocalBullte.transform.localScale.z);
                    rbBullte.velocity = LocalBullte.transform.TransformDirection(-Vector2.right * 100);
                }
            }
        }
    }
    #endregion

    #region 跳
    public void Jump()
    {
        if (isGround)
        {
            rbPlayer.velocity = new Vector2(rbPlayer.velocity.x, jumpVlue);
            Clip = Resources.Load("Audios/Jump") as AudioClip;
            AudioSource.PlayClipAtPoint(Clip, Camera.main.transform.position);
        }
        //Clip = Resources.Load("Audios/Jump") as AudioClip;
        //AudioSource.PlayClipAtPoint(Clip, Camera.main.transform.position);
    }
    #endregion

    #region 摇杆事件
    void OnJoyStickBegin(Vector2 vec)//开始触摸虚拟摇杆
    {
        Debug.Log("开始触摸虚拟摇杆");
    }

    void OnJoyStickMove(Vector2 vec)//正在移动虚拟摇杆
    {

        if (vec.x > 0 && isGround)
        {
            isRightMove = true;
            isLeftMove = false;
        }
        else if (vec.x < 0 && isGround)
        {
            isLeftMove = true;
            isRightMove = false;
        }
        //移动角色并播放奔跑动画
        isRun = true;
    }

    void OnJoyStickEnd()//触摸移动摇杆结束
    {
        Debug.Log("触摸移动摇杆结束");
        isRun = false;
        isLeftMove = false;
        isRightMove = false;
    }
    #endregion

    #region 碰撞事件
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")//判断是否在地面
        {
            isGround = true;
        }
        else if (collision.collider.tag == "money")//判断是否与金币发生碰撞
        {
            Clip = Resources.Load("Audios/menoy") as AudioClip;
            AudioSource.PlayClipAtPoint(Clip, Camera.main.transform.position);
            Destroy(collision.gameObject);
        }
        else if (collision.collider.tag == "wu")//判断与小怪的碰撞
        {
            dethNumber += 1;
            Clip = Resources.Load("Audios/deth") as AudioClip;
            AudioSource.PlayClipAtPoint(Clip, Camera.main.transform.position);
            Destroy(collision.gameObject);
            this.transform.position = new Vector3(this.transform.position.x - 3, this.transform.position.y + 10, this.transform.position.z);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground")
        {
            isGround = false;
        }
    }
    #endregion

    #region 死亡处理事件
    private void DethPlayer()
    {
        if (dethNumber == 3)
        {
            lifeObj3.SetActive(false);
            Destroy(this.gameObject);
            endGame.SetActive(true);
        }
        if (dethNumber == 1)
        {
            lifeObj1.SetActive(false);
        }
        else if(dethNumber == 2)
        {
            lifeObj2.SetActive(false);
        }
    }
    #endregion

}