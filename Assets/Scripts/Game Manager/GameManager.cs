using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Vector3 playerStartPosition;

    public GameObject player;
    public GameObject bringerOfDeath;

    private bool is_bringer_created = false;

    private void Update() {
        if(!is_bringer_created){
            if(player.transform.position.x > 247.0f){
                GameObject newBringer = Instantiate(bringerOfDeath);
                newBringer.transform.position = new Vector3(262.0f, -5.32f, 0);
                is_bringer_created = true;
            }
        }
    }
}