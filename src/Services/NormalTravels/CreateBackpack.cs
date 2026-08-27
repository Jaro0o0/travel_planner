using System;
using System.Drawing;
using System.Collections.Generic;


//nesescary elements
// taask count
// for loop for each elemnt dispaly
// wybierbai po liczbie int

namespace Services{

public class NormalBackpack : IBackpack {


    public List<string>  BackpacItems = new List<string>();


    public int counter = 0;

    public Backpack()
    {
        Console.WriteLine("Create your backpack for your trip");
        Console.WriteLine("Enter the name of your backpack: ");
        string backpackName = Console.ReadLine();


        Console.WriteLine(backpackName);

        Console.WriteLine("Choose option");
        int Option = int.Parse(Console.ReadLine());
    }

     public void SelectSize(){

        Console.WriteLine("Select your backpack size ");
        int BackpackSize =  int.Parse(Console.ReadLine());

        Backpack =  new string[BackpackSize];
    }

     public void AddItem(){
        Console.WriteLine("Add your elements");
        Backpack[counter] = Console.ReadLine();
        counter++;

    }

     void  ShowBackpack()
    {
        for (int i=0; i< Backpack.Length; i++)
        {
            Console.WriteLine(Backpack[i]);

        }
    }

    static void Mark()
    {
        Console.WriteLine("Whith item you want to consider: ");
        int ItemNumber = int.Parse(Console.ReadLine());

        Backpack[ItemNumber] = Backpack[ItemNumber] + $"Choose {ItemNumber}";
        

    }

}

}
