using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    private Vector3 direction = Vector3.forward;
    public bool isStacked = false;
    private RaycastHit hit;

    
    //rightbottom vector3

    //private Vector3 rightBottomVector = new Vector3(transform.position, -0.5f, 0);

    void FixedUpdate()
    {
        
        if (!isStacked)
            return;

        float coeff = 0.02f;
        Debug.DrawRay(transform.position, direction * coeff, Color.red);

        //küplerden en alttakini buluruz. Bu küpe toplayýcý olmasý açýsýndan raycast ekleriz.

        //if this object is

        if (Physics.Raycast(transform.position, direction, out hit, coeff))
        {
            
            if (hit.transform.gameObject.name == "EndPoint")
            {
                hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                GameManager.instance.ActivateWinUI();

            }
        }

        if (Physics.BoxCast(transform.GetComponent<Collider>().bounds.center, new Vector3(0.01f, 0.01f, 0.01f), direction, out hit, Quaternion.identity, 0.01f))
        {
            
            if (hit.transform.gameObject.CompareTag("AntiCube"))
            {
                
                PlayerCubeStackController.Instance.DropCube(this);
                
            }
            //crash.gameObject.SetActive(true);
            
        }



    }


}
