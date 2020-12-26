using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{

    Animator anim;
    public Animator Anim { get { return (anim == null) ? GetComponent<Animator>() : anim; } }



    public CharacterDatas enemyData;
    public float speedEnemy=10;
    public GameObject playerObject;
    int healthOfEnemy;
  public  float radiusOfEnemy;
    float damageroofCounter = 0;
    public LayerMask playerLayer;
    bool shouldWalk;
    
    void Start()
    {
        EnemyManager.Instance.AddEnemy(this);
        playerObject = GameObject.Find("Kizak");
        shouldWalk = true;
        healthOfEnemy = enemyData.health;   
    }
    void OnDestroy()
    {
        EnemyManager.Instance.RemoveEnemy(this);
       
    }
 
    void Update()
    {
        if (healthOfEnemy>0)
        {
            HitPlayer();


      if(shouldWalk)
            FollowPlayer();
        }
       

    }

    void FollowPlayer()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerObject.transform.position.x,
            transform.position.y,playerObject.transform.position.z),  speedEnemy * Time.deltaTime) ;
        //transform.rotation = Quaternion.LookRotation(playerObject.transform.position);
    }


    void HitPlayer()
    {
        if( Physics.CheckSphere(transform.position, radiusOfEnemy,playerLayer))
        {
           
            shouldWalk = false;
            Anim.SetTrigger("punch");
            damageroofCounter += Time.deltaTime;
            if (damageroofCounter>3) {
                IDamagable obje = playerObject.GetComponent<IDamagable>();
                Debug.Log("dd");
                if (obje != null)
                {
                    obje.TakeDamage(1);
                }
                damageroofCounter = 0;
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
            Destroy(this.gameObject,2f);
            Debug.Log("düşman öldü");
            //EventManager.OnEnemyDie.Invoke();
            
        }
    }



    

}
