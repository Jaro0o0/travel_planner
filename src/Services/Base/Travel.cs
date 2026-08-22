namespace Services
{
    public class Travel
    {
        public string Place { get;  private set;}
        public DateTime Date { get; private set;}

        public string Eq {get; private set;}

        public Travel(string place, DateTime date,  string equipment)
        {
            Place =  place;
            Date = date;
            Eq = equipment ;
        }   

        public virtual void ShowWeather(){}
    }

    
}