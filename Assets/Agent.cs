using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    private Rigidbody2D rb2D;
    [SerializeField] static float thrust = 5f;
    [SerializeField] static int vectorQuantity = 5;

    [SerializeField] Vector2[] vectors;

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        vectors = PopulateVector();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.AddForce(vectors[0] * thrust);
    }

    static Vector2[] PopulateVector()
    {
        Vector2[] vectors = new Vector2[vectorQuantity];
        for(int i = 0; i < vectorQuantity; i++)
        {
            vectors[i] = new Vector2(RandomValue(), RandomValue());
        }

        return vectors;
    }

    static float RandomValue()
    {
        bool isNegative = Random.value > 0.5;

        if (isNegative)
            return Random.value * -1;

        else
            return Random.value;
    }
}
