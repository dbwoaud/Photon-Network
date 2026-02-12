using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public enum Panel
{
    ERROR,
    SUBSCRIBE,
    GENERATOR,
    PAUSE
}
public class PanelManager : MonoBehaviour
{
    private Dictionary<Panel, GameObject> dict = new Dictionary<Panel, GameObject>();
    [SerializeField] GameObject clone = null;
    [SerializeField] Text errorMessage;
    private static PanelManager instance;
    public static PanelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindAnyObjectByType<PanelManager>();
                if (instance == null)
                {
                    GameObject clone = new GameObject(typeof(PanelManager).Name);
                    instance = clone.AddComponent<PanelManager>();
                }
            }
            return instance;
        }
    }
    private void Awake()
    {
        if(instance != null)
            Destroy(gameObject);

    }

    public void Load(Panel panel, string message = null)
    {
        if (!dict.TryGetValue(panel, out clone))
        {
            clone = (GameObject)Instantiate(Resources.Load(panel.ToString()));
            clone.name = clone.name.Replace("(Clone)", "");
            dict.Add(panel, clone);
            DontDestroyOnLoad(clone);
        }
        else
        {
            clone = dict[panel];
            clone.SetActive(true);
        }

        if (panel == Panel.ERROR && message != null)
            Debug.Log(message);
    }
}
