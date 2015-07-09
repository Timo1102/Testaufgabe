using UnityEngine;
using System.Collections;

public class InterfaceBase : MonoBehaviour {


    public void enable(bool enable)
    {
        this.gameObject.SetActive(enable);
    }

}
