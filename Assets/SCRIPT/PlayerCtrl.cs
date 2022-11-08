using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerCtrl : MonoBehaviour
{
    public float speed;
    float resetSpeed;
    public float dashSpeed, dashTime;

    PhotonView view;
    Animator anim;

    LineRenderer rend;
    Health healthScript;


    Vector2 _border;
    void Start()
    {
        resetSpeed = speed;
        view = GetComponent<PhotonView>();
        anim = GetComponent<Animator>();
        rend = FindObjectOfType<LineRenderer>();
        healthScript = FindObjectOfType<Health>();

        _border = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    
    void Update()
    {
        if (view.IsMine)
        {
            Wrap();
            Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            Vector2 moveAmount = moveInput.normalized * speed * Time.deltaTime;
            transform.position += (Vector3)moveAmount;
            
            if (Input.GetKeyDown(KeyCode.Space) && moveInput != Vector2.zero)
            {
                StartCoroutine(Dash());
            }
            if (moveInput == Vector2.zero)
            {
                anim.SetBool("IsRun", false);
            }
            else
            {
                anim.SetBool("IsRun", true);
            }
            anim.SetFloat("Horizontal", moveInput.x);
            anim.SetFloat("Vertical", moveInput.y);
            rend.SetPosition(0, transform.position);
           
        }
        else
        {
            rend.SetPosition(1, transform.position);
        }
    }
    private void OnTriggerEnter2D(Collider2D orther)
    {
        if (view.IsMine)
        {
            if (orther.tag == "Enemy")
            {
                healthScript.TakeDamage();
            }
        }
    }

    IEnumerator Dash()
    {
        speed = dashSpeed;
        yield return new WaitForSeconds(dashTime);
        speed = resetSpeed;
    }
    void Wrap()
    {
        if(transform.position.x < -_border.x)
        {
            transform.position = new Vector2(_border.x, transform.position.y);
        }
        if (transform.position.x > _border.x)
        {
            transform.position = new Vector2(-_border.x, transform.position.y);
        }
        if (transform.position.y < -_border.y)
        {
            transform.position = new Vector2(transform.position.x, _border.y);
        }
        if (transform.position.y > _border.y)
        {
            transform.position = new Vector2(transform.position.x, -_border.y);
        }
    }
}
