using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    private GameObject cam;
    [SerializeField] private float parallaxEffect;
    private float xPosition;
    private float length;

    void Start()
    {
        cam = GameObject.Find("Main Camera");
        xPosition = transform.position.x;

        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        float distanceMoved = cam.transform.position.x * (1 - parallaxEffect);
        float distanceMove = cam.transform.position.x * parallaxEffect;
        transform.position = new Vector2(xPosition + distanceMove, transform.position.y);

        if (distanceMoved > xPosition + length)
            xPosition = xPosition + length;
        else if (distanceMoved < xPosition - length)
            xPosition = xPosition - length;
    }
}
