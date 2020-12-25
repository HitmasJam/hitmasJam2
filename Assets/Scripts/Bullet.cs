using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

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
