using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enermyfly : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    [SerializeField] private Transform target;
    [SerializeField] private float follw;
    [SerializeField] private float giukhoangcach;
    [SerializeField] private float khaccach;

  
    public float Flydown;
    public float Flyup;
    private bool _flip;
    public float rightBoundary;
    public float leftBoundary;
    public bool _isMovingRight;
   
    private float timeBtwShot;
    public float startTimeBtwShort;

    public GameObject projectile;
    public Transform Gun;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Hitbox").transform;
       
    }
    void Update()
    {
        move();
    }
    void placeply()
    {
        var currem = transform.position;
        var flyup = Vector2.up;
        var flydown = Vector2.down;
        if (currem.y < Flyup)
        {
            transform.Translate(flyup * Time.deltaTime * speed);
        }
        else if (currem.y > Flydown)
        {
            transform.Translate(flydown * Time.deltaTime * speed);
        }else if (currem.y > Flydown || currem.y >Flyup)
        {

        }
    }
    public void move()
    {
        if (Vector2.Distance(transform.position, target.position) < khaccach)
        {

            hitplayer();
        }
        else
        {
            spawn();
            placeply();
        }
    }
    private void spawn()
    {
        var currenPosition = transform.position;
        if (currenPosition.x > rightBoundary)
        {
            // Niếu vị trí hiện tại của snail > rightBoundary
            // Di chuyển sang trái
            _isMovingRight = false;
        }
        else if (currenPosition.x < leftBoundary)
        {
            // Niếu vi trí hiện tại của Snail < leftBoundary
            // Di chuyển sang phải
            _isMovingRight = true;
        }
        // Di chuyển ngang 
        // (1,0,0) * 1 * 0.02 = (0.02,0,0)
        var direction = Vector3.right;
        if (_isMovingRight == false)
        {
            direction = Vector3.left;
        }
        // var direction = _isMovingRight ? Vector3.right : Vector3.left; cái này bằng if trên
        transform.Translate(direction * speed * Time.deltaTime);
        // Scale Hiện tại
        // Trái < 0 Phải >0
        var currentSacle = transform.localScale;

        if ((_isMovingRight && currentSacle.x < 0) || (_isMovingRight == false && currentSacle.x > 0))
        {
            flip();
        }

    }
    private void hitplayer()
    {
        if (Vector2.Distance(transform.position, target.position) > follw)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
        else if (Vector2.Distance(transform.position, target.position) < follw && Vector2.Distance(transform.position, target.position) > giukhoangcach)
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, transform.position) < giukhoangcach)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, -speed * Time.deltaTime);
        }
        isflip();
        timeBtwShot -= Time.deltaTime;
        if (timeBtwShot <= 0)
        {
            var fire = Instantiate(projectile, Gun.position, Quaternion.identity);
            timeBtwShot = startTimeBtwShort;
            Destroy(fire, 5f);
        }

    }


    private void isflip()
    {
        if (transform.position.x < target.position.x && _flip)
        {
            flip();
        }
        else if (transform.position.x > target.position.x && !_flip)
        {
            flip();
        }
    }
    void flip()
    {
        _flip = !_flip;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
   
}
    


