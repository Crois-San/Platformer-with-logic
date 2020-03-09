using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject player;
    private Vector2 playerPosition;
    [SerializeField]
    private float speedModifier;
    void Start()
    {
        player = GameObject.Find("Character");
    }

    // Update is called once per frame
    void Update()
    {
        if (player)
        {
            playerPosition = player.transform.position;
                    transform.position =
                        new Vector3(Mathf.Lerp(transform.position.x, playerPosition.x, Time.deltaTime * speedModifier) ,
                            Mathf.Lerp(transform.position.y, playerPosition.y, Time.deltaTime * speedModifier),transform.position.z);
        }
    }
}
