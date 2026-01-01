using UnityEngine;

public class ObjectSetParent : MonoBehaviour
{
    private GameObject WallOBJFolder;
    private GameObject CubeOBJFolder;
    private string myName;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        myName = this.gameObject.name;


        if (myName.Contains("test_tile"))
        {
            WallOBJFolder = GameObject.Find("Wall OBJ Group");
            this.gameObject.transform.SetParent(WallOBJFolder.transform);
        }
        else if (myName.Contains("Cube"))
        {
            CubeOBJFolder = GameObject.Find("Cube OBJ Group");
            this.gameObject.transform.SetParent(CubeOBJFolder.transform);
        }
        
        
    }

}
