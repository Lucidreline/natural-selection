using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AgentManager : MonoBehaviour
{

    static int IdCounter = 0;

    [SerializeField] GameObject spawnPoint;
    [SerializeField] GameObject endPoint;

    [SerializeField] float generationDurration = 10f;

    [SerializeField] Agent agentPrefab;
    [SerializeField] int agentQuantity = 5;

    Agent[] agents;
    // Start is called before the first frame update
    void Start()
    {
        InitAgents();
        StartCoroutine(GenerationChanger());

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitAgents()
    {
        agents = new Agent[agentQuantity];

        for(int i = 0; i < agentQuantity; i++)
        {
            agents[i] = Instantiate(agentPrefab, spawnPoint.transform.position, Quaternion.identity);
            agents[i].name = IdCounter.ToString();
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
        {
            agent.distFromEndPoint = Vector2.Distance(spawnPoint.transform.position, agent.transform.position);
        }

        agents = agents.OrderBy(x => x.distFromEndPoint).ToArray();
 

        // remove the 2 agents who are the furthest from end point

        // average vectors of the 4 best agents

        // fill in array with the 2 new offspring

        // add a random new agent in there to diversify
    }

}
