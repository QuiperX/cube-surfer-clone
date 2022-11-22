using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class PlayerCubeStackController : MonoBehaviour
{
    public float cubeSize = 0.04f;

    public GameObject playerBody;

    public ParticleSystem crash;


    public List<CubeBehaviour> listOfCubeBehaviour = new List<CubeBehaviour>();

    //topcube
    private CubeBehaviour topCube;

    private void FixedUpdate()
    {
       if (listOfCubeBehaviour.Count >= 1)
           RelocatePlayer();
    }
    
    private void Awake()
    {
        Singleton();
    }

    #region Singleton
    
    public static PlayerCubeStackController Instance;


    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        Instance = this;
    }

    #endregion

    public void GetCube(CubeBehaviour cubeBehaviour)
    {
        listOfCubeBehaviour.Add(cubeBehaviour);
        cubeBehaviour.isStacked = true;

        cubeBehaviour.transform.parent = transform;

        ReorderCubes();

        RelocatePlayer();

        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "EndPoint")
        {
            Debug.Log("EndPoint");

            PlayerBehaviour.Instance.VictoryAnimation();
        }
    }
    
    private void RelocatePlayer()
    {
        
        var playerBehaviour = PlayerBehaviour.Instance.transform;
        
        
        Vector3 playerTarget = new Vector3(0f, listOfCubeBehaviour[listOfCubeBehaviour.Count - 1].transform.position.y +0.02f, 0f);

        playerBehaviour.transform.position = new Vector3(playerBehaviour.transform.position.x, playerTarget.y, playerBehaviour.transform.position.z);
    }

    private void characterFall()
    {

        var playerTransform = PlayerBehaviour.Instance.transform;
        float randomRotation = Random.Range(90, 270);
        //Debug.Log("RANDOM: " + randomRotation);

        playerTransform.DORotate(new Vector3(0f, randomRotation, 0f), 0.1f);
        Vector3 ground = new Vector3(0f, -0.035f, 0f);

        playerBody.gameObject.transform.position = new Vector3(playerBody.gameObject.transform.position.x, 0.005f, playerBody.gameObject.transform.position.z);

    }

    public void DropCube(CubeBehaviour cubeBehaviour)
    {
        Debug.Log("We are in DropCube");
        cubeBehaviour.transform.parent = null;
        cubeBehaviour.isStacked = false;

        listOfCubeBehaviour.Remove(cubeBehaviour);

        crash.transform.position = cubeBehaviour.transform.position;
        crash.Play();

        if (listOfCubeBehaviour.Count < 1)
        {
            Debug.Log("Gameover");
            
            PlayerBehaviour.Instance.StopPlayer();
            PlayerBehaviour.Instance.FailAnimation();
            CameMovement.instance.FocusPlayer();

            characterFall();
            
            GameManager.instance.ActivateLoseUI();

            return;
        }

        RelocatePlayer();

    }

    private void ReorderCubes()
    {
        foreach (var cube in listOfCubeBehaviour)
        {
            cube.GetComponent<Rigidbody>().isKinematic = true;

        }
        
        for (int i = 0; i < listOfCubeBehaviour.Count; i++)
        {
            var cube = listOfCubeBehaviour[i];
            cube.transform.localPosition = new Vector3(0f, i * (cubeSize/8), 0f);
        }

        int index = listOfCubeBehaviour.Count - 1;

        foreach (var cube in listOfCubeBehaviour)
        {
            cube.GetComponent<Rigidbody>().isKinematic = false;

        }

    }

}
