using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField][Range(0f, 50f)] float timeSpeed = 1f;
    private float fixedDeltaTime;
    void Awake()
    {
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timeSpeed;
        Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
    }
}
