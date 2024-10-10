using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static Vector3 playerStartPosition;

    public GameObject player;
    public GameObject bringerOfDeath;

    private bool is_bringer_created = false;

    private void Update()
    {
        if (!is_bringer_created)
        {
            if (player.transform.position.x > 280.0f)
            {
                if (Input.GetButtonDown("Boss"))
                {
                    SceneManager.LoadScene("Boss1");
                    is_bringer_created = true;
                }
            }
        }
    }

    private void OnApplicationFocus(bool focus) // This used to reset money when player turn the game off
    {
        if(!focus)
        {
            if (PlayerPrefs.HasKey("money"))
                PlayerPrefs.DeleteKey("money");
        }
    }
}