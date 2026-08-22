using System.Collections.Generic;


namespace Services
{
    public class WeatherStation
    {
       private List<IWeatherStation> Weathers  = new List<IWeatherStation>();
       private float temperature;


    public void RegisterObserver(IWeatherStation observer)
    {
            Weathers.Add(observer);
    }

    public void RemoveObserver(IWeatherStation observer)
        {
            Weathers.Remove(observer);
        }
    
    public void NotifiObservers()
        {
            foreach(var observer in Weathers)
            {
                observer.Update(temperature);

            }
        }

    public void SetTemperature(float newTemperature)
        {
            temperature = newTemperature;
            NotifiObservers();

        }

    }

}