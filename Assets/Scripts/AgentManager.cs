using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
            agent.distFromEndPoint = Vector2.Distance(spawnPoint.transform.position, agent.transform.position);

        agents = agents.OrderBy(x => x.distFromEndPoint).ToArray();
 

        // remove the 2 agents who are the furthest from end point

        // average vectors of the 4 best agents

        // fill in array with the 2 new offspring

        // add a random new agent in there to diversify
    }

}
