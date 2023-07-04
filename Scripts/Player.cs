using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public LevelGenerator levelGenerator;
    public Animator anim;
    public TMP_Text scoreTxt;
    public TMP_Text highScoreTxt;
    public TMP_Text scoreTxt2;
    public TMP_Text highScoreTxt2;

    public GameObject startScreen;
    public GameObject endScreen;

    public float gravityScale = 10;
    public float speed;
    public float jumpForce;

    public bool isStart;
    public Vector3 nextPos;

    int lane;
    int score;
    int highScore;
    Rigidbody rb;

    private void Start()
    {
        highScore =  PlayerPrefs.GetInt("highScore");
        highScoreTxt.text = "HighScore: " + highScore.ToString();
        score = 0;
        lane = 1;
        rb = GetComponent<Rigidbody>();
        isStart = false;
    }

    private void FixedUpdate()
    {
        if (!isStart) return;
        rb.AddForce(Physics.gravity * (gravityScale - 1) * rb.mass);
        transform.position = Vector3.Lerp(transform.position, new Vector3(nextPos.x,transform.position.y,transform.position.z),10*Time.fixedDeltaTime);
    }

    private void Update()
    {
        if (!isStart) return;
        scoreTxt.text = score.ToString();
        rb.velocity = new Vector3(rb.velocity.x,rb.velocity.y,speed) * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (lane !=0)
            {
                nextPos -= new Vector3(5, 0, 0);
                lane--;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (lane!=2)
            {
                lane++;
                nextPos += new Vector3(5, 0, 0);
            }
        }

        if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
            //anim.SetTrigger("jump");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.CompareTag("train"))
        {
            if (score > highScore) PlayerPrefs.SetInt("highScore",score);
            endScreen.SetActive(true);
            scoreTxt2.text = "Score: " + score.ToString();
            highScoreTxt2.text = "HighScore: " + PlayerPrefs.GetInt("highScore").ToString();
            Destroy(gameObject);
            print("Game Over");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("levelChange")) levelGenerator.SpawnLevelPiece();
        if(other.gameObject.CompareTag("coin"))
        {
            Destroy(other.gameObject);
            score++;
        }

    }

    public void OnPlay()
    {
        startScreen.SetActive(false);
        isStart = true;
        transform.position = new Vector3(0, 0, 2);
    }

    public void OnReplay()
    {
        SceneManager.LoadSceneAsync(0);
    }
}