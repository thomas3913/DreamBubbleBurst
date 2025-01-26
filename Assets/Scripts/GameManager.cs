using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public Texture2D cursorTexture;
    public CursorMode cursorMode = CursorMode.Auto;

    private void Awake()
    {
        if (cursorTexture != null) Cursor.SetCursor(cursorTexture, Vector2.zero, cursorMode);

        DontDestroyOnLoad(gameObject); 
    }

    private void Update()
    {
        
        
    }


}



