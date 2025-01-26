using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance;

    

    private void Awake()
    {
        Instance = this;


    
        DontDestroyOnLoad(gameObject); 
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) & this.getCurrentScene() != "MainScene"){
            this.loadScene("MainScene");
        }
    }

    public string getCurrentScene(){
        return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
    }

    public void loadScene(string sceneName){
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    public void reloadCurrentScene(){
        this.loadScene(this.getCurrentScene());

        
    }

    public void quitApplication(){
        Application.Quit();
    }
}
