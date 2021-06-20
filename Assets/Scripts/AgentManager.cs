using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentManager : MonoBehaviour
{
    [SerializeField] Agent agentPrefab;
    [SerializeField] int agentQuantity = 5;
    Agent[] agents;
    // Start is called before the first frame update
    void Start()
    {
        InitAgents();

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
            agents[i] = Instantiate(agentPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }
    }

     
}
