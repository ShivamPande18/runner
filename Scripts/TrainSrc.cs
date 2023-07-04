using UnityEngine;

public class TrainSrc : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Translate(-Vector3.forward * speed * Time.deltaTime);
    }
}