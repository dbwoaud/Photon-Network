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
        DontDestroyOnLoad(gameObject);
    }

public void Load(Panel panel, string message = null)
{
    if (!dict.TryGetValue(panel, out clone) || clone == null)
    {
        GameObject prefab = Resources.Load<GameObject>(panel.ToString());
        clone = Instantiate(prefab);
        clone.name = panel.ToString();
        dict[panel] = clone;  
    }

    clone.SetActive(true);

    if (panel == Panel.ERROR && message != null)
        clone.GetComponent<ErrorPanel>().SetText(message);
}
}
