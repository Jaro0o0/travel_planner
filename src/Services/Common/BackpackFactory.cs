namespace Services
{
    public interface IBackpack
    {
        void ShowBackpack();
    }
    public class BackPackFactory 
    {
        public static IBackpack Create( string type )
        {
            if (type == "normal")
            {
                
            }

            if (type == "mountain")
            {
                
            }

            throw new ArgumentException("Unknown backpack type");
        }
        
    }
}
