using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackRotation : MonoBehaviour
{
    private Transform Player;
    private Animator Animator;
    private Rigidbody2D rb;
    
    public Vector3 attackOffset;
    public float attackRange = 1f;
    public int attackDamage = 10;
    public LayerMask attackMask;


    public Transform meleeTransform;
    public Transform rangeTransform;
    public Transform laserTransform;

    public GameObject rangeBullet;
    public float bulletSpeed = 3f;

    public GameObject laserBeam;
    public bool isParry = false;
    public bool isAttack = false;
    void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
        Animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    public void Parry()
    {
        isParry = true;
        Invoke("ParryComplete", 2f);
    }

    public void LaserAttack()
    {
        Vector3 direction = Player.position - laserTransform.position;
        float distance = direction.magnitude;
        direction.Normalize(); // Chuẩn hóa hướng

        // Tạo laser tại vị trí của laserTransform
        var laser = Instantiate(laserBeam, laserTransform.position, Quaternion.identity);

        // Điều chỉnh kích thước của laser
        laser.transform.localScale = new Vector3(distance, laser.transform.localScale.y, laser.transform.localScale.z);

        // Tính toán góc quay của laser
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        laser.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        // Tạo và điều chỉnh collider cho laser
        BoxCollider2D laserCollider = laser.GetComponent<BoxCollider2D>();
        if (laserCollider != null)
        {
            // Điều chỉnh kích thước và vị trí của collider
            laserCollider.size = new Vector2(distance, laserCollider.size.y);
            laserCollider.offset = new Vector2(distance / 2, 0); // Đặt collider ở giữa
        }

        // Hủy laser sau một khoảng thời gian (ví dụ: 2 giây)
        Destroy(laser, 2f);

    }


    public void MeleeAttack()
    {
        Vector3 pos = meleeTransform.position;
        pos += meleeTransform.right * attackOffset.x;
        pos += meleeTransform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if(colInfo != null)
        {
            colInfo.GetComponent<PlayerHealthBar>().takeDamage(attackDamage);
        }
    }
    
    public void RangeAttack()
    {
        var bullet = Instantiate(rangeBullet, rangeTransform.position, Quaternion.identity);
        Rigidbody2D rbBullet = bullet.GetComponent<Rigidbody2D>();

        Vector3 direction = Player.position - rangeTransform.position;
        direction.z = 0; // Đảm bảo hướng di chuyển chỉ trên mặt phẳng 2D
        direction.Normalize(); // Chuẩn hóa vector hướng

        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        rbBullet.velocity = direction * bulletSpeed; // Thay đổi vận tốc của Rigidbody2D
        StartCoroutine(InvokeDestroyGameObject(bullet, 2f));
    }

    IEnumerator InvokeDestroyGameObject(GameObject g, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(g);
    }

    public void ParryComplete()
    {
        isParry = false;
    }

    public void AttackComplete()
    {
        isAttack = false;
    }

}
