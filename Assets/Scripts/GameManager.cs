using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{

    private bool isGameOver { get; set; } = false;

    #region Events 
    public UnityEvent<bool> gameOver = new UnityEvent<bool>();
    #endregion

    private void MoveScene()
    {
        // Movimiento de tiles para simular avance
    }


}
