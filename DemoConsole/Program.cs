using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SatsuiInput;
using SatsuiInput.Pad;

namespace DemoConsole
{
    class Program
    {
        /// <summary>
        /// Console application entry point
        /// </summary>
        /// <param name="args">Startup arguments</param>
        static void Main(string[] args)
        {
            // Initializing the recorder
            // Only pads inputs are allowed for console applications
            InputRecorder inputRecorder = new InputRecorder(InputType.Pad);
            inputRecorder.Pressed += InputRecorder_Pressed;

            try
            {
                // Starting the recorde
                inputRecorder.Start();

                // Intercepting inputs
                Console.WriteLine("------> Input recorder started");
                Console.WriteLine("[ESC] : Exit");
                Console.WriteLine("------------------------------");

                bool exit = false;
                while (!exit)
                {
                    ConsoleKeyInfo key = Console.ReadKey(true);
                    switch (key.Key)
                    {
                        case ConsoleKey.Escape:
                            exit = true;
                            break;

                    }
                }
            }
            catch(Exception ex)
            {
                // Errors happens...
                Console.WriteLine("-> " + ex.Message);
                Console.ReadKey();
            }
            finally
            {
                // Stopping the recorder
                inputRecorder.Stop();
            }
        }

        /// <summary>
        /// Callback receiving input events
        /// </summary>
        /// <param name="data">Informations about the input</param>
        private static void InputRecorder_Pressed(InputData data)
        {
            PadInputData padData = data as PadInputData;
            if (padData == null) return;

            Console.WriteLine("Id      : " + padData.Id);
            Console.WriteLine("Device  : " + padData.Device);
            Console.WriteLine("Button  : " + padData.Button);
            Console.WriteLine("Pressed : " + padData.Pressed);
            Console.WriteLine("------------------------------");
        }
    }
}
