using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{
    //helps to detect the collision of the cube with the end point

    public ParticleSystem gemCollect;

    public GemUIController gemUIController;

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (!other.GetComponent<CubeBehaviour>().isStacked)
            {
                if (other.gameObject.CompareTag("Cube"))
                {

                    var cubeBehaviour = other.gameObject.GetComponent<CubeBehaviour>();

                    if (!cubeBehaviour.isStacked)
                    {
                        //Debug listOfCubeBehaviour count
                        //Debug.Log($"List Count for now: {PlayerCubeStackController.Instance.listOfCubeBehaviour.Count}");
                        PlayerCubeStackController.Instance.GetCube(cubeBehaviour);
                    }
                }
                
            }
            
        }
        catch
        {
            Debug.Log("No cube detected so pass");
        }

        if (other.gameObject.CompareTag("gem"))
        {
            //TODO:
            
            gemCollect.transform.position = other.transform.position;
            gemCollect.Play();
            
            
            gemUIController.IncreaseGemCount(gemUIController.getGemCount());
            gemUIController.UpdateText();
            Destroy(other.gameObject);

        }

    }
}
