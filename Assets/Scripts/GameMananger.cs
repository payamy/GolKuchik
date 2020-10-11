using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class GameMananger : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        players = FindObjectsOfType<Player>().ToList();
        pointer = players.Count;
        isStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (!isServer)
        //    return;

        players = FindObjectsOfType<Player>().ToList();
        pointer = players.Count;

        if (pointer == 2 && !isStarted)
        {
            players[0].transform.position = new Vector3(0, -4, -1);
            players[1].transform.position = new Vector3(0, +4, -1);
            //ball = new Ball();
            //ball.transform.position = new Vector3(0, 0, -1);
            isStarted = true;
        }
    }

    private List<Player> players;
    private int pointer;
    private bool isStarted;

    public Ball ball;
}
