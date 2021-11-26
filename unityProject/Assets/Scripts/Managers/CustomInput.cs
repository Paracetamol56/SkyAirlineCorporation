using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInput : MonoBehaviour
{

    static public CustomInput instance;

    private static float ThrottleAxis;
    private static float YawAxis;
    private static float PitchAxis;
    private static float RollAxis;

    private static KeyCode NegativeThrottle;
    private static KeyCode PositiveThrottle;
    private static KeyCode NegativeYaw;
    private static KeyCode PositiveYaw;
    private static KeyCode NegativePitch;
    private static KeyCode PositivePitch;
    private static KeyCode NegativeRoll;
    private static KeyCode PositiveRoll;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        RefreshKeys();
    }

    // Update is called once per frame
    void Update()
    {
        ThrottleAxis = Input.GetKey(NegativeThrottle) ? -1.0f : Input.GetKey(PositiveThrottle) ? 1.0f : 0.0f;
        YawAxis = Input.GetKey(NegativeYaw) ? -1.0f : Input.GetKey(PositiveYaw) ? 1.0f : 0.0f;
        PitchAxis = Input.GetKey(NegativePitch) ? -1.0f : Input.GetKey(PositivePitch) ? 1.0f : 0.0f;
        RollAxis = Input.GetKey(NegativeRoll) ? -1.0f : Input.GetKey(PositiveRoll) ? 1.0f : 0.0f;
    }

    public static float GetAxis(string axis)
    {
        return axis switch
        {
            "Throttle" => ThrottleAxis,
            "Yaw" => YawAxis,
            "Pitch" => PitchAxis,
            "Roll" => RollAxis,
            _ => 0,
        };
    }

    public static void RefreshKeys()
    {
        NegativeThrottle = (KeyCode)PlayerPrefs.GetInt("NegThrottle");
        PositiveThrottle = (KeyCode)PlayerPrefs.GetInt("PosThrottle");
        NegativeYaw = (KeyCode)PlayerPrefs.GetInt("NegYaw");
        PositiveYaw = (KeyCode)PlayerPrefs.GetInt("PosYaw");
        NegativePitch = (KeyCode)PlayerPrefs.GetInt("NegPitch");
        PositivePitch = (KeyCode)PlayerPrefs.GetInt("PosPitch");
        NegativeRoll = (KeyCode)PlayerPrefs.GetInt("NegRoll");
        PositiveRoll = (KeyCode)PlayerPrefs.GetInt("PosRoll");
    }
}
