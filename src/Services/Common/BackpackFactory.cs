

namespace Services
{
    public interface IBackpack
    {
        void ShowBackpack();
        void AddItem();
    }
    public class BackPackFactory 
    {
        public static IBackpack Create( string type )
        {
            if (type == "normal")
            {
                return new Backpack();
            }

            if (type == "mountain")
            {
                
            }

            throw new ArgumentException("Unknown backpack type");
        }
        
    }

    public class Backpack : IBackpack
    {
        public void AddItem()
        {
        }

        public void ShowBackpack()
        {
        }
    }
}
