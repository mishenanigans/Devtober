using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingSpot : MonoBehaviour
{
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        if (player == null)
		{
			player = GameObject.Find("Player").GetComponent<Player>();
		}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.tag == "Player")
        {
            if (player != null && player.IsCrouching == true )
            {
                player.Hiding();
            }
        }
    }
}
