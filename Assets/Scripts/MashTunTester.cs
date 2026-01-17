using UnityEngine;
using UnityEngine.UI;

public class MashTunTester : MonoBehaviour
{
    public MashTun mashTun;
    public Slider waterLevelSlider;
    WaterLevelSensor sensor; 
    
    void Start()
    {
        // Create and install the water level sensor
        if (waterLevelSlider != null)
        {
            sensor = new WaterLevelSensor(waterLevelSlider);
            mashTun.AddInstrument(sensor);
        }
    }
    
    void Update()
    {
        // F = Start filling
        if (Input.GetKeyDown(KeyCode.F))
        {
            mashTun.StartWaterFlow();
            Debug.Log("Started water flow");
        }
        
        // S = Stop filling
        if (Input.GetKeyDown(KeyCode.S))
        {
            mashTun.StopWaterFlow();
            Debug.Log("Stopped water flow");
        }
        
        // B = Brew
        if (Input.GetKeyDown(KeyCode.B))
        {
            mashTun.BrewBeer();
        }
        
        // C = Collect
        if (Input.GetKeyDown(KeyCode.C))
        {
            int points = mashTun.CollectBeer();
            Debug.Log($"Beer collected. Points: {points}");
        }
        //U = uninstall water level sensor
        if(Input.GetKeyDown(KeyCode.U))
        {
            mashTun.RemoveInstrument(sensor);
        }
        //I = install water level sensor
                if(Input.GetKeyDown(KeyCode.I))
        {
            mashTun.AddInstrument(sensor);
        }
    }
}