//using OneSignalSDK;
using UnityEngine;

public class OneSignalManager : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        //OneSignal.Default.Initialize("8e7846c4-2829-4d49-8670-6241a4ec72c7");
    }
}
