using System.Collections.Generic;


namespace Services
{
    public class WeatherStation
    {
       private List<IWeatherStation> Weathers  = new List<IWeatherStation>();
       private float CityName;


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
                observer.Update(CityName);

            }
        }

    public void SetTemperature(float cityName)
        {
            CityName = cityName;
            NotifiObservers();

        }

    }

}