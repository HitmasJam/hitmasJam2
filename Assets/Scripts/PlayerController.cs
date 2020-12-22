using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : MonoBehaviour, IDamagable
{

    /*
     eventler
    oyun basladı mı

      
     
     */
    public CharacterDatas playerData;
    Camera cam;
   public int healthOfPlayer;
    public Camera Cam { get { return (cam == null) ? cam = Camera.main : cam; } }

    public float movementSpeed=20;
    public enum States { isStarted, notStarted, isStopped,isMoving,isFiring,isRoofClear}
    public static States state;

    public GameObject projectile;
    public float bulletSpeed;
    public GameObject enemy;

    public GameObject[] roofs;
    int counter = 0;
    public LayerMask layerEnemy;
    Vector3 clickPos;
    public Transform bulletPoint;

    void Start()
    {
       //state = States.notStarted;

       roofs = GameObject.FindGameObjectsWithTag("roof");

       roofs = roofs.OrderBy((d) => (d.transform.position - transform.position).sqrMagnitude).ToArray();

        healthOfPlayer = playerData.health;

    }
   
    void Update()
    {
        // oyun basladı mı eventi gelecek
        Movement();
        CheckState();
    }

    void Movement()
    {
        if (state == States.isRoofClear || state == States.isStarted) {
            if (Input.GetMouseButtonDown(0))
            {
                    state = States.isMoving;
                    counter++;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        healthOfPlayer = healthOfPlayer - damage;
        if (healthOfPlayer <= 0)
        {

            //vurulma animasyonu eklenecek
            //Destroy(this.gameObject, 2f);
            Debug.Log("player öldü");
        }
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Cam.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Cam.transform.position.z + transform.position.z+10));

            Vector3 dir = worldPosition - (new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir.Normalize();

            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
        }
    }

    void CheckState()
    {
        if (state == States.isMoving)
        {

            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(roofs[counter].transform.position.x, roofs[counter].transform.position.y+1f, roofs[counter].transform.position.z - 5f), 0.1f);
            MoveCheck();
        }
        if (state == States.isStopped) {
            Shoot();
        }
    }

    void MoveCheck()
    {
        if (transform.position.z == (roofs[counter].transform.position.z -5f))
        {
            Debug.Log("stopped");
            state = States.isStopped;
        }
    }
}
