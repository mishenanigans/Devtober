using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionCone : MonoBehaviour
{
    private Player player;
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
		{
			Player player = other.transform.GetComponent<Player>();

			if (player != null && player.IsHiding == false)
			{
				player.Detected();
			}
        }
    }
}
