using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
public class MenuFunction : MonoBehaviour
{
    public void NewGame(){
        SceneManager.LoadScene("Game");
    }

    public void Continue()
    {
        // Tải thông tin vị trí từ file save
        PlayerData data = SaveSystem.LoadGame();
        
        // Kiểm tra nếu dữ liệu đã được tải thành công
        if (data == null)
        {
            Debug.LogError("Failed to load player data.");
            return;
        }

        // Kiểm tra nếu GameManager.Instance không null
        // if (GameManager.Instance == null)
        // {
        //     Debug.LogError("GameManager instance is not initialized.");
        //     return;
        // }

        // Lưu vị trí player vào GameManager
        GameManager.playerStartPosition = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);

        // Tải cảnh mới
        SceneManager.LoadScene("Game");
    }

    // private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     if (scene.name == "Game")
    //     {
    //         // Đảm bảo chỉ đặt vị trí cho player khi cảnh "Game" được tải
    //         Player player = FindObjectOfType<Player>();
    //         if (player != null)
    //         {
    //             player.transform.position = GameManager.Instance.playerStartPosition;
    //         }

    //         // Hủy đăng ký sự kiện để không gây ra lỗi nhiều lần
    //         SceneManager.sceneLoaded -= OnSceneLoaded;
    //     }
    // }
}
