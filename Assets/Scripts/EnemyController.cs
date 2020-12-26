using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{

    Animator anim;
    public Animator Anim { get { return (anim == null) ? GetComponent<Animator>() : anim; } }



    public CharacterDatas enemyData;
    public float speedEnemy=10;
    public GameObject playerObject;
    int healthOfEnemy;
  public  float radiusOfEnemy;
    float damageCounter = 0;
    public LayerMask playerLayer;
    bool shouldWalk;
    bool isDead;
    
    void Start()
    {
        playerObject = GameObject.Find("Kizak");
        shouldWalk = false;
    }

 
    void Update()
    {
        HitPlayer();
        if (PlayerController.state == PlayerController.States.isFiring)
        {
            shouldWalk = true;
        }
        if (shouldWalk && !isDead)
        {
            FollowPlayer(); 
        }

    }

    void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position,playerObject.transform.position,speedEnemy * Time.deltaTime);
        //transform.rotation = Quaternion.LookRotation(playerObject.transform.position);
    }


    void HitPlayer()
    {
        if( Physics.CheckSphere(transform.position, radiusOfEnemy,playerLayer))
        {
           
            shouldWalk = false;
            Anim.SetTrigger("punch");
            damageCounter += Time.deltaTime;
            if (damageCounter>3) {
                IDamagable obje = playerObject.GetComponent<IDamagable>();
                Debug.Log("dd");
                if (obje != null)
                {
                    obje.TakeDamage(1);
                }
                damageCounter = 0;
            }
            //vurma animasyonu
        }
       
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, radiusOfEnemy);
    }



    public void TakeDamage(int damage)
    {
        healthOfEnemy = healthOfEnemy - damage;
        //vurulma anim
        Anim.SetTrigger("hit");
        if (healthOfEnemy<=0)
        {
            Anim.SetTrigger("die");
            //ölme anim
            //Destroy(this.gameObject,2f);
            isDead = true;
            Debug.Log("düşman öldü");
        }
    }



    

}
