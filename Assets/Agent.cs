using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Agent : MonoBehaviour
{

    private Rigidbody2D rb2D;
    [SerializeField] static float thrust = 0.5f;

    [SerializeField] static float vectorUpdateFreq = 1f;
    [SerializeField] static int vectorQuantity = 5;
    Vector2 currentVector;

    [SerializeField] Vector2[] vectors;

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        vectors = PopulateVector();
        UpdateVector();
        StartCoroutine(VectorChanger());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb2D.AddForce(currentVector * thrust);
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

    IEnumerator VectorChanger()
    {
        while(true)
        {
            yield return new WaitForSeconds(vectorUpdateFreq);
            UpdateVector();
        }
    }

    void UpdateVector()
    {
        currentVector = vectors[(int)Random.Range(0f, (float)vectorQuantity)];
    }
}
