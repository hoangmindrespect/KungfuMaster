using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
public class MenuFunction : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Scene1");
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

        // Lưu vị trí player vào GameManager
        GameManager.playerStartPosition = new Vector3(data.playerPosition[0], data.playerPosition[1], data.playerPosition[2]);

        // Tải cảnh mới
        SceneManager.LoadScene("Scene1");
    }
}
