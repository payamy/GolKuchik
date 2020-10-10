using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class CameraController : NetworkBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        players = FindObjectsOfType<Player>();
        foreach (Player p in players)
            if (p.isLocalPlayer)
                transform.position = new Vector3(p.transform.position.x,
                                                 p.transform.position.y,
                                                 -10);
    }

    private Player[] players;
}
