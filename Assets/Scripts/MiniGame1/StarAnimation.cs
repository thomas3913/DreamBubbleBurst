using UnityEngine;

public class StarAnimation : MonoBehaviour
{

    private float hoverLength = 0.4F;
    private float hoverTimer = 0.0F;
    private bool hoverMode = true;

    public int starNumber;

    private float scaleUpTime = 0.5f;

    private float scaleTimer = 0.0f;

    private bool scaleAnimationStarted =false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.localScale = new Vector3(0.0f, 0.0f, 1.0f);


        
    }

    // Update is called once per frame
    void Update()
    {
        hoverTimer += Time.deltaTime;
        if (hoverTimer > hoverLength) {
            Vector3 angles = gameObject.transform.localEulerAngles;
            angles.z = 2.0F;
            if (hoverMode) {
                angles.z *= -1;
            }

            gameObject.transform.localEulerAngles = angles;

            hoverMode = !hoverMode;
            hoverTimer = 0;
        }

        if(scaleAnimationStarted){
            scaleTimer += Time.deltaTime;

            if(scaleUpTime * starNumber > scaleTimer){

                float scaleFactor = scaleTimer / (scaleUpTime * starNumber); // Calculate the fraction of time elapsed
                scaleFactor = Mathf.Clamp01(scaleFactor);

                transform.localScale = new Vector3(scaleFactor, scaleFactor, 1.0f);

            }
        }
        
        
    }

    public void popUp(){
        scaleAnimationStarted = true;

    }
}
