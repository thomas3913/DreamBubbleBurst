using UnityEngine;
using UnityEngine.UI;
public class BildMovement : MonoBehaviour
{
    public Button playButton; // Der Play-Button, der die Bildbewegung ausl�st
    public RectTransform imageTransform; // Das RectTransform des Bildes
    public float moveSpeed = 3.5f; // Geschwindigkeit, mit der das Bild bewegt wird
    private Vector3 startPosition;
    private Vector3 targetPosition; // Zielposition, an die das Bild verschoben werden soll

    private bool clicked;

    private bool moveDownStarted;

    void Start()
    {
        clicked = false;
        // Setze die Zielposition auf die Position, bei der das Bild ganz nach oben verschoben wird
        targetPosition = new Vector3(imageTransform.position.x, 0, imageTransform.position.z); // Zielposition oben
        startPosition = new Vector3(imageTransform.position.x, imageTransform.position.y, imageTransform.position.z); // Zielposition oben

    }

    void Update()
    {
        // Wenn das Bild zu seiner Zielposition bewegt werden soll
        if (imageTransform.position.y > targetPosition.y & clicked)
        {

            // Bewege das Bild nach oben (auf die Zielposition zu)
            float step = moveSpeed * Time.deltaTime;
            imageTransform.position = Vector3.MoveTowards(imageTransform.position, targetPosition, step);
        }

        if(Input.GetKeyDown(KeyCode.Escape) & SceneManager.Instance.getCurrentScene() == "MainScene"){
            this.moveDown();
        }

        if (imageTransform.position.y < startPosition.y & moveDownStarted)
        {

            // Bewege das Bild nach oben (auf die Zielposition zu)
            float step = moveSpeed * Time.deltaTime;
            imageTransform.position = Vector3.MoveTowards(imageTransform.position, startPosition, step);
        }
        
       
    }

    // Startet die Bewegung des Bildes
    public void StartImageMovement()
    {
        //Debug.Log("ABC");
        // Wir setzen die Zielposition auf die obere H�lfte des Bildes (z. B. Y = 0 oder die obere Grenze des Bildes)


        clicked = true;
        moveDownStarted = false;
        float step = moveSpeed * Time.deltaTime;
        imageTransform.position = Vector3.MoveTowards(imageTransform.position, targetPosition, step);
        
    }

    private void moveDown(){
        clicked = false;
        moveDownStarted = true;

    }



    
}
