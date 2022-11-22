using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detector : MonoBehaviour
{

    public ParticleSystem gemCollect;

    
    
    //GemUIController instance
    public GemUIController gemUIController;

    private void OnTriggerEnter(Collider other)
    {
        try
        {
            if (!other.GetComponent<CubeBehaviour>().isStacked)
            {
                if (other.gameObject.CompareTag("Cube"))
                {
                    //Debug.Log($"Cube {collision.gameObject.name}");

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

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("REAL COLLISION");

        //buradaki collision sadece collection layeri ile etkileþime geçmesi gerekir.
        //Dolayýsýyla öncelikle küp toplayý diðer kýsýmdan devre dýþý býrakarak burada debug ile bu sistemin çalýþýp çalýþmadýðýný sorgulayacaðýz.



        if (collision.gameObject.CompareTag("Cube"))
        {
            //Debug.Log($"Cube {collision.gameObject.name}");

            var cubeBehaviour = collision.gameObject.GetComponent<CubeBehaviour>();

            if (!cubeBehaviour.isStacked)
            {
                //Debug listOfCubeBehaviour count
                //Debug.Log($"List Count for now: {PlayerCubeStackController.Instance.listOfCubeBehaviour.Count}");
                PlayerCubeStackController.Instance.GetCube(cubeBehaviour);
            }
        }
    }*/
}
