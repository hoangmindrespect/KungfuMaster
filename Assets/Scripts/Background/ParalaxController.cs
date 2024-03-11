using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    Transform cam; // Main Camera
    Vector3 camStartPos;

    GameObject[] backgrounds;
    Material[] mat;
    float[] backSpeed;

    float farthestBack;

    [Range(0.01f, 0.05f)]
    public float parallaxSpeed;

    void Start()
    {
        cam = Camera.main.transform;
        camStartPos = cam.position;

        int backCount = transform.childCount;
        mat = new Material[backCount];
        backSpeed = new float[backCount];
        backgrounds = new GameObject[backCount];

        for (int i = 0; i < backCount; i++)
        {
            backgrounds[i] = transform.GetChild(i).gameObject;
            mat[i] = backgrounds[i].GetComponent<Renderer>().material;
        }
        BackSpeedCalculate(backCount);
    }

    void BackSpeedCalculate(int backCount)
    {
        farthestBack = float.MinValue; // Initialize to the smallest possible value
        for (int i = 0; i < backCount; i++) // find the farthest background
        {
            float distance = backgrounds[i].transform.position.z - cam.position.z;
            if (distance > farthestBack)
            {
                farthestBack = distance;
            }
        }

        for (int i = 0; i < backCount; i++) // set the speed of backgrounds
        {
            backSpeed[i] = 1 - (backgrounds[i].transform.position.z - cam.position.z) / farthestBack;
        }
    }



    void FixedUpdate() 
    {        
        transform.position = new Vector3(cam.position.x, transform.position.y, 0);

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float speed = backSpeed[i] * parallaxSpeed;
            mat[i].SetTextureOffset("_MainTex", new Vector2(cam.position.x - camStartPos.x, 0) * speed);
        }
    }
}
