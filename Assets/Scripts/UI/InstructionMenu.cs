using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionMenu : MonoBehaviour
{
    // Hàm gọi khi nhấn nút Back trên màn hình Instruction
    public void BackToMainMenu()
    {
        // Quay lại Scene MainMenu
        SceneManager.LoadScene("MainMenu");
    }
}
