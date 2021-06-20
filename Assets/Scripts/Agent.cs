using System.Collections;
using UnityEngine;
using TMPro;

public class Agent : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI nameText;

    private Rigidbody2D rb2D;
    [SerializeField] static float thrust = 0.5f;

    public float distFromEndPoint;
    public string id;

    [SerializeField] static float vectorUpdateFreq = 0.5f;
    [SerializeField] static int vectorQuantity = 5;
    Vector2 currentVector;

    [SerializeField] Vector2[] vectors;

    void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
        if(vectors.Length == 0)
        {
            vectors = PopulateVector();
        }

        
        
        

        UpdateVector();
        StartCoroutine(VectorChanger());
    }

    private void Start()
    {
        nameText.text = id;
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
