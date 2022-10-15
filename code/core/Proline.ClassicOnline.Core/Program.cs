using Newtonsoft.Json; 
using Proline.ClassicOnline.Engine.Parts;
using Proline.ClassicOnline.EventQueue;
using Proline.Resource.IO;
using System;
using System.Collections.Generic;
using Console = Proline.Resource.Console;

namespace Proline.ClassicOnline.Engine
{
    public class Program
    {

        private static void Main(string[] args)
        {
            Console.WriteLine($"Started ClassicOnline"); 

            try
            {
                // Data system

                // Event Systems

                Component.InitializeComponents();

                EngineAPI.StartNewScript("Main");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
