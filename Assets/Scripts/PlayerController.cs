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

    [SerializeField]
    private float dieSpeed = 50f;

    Animator anim;
    



    public CharacterDatas playerData;
    Camera cam;
   public int healthOfPlayer;
    public Camera Cam { get { return (cam == null) ? cam = Camera.main : cam; } }

    public float movementSpeed=20;
    public enum States { isStarted, notStarted, isStopped,isMoving,isFiring,isRoofClear,gameOver}
    public static States state;

    public GameObject projectile;
    public float bulletSpeed;
    public GameObject enemy;

    public GameObject[] roofs;
    int counter = 0;
    public LayerMask layerEnemy;
    Vector3 clickPos;
    public Transform bulletPoint;


    private void OnEnable()
    {
        EventManager.GameOver.AddListener(GameOver);
    }
    private void OnDisable()
    {
        EventManager.GameOver.AddListener(GameOver);
        
    }



    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
       //state = States.notStarted;

       roofs = GameObject.FindGameObjectsWithTag("roof");

       roofs = roofs.OrderBy((d) => (d.transform.position - transform.position).sqrMagnitude).ToArray();

        healthOfPlayer = playerData.health;

    }
   
    void Update()
    {
        Debug.Log(state);
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
            EventManager.GameOver.Invoke();
            
            //vurulma animasyonu eklenecek
            //Destroy(this.gameObject, 2f);
            Debug.Log("player öldü");
        }
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isShooting",true);
            Vector3 worldPosition = Cam.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Cam.transform.position.z + transform.position.z+10));

            Vector3 dir = worldPosition - (new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir.Normalize();

            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
        }
        else
        {
            anim.SetBool("isShooting",false);
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
       else if (state == States.isStopped) {
            StartCoroutine(FiringState());
           
        }
        else if (state==States.isFiring)
        {
            Shoot();
        }
        else if (state == States.gameOver)
        {
            transform.Translate(Vector3.forward * Time.deltaTime * dieSpeed, Space.World);
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
    void GameOver()
    {
        state = States.gameOver;
    }
    
    
    
    
    
    IEnumerator FiringState()
    {
        yield return new WaitForEndOfFrame();
        state = States.isFiring;
    }
}
