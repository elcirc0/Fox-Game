using UnityEngine;
public class BackGroundParallax : MonoBehaviour
{
    [SerializeField] private float length;


    private float startPos;

    [SerializeField] private GameObject cam;


    [SerializeField][Range(0f, 1f)] private float parallaxEffect;



    void Start()
    {
        startPos = transform.position.x;
    }


    void Update()
    {
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        float dist = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

        if (temp > startPos + length)
            startPos += length;
        else if (temp < startPos - length)
            startPos -= length;
    }
}
