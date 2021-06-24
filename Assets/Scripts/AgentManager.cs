using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;

public class AgentManager : MonoBehaviour
{

    

    [Header("Locations")]
    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject endPoint;

    [Header("Agent")]
    [SerializeField] Agent agentPrefab;
    [SerializeField] int agentQuantity = 5;
    [SerializeField] public float agentThrust = 0.5f;
    [SerializeField] public float agentVectorUpdateFreq = 0.5f;
    [SerializeField] public int agentVectorQuantity = 5;

    Agent[] agents;
    int IdCounter = 0;

    [Header("Generation")]
    [SerializeField] float generationDurration = 10f;
    [SerializeField] float mutationPercent = 0.5f;
    [SerializeField] TextMeshProUGUI generationCounterText;
    public int generationCount = 1;


    // Start is called before the first frame update
    void Start()
    {
        InitAgents();
        StartCoroutine(GenerationChanger());
    }

    void InitAgents()
    {
        agents = new Agent[agentQuantity];

        for(int i = 0; i < agentQuantity; i++)
        {
            agents[i] = Instantiate(agentPrefab, spawnPoint.transform.position, Quaternion.identity);
            agents[i].id = IdCounter.ToString();
            agents[i].name = "Agent " + IdCounter.ToString();
            IdCounter++;
        }
    }

    void SpawnGeneration() 
    {
        foreach(Agent agent in agents)
        {
            agent.transform.position = spawnPoint.transform.position;
        }
    }


    IEnumerator GenerationChanger()
    {
        while (true)
        {
            yield return new WaitForSeconds(generationDurration);
            UpdateGeneration();
        }
    }

    void UpdateGeneration()
    {
        // find the distance from each agent to end point
        foreach(Agent agent in agents)
            agent.distFromEndPoint = Vector2.Distance(endPoint.transform.position, agent.transform.position);

        agents = agents.OrderBy(x => x.distFromEndPoint).ToArray();

        EvolveGeneration();

        generationCount++;
        generationCounterText.text = "Generation " + generationCount.ToString();

        // add a random new agent in there to diversify
    }

    void EvolveGeneration()
    {
        // int numberOfMutations = (int)((1 / 3) * ((float) agents.Length));
        int numberOfMutations = (int)(agents.Length / 3);

        for(int i = 1; i <= numberOfMutations; i++)
        {
            int parentAgentIndex = (2 * i) - 2;
            Agent agent1 = agents[parentAgentIndex];
            Debug.Log(agent1.id + " Will be parent 1");
            Agent agent2 = agents[parentAgentIndex + 1];
            Debug.Log(agent2.id + " Will be parent 2");

            int newAgentIndex = agents.Length - i;
            Debug.Log("Destroy Agent " + agents[newAgentIndex].id);
            agents[newAgentIndex].Destroy();

            agents[newAgentIndex] = Instantiate(agentPrefab, new Vector3(1000, 1000, 0), Quaternion.identity);
            agents[newAgentIndex].vectors = CreateOffSpring(agent1, agent2);
            agents[newAgentIndex].id = IdCounter.ToString();
            agents[newAgentIndex].name = "Agent " + IdCounter.ToString();
            IdCounter++;
            Debug.Log("Created " + agents[newAgentIndex].name);
        }

        SpawnGeneration();
    }

    Vector2[] CreateOffSpring(Agent agent1, Agent agent2)
    {
        int length = agent1.vectors.Length;
        
        int howManyVectorsToCombine = (int) (mutationPercent * length);

        int[] indexesToCombine = new int[howManyVectorsToCombine];

        for(int i = 0; i < howManyVectorsToCombine; i++)
        {
            indexesToCombine[i] = Random.Range(0, length);
        }

        Vector2[] mutations = new Vector2[length];

        for(int i = 0; i < length; i++)
        {
            if (indexesToCombine.Contains(i))
            {
                float x = (agent1.vectors[i].x + agent2.vectors[i].x) / (float)2;
                float y = (agent1.vectors[i].y + agent2.vectors[i].y) / (float)2;
                mutations[i] = new Vector2(x, y);
            }
            else
            {
                mutations[i] = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            }
        }

        return mutations;
    }
    
}
