using RootMotion.FinalIK;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tool : MonoBehaviour
{
}

public class ToolsManager : MonoBehaviour
{
    [Tooltip("The object to interact to")]
    public InteractionObject interactionObject;
    [Tooltip("The effectors to interact with")]
    public FullBodyBipedEffector[] effectors;
    [Tooltip("The body that has the interaction system")]
    public InteractionSystem interactionSystem;
    [Tooltip("The mesh of the avatar")]
    public GameObject avatarMesh;

    public GameObject fingerPlane;
    public GameObject indicator;

    public GameObject nose;
    public Collider left_hand;
    public Collider right_hand;
    public List<Collider> indexes_hand = new List<Collider>();

    public static ToolsManager Instance { get; private set; }

    private bool _showAvatar = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        if (fingerPlane != null)
            fingerPlane.GetComponent<MeshRenderer>().enabled = false;

        if (indicator != null)
            indicator.GetComponent<MeshRenderer>().enabled = false;

        if (nose != null)
            nose.GetComponent<MeshRenderer>().enabled = false;

        Invoke("StartFingerInteraction", 1);
    }

    private void Update()
    {
        if (OVRInput.GetUp(OVRInput.RawButton.RIndexTrigger) && fingerPlane != null)
        {
            fingerPlane.transform.position = new Vector3(right_hand.transform.position.x, right_hand.transform.position.y, right_hand.transform.position.z * 0.9f);
            
            fingerPlane.GetComponent<FindRandomPoint>().Recalculate();

            indicator.gameObject.transform.position = fingerPlane.GetComponent<FindRandomPoint>().CalculateRandomPoint();
        }
    }

    // TODO: G�rer les exp�riences a faire en faisant un menu pour faire le bon tool

    void StartFingerInteraction()
    {
        if (interactionSystem != null)
        {
            foreach (FullBodyBipedEffector e in effectors)
            {
                interactionSystem.StartInteraction(e, interactionObject, true);
            }
        }
    }

    public bool showAvatar
    {
        get { return _showAvatar; }
        set
        {
            avatarMesh.SetActive(value);
            indicator.GetComponent<MeshRenderer>().enabled = !value;
            _showAvatar = value;
        }
    }
}