using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartBullet : MonoBehaviour
{
    Rigidbody2D rb;
    int damage = 0;
    float bulletSpeed = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("??");
            rb.velocity = Vector2.zero;
            TrapManager.Instance.ReturnDartBullet(this);
        }
        else if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("????");
            rb.velocity = Vector2.zero;
            collision.gameObject.GetComponent<Enemy>().GetDamage(damage);
            TrapManager.Instance.ReturnDartBullet(this);
        }
    }

    public void MoveBullet(Vector3 euler, int _damage)
    {
        if(rb == null) 
            rb = GetComponent<Rigidbody2D>();
        
        damage = _damage;

        if (-1 <= euler.z && euler.z <= 1) // left
        {
            this.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 270));
            rb.velocity = new Vector2(-bulletSpeed, 0);
        }
        else if (89 <= euler.z && euler.z <= 91) // down
        {
            this.transform.localRotation = Quaternion.Euler(Vector3.zero);
            rb.velocity = new Vector2(0, -bulletSpeed);
        }
        else if (179 <= euler.z && euler.z <= 181) //right 
        {
            this.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 90));
            rb.velocity = new Vector2(bulletSpeed, 0);
        }
        else if (269 <= euler.z && euler.z <= 271) // up
        {
            this.transform.localRotation = Quaternion.Euler(new Vector3(0,0,180));
            rb.velocity = new Vector2(0, bulletSpeed);
        }
    }
}
