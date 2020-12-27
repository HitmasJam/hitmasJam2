using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour, IDamagable
{

    /*
     eventler
    oyun basladı mı

      
     
     */

    [SerializeField]
    private float dieSpeed = 50f;

    Animator anim;

    GameObject droppedGift;


    public CharacterDatas playerData;
    Camera cam;
    public int healthOfPlayer;
    public HealthBar healthBar;
    public Camera Cam { get { return (cam == null) ? cam = Camera.main : cam; } }

    public float movementSpeed = 20;
    public enum States { isStarted, notStarted, onRoof, isStopped, isMoving, isFiring, isRoofClear, gameOver, droppingGift }
    public static States state;

    public GameObject projectile;
    public float bulletSpeed;
    public GameObject enemy;

    public List<GameObject> giftBoxes = new List<GameObject>();



    int roofCounter = 0;
    public LayerMask layerEnemy;
    Vector3 clickPos;
    public Transform bulletPoint;


    private void OnEnable()
    {
        EventManager.OnGameStart.AddListener(GameStart);
        EventManager.OnGameOver.AddListener(GameOver);
        EventManager.OnRoof.AddListener(OnRoofState);
        EventManager.OnRoofClear.AddListener(RoofClear);
        EventManager.OnDropGift.AddListener(DropGift);
    }
    private void OnDisable()
    {
        EventManager.OnGameOver.RemoveListener(GameStart);
        EventManager.OnGameOver.RemoveListener(GameOver);
        EventManager.OnRoof.RemoveListener(OnRoofState);
        EventManager.OnRoofClear.RemoveListener(RoofClear);
        EventManager.OnDropGift.RemoveListener(DropGift);

    }



    void Start()
    {
        anim = transform.GetChild(0).GetComponent<Animator>();
        //state = States.notStarted;



        healthOfPlayer = playerData.health;
        healthBar.SetMaxHealth(playerData.health);

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
        if (state == States.isRoofClear || state == States.isStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                state = States.isMoving;

            }
        }
    }
    public void TakeDamage(int damage)
    {
        healthOfPlayer = healthOfPlayer - damage;
        healthBar.SetHealth(healthOfPlayer);
        if (healthOfPlayer <= 0)
        {
            EventManager.OnGameOver.Invoke();

            //vurulma animasyonu eklenecek
            //Destroy(this.gameObject, 2f);
            Debug.Log("player öldü");
        }
    }
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetBool("isShooting", true);
            Vector3 worldPosition = Cam.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x,
            Input.mousePosition.y,
            -Cam.transform.position.z + transform.position.z + 10));

            Vector3 dir = worldPosition - (new Vector3(transform.position.x, transform.position.y, transform.position.z));
            dir.Normalize();

            GameObject bullet = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
            bullet.GetComponent<Rigidbody>().velocity = dir * bulletSpeed;
        }
        else
        {
            anim.SetBool("isShooting", false);
        }
    }

    void CheckState()
    {
        if (state == States.isMoving)
        {

            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(GameManager.Instance.roofs[roofCounter].transform.position.x,
                GameManager.Instance.roofs[roofCounter].transform.position.y + 1f, GameManager.Instance.roofs[roofCounter].transform.position.z - 5f), 0.075f);
            MoveCheck();
        }
        else if (state == States.isStopped)
        {
            StartCoroutine(FiringState());

        }
        else if (state == States.onRoof)
        {
            Shoot();
        }
        else if (state == States.gameOver)
        {
            Debug.Log("sds");
            transform.Translate(Vector3.forward * Time.deltaTime * dieSpeed, Space.World);
        }
        else if (state == States.isRoofClear)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                new Vector3(GameManager.Instance.flues[roofCounter].transform.position.x, GameManager.Instance.flues[roofCounter].transform.position.y + 5f,
                GameManager.Instance.flues[roofCounter].transform.position.z), 0.1f);
            if (transform.position.x == GameManager.Instance.flues[roofCounter].transform.position.x)
            {
                EventManager.OnDropGift.Invoke();

            }
        }
        else if (state == States.droppingGift)
        {

        }
    }


    void MoveCheck()
    {
        //çatıya ulaşma durumu
        if (transform.position.z == (GameManager.Instance.roofs[roofCounter].transform.position.z - 5f))
        {

            EventManager.OnRoof.Invoke();
            //hediye atınca arttır
            //roofCounter++;
        }
    }


    void GameStart()
    {
        state = States.isMoving;
    }







    void OnRoofState()
    {
        state = States.onRoof;
    }

    void RoofClear()
    {
        state = States.isRoofClear;
    }


    void DropGift()
    {
        droppedGift = Instantiate(giftBoxes[Random.Range(0, 5)], transform.position, Quaternion.identity);
        
       
        state = States.droppingGift;
       
        if (roofCounter != GameManager.Instance.roofs.Length-1)
        {
            StartCoroutine(WaitGiftDrop());
            roofCounter++;
        }
        else
        {
            EventManager.OnLevelFinish.Invoke();
        }
    }






    void GameOver()
    {
        state = States.gameOver;
    }




    IEnumerator WaitGiftDrop()
    {
        yield return new WaitForSeconds(1.15f);

            state = States.isMoving;
        
        yield return new WaitForSeconds(2);
        Destroy(droppedGift);

    }

    IEnumerator FiringState()
    {
        yield return new WaitForEndOfFrame();
        state = States.isFiring;
    }
}
