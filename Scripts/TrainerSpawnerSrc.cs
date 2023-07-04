using UnityEngine;

public class TrainerSpawnerSrc : MonoBehaviour
{
    public GameObject train;
    public Player player;

    public float spawnInterval;
    public float ypos;

    float cnt = 0;

    private void Update()
    {
        if (!player.isStart) return;
        cnt += Time.deltaTime;

        if (cnt >= spawnInterval)
        {
            int choice = Random.Range(0,3);
            int xpos2;
            int xpos1;

            if (choice == 0)
            {
                xpos1 = -5;
                xpos2 = 0;
            }
            else if (choice == 1)
            {
                xpos1 = 0;
                xpos2 = 5;
            }
            else
            {
                xpos1 = -5;
                xpos2 = 5;
            }


            Destroy(Instantiate(train,new Vector3(xpos1,ypos,transform.position.z),Quaternion.identity),10f);
            Destroy(Instantiate(train,new Vector3(xpos2,ypos,transform.position.z),Quaternion.identity),10f);

            cnt = 0;
        }

    }
}