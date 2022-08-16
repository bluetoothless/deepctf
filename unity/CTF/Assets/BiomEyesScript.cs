using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomEyesScript : MonoBehaviour
{
    public GameObject BiomEyePrefab;
    public float lengthBetweenEyes = 1.0f;
    private int[,] eyes = new int [,] {{ 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0 },
                                       { 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0 },
                                       { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },
                                       { 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
                            /*[agent]*/{ 0, 1, 1, 0, 1, 0, 1, 0, 1, 0, 1 },
                                       { 1, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0 },
                                       { 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 0 },
                                       { 0, 1, 0, 1, 0, 1, 0, 1, 0, 0, 0 },
                                       { 1, 0, 1, 0, 1, 0, 1, 0, 0, 0, 0 }};

    // Start is called before the first frame update
    void Start()
    {
        int halfLength = eyes.GetLength(0) / 2;

        for (int i = 0; i < eyes.GetLength(0); i++)
        {
            for (int j = 0; j < eyes.GetLength(1); j++)
            {
                if (eyes[i, j] == 1)
                    SpawnBiomEyeAt(i - halfLength, j);
            }
        }
    }

    public void SpawnBiomEyeAt(int x, int y)
    {
        float xf = x;
        float yf = y;
        GameObject biomEye = Instantiate(BiomEyePrefab, transform);
        //biomEye.transform.SetParent(gameObject.transform);
        // new Vector3(transform.position.x + lengthBetweenEyes * xf, transform.position.y, transform.position.z + lengthBetweenEyes * yf), Quaternion.identity
        
        biomEye.transform.parent = gameObject.transform;
        //biomEye.transform.position = new Vector3(transform.position.x + lengthBetweenEyes * xf, transform.position.y, transform.position.z + lengthBetweenEyes * yf);
        biomEye.transform.localPosition = new Vector3(lengthBetweenEyes * xf, 0, lengthBetweenEyes * yf);
    }
}
