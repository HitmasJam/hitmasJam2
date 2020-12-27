using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bulletParticle;
    private void Start()
    {
        StartCoroutine(DestroyBullet()) ;
    }




    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            IDamagable obje = other.GetComponent<IDamagable>();
            if (obje != null)
            {
                Instantiate(bulletParticle,transform.position,Quaternion.identity);
                obje.TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
