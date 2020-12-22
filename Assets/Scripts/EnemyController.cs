using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IDamagable
{

    public CharacterDatas enemyData;
    public float speedEnemy=10;
    public GameObject playerObject;
    int healthOfEnemy;
  public  float radiusOfEnemy;
    float damageCounter = 0;
    public LayerMask playerLayer;
    
    void Start()
    {
        healthOfEnemy = enemyData.health;   
    }

 
    void Update()
    {
        if (PlayerController.state == PlayerController.States.isStopped)
        {
            FollowPlayer();
            HitPlayer();
        }


    }

    void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position,playerObject.transform.position,speedEnemy * Time.deltaTime);
    }


    void HitPlayer()
    {
        if( Physics.CheckSphere(transform.position, radiusOfEnemy,playerLayer))
        {
           
            damageCounter += Time.deltaTime;
            if (damageCounter>3) {
                IDamagable obje = playerObject.GetComponent<IDamagable>();
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
        if (healthOfEnemy<=0)
        {

            //vurulma animasyonu eklenecek
            //Destroy(this.gameObject,2f);
            Debug.Log("düşman öldü");
        }
    }



    

}
