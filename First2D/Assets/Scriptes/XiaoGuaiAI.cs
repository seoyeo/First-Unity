using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XiaoGuaiAI : MonoBehaviour {

    private bool isAwake = false;
    public Transform start;
    public Transform end;
    private float speed = 3;
    private bool isRight;
    private bool isLeft;
    private Animator xiaoguaiAnimator;

    void Start()
    {
        xiaoguaiAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        xiaoguaiAnimator.SetBool("isLeft", isLeft);
        xiaoguaiAnimator.SetBool("isRight", isRight);
        CheckPlayerDistance();
        if (isAwake)
        {
            isLeft = true;
            isRight = false;
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, start.position, step);
       }
        if (isAwake ==false)
        {
            isLeft = false;
            isRight = true;
            float step = speed * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, end.position, step);
        }
       
    }

    void CheckPlayerDistance()
    {
        if (Vector3.Distance( start.position, this.transform.position) < 1.5)
        {
            isAwake = false;
        }
        else if (Vector3.Distance( end.transform.position, this.transform.position) < 1.5)
        {
            isAwake = true;
        }
    }
}
