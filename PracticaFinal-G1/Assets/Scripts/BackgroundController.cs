using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    private float startPos, length;
    public GameObject cam;
    public float parallaxEffect;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float distancia = cam.transform.position.x * parallaxEffect;
        float movimiento = cam.transform.position.x * (1 - parallaxEffect);

        transform.position = new Vector3(startPos + distancia, transform.position.y, transform.position.z);
            
        if (movimiento > startPos + length)
        {
            startPos += length;
        }
        else if (movimiento < startPos - length)
        {
            startPos -= length;
        }
    }
}
