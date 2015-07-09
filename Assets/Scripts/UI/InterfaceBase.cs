using UnityEngine;
using System.Collections;

//Interface Base Class
public class InterfaceBase : MonoBehaviour {


    public void enable(bool enable)
    {
        this.gameObject.SetActive(enable);
    }

}
