using System.Collections;
using UnityEngine;
using TMPro;

public class Agent : MonoBehaviour
{
    [SerializeField] AgentManager manager;
    [SerializeField] TextMeshProUGUI nameText;
    private Rigidbody2D rb2D;

    public string id;
    public float distFromEndPoint;

    Vector2 currentVector;
    int vectorIndex = 0;
    [SerializeField] public Vector2[] vectors;

    void Awake()
    {
        manager = FindObjectOfType<AgentManager>();
        rb2D = gameObject.GetComponent<Rigidbody2D>();

        if (vectors.Length == 0)
        { // if the agent exsisted before, don't give it new random vectors
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
        rb2D.AddForce(currentVector * manager.agentThrust); // moves the agent based on the vector direction
    }

     Vector2[] PopulateVector()
    {
        Vector2[] vectors = new Vector2[manager.agentVectorQuantity]; // creates new array of vectors

        for(int i = 0; i < manager.agentVectorQuantity; i++)
            vectors[i] = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)); // populates array with random vectors

        return vectors;
    }

    IEnumerator VectorChanger()
    {
        while(true)
        {
            yield return new WaitForSeconds(2);
            UpdateVector();
        }
    }

    void UpdateVector()
    {
        if (manager.randomVectorUpdating)
            currentVector = vectors[(int)Random.Range(0f, (float)manager.agentVectorQuantity)];
        else
        {
            currentVector = vectors[vectorIndex];
            if (vectorIndex == manager.agentVectorQuantity - 1)
                vectorIndex = 0;
            else
                vectorIndex++;

        }
    }

    public void Destroy(float delay = 0)
    {
        Destroy(this.gameObject, delay);
    }
}
