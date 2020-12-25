using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemy"))
        {
            IDamagable obje = other.GetComponent<IDamagable>();
            if (obje != null)
            {
                obje.TakeDamage(1);
            }
        }
    }
}
