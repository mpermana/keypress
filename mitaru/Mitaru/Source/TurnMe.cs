using System;
using System.Threading;

using FFACETools;

namespace Mitaru
{

    public class TurnMe
    {
        Throttle runningThrottle = new Throttle(3);
        bool isRunning;
        bool isTurning;
        bool turningLeft;

        FFACETools.FFACE fface;

        public TurnMe(FFACETools.FFACE fface)
        {
            this.fface = fface;
        }

        public void switchViewMode()
        {
            fface.Windower.SendKeyPress(KeyCode.NP_Number5);
        }
        public void stopTurning()
        {
            if (isTurning)
            {
                Console.WriteLine("stopTurning");
                isTurning = false;
            }
            fface.Windower.SendKey(KeyCode.NP_Number4, false);
            fface.Windower.SendKey(KeyCode.NP_Number6, false);
        }
        public void stopRunning()
        {
            isRunning = false;
            fface.Windower.SendKey(KeyCode.NP_Number8, false);
            // Console.WriteLine("TurnMe.stopRunning");
        }
        public void stopIt()
        {
            isTurning = false;
            isRunning = false;
            fface.Windower.SendKey(KeyCode.NP_Number4, false);
            fface.Windower.SendKey(KeyCode.NP_Number6, false);
            fface.Windower.SendKey(KeyCode.NP_Number7, false);
            fface.Windower.SendKey(KeyCode.NP_Number8, false);
            fface.Windower.SendKey(KeyCode.LeftArrow, false);
            fface.Windower.SendKey(KeyCode.RightArrow, false);
            fface.Windower.SendKey(KeyCode.TabKey, false);
            Thread.Sleep(100);
            // Console.WriteLine("TurnMe.stopIt");
        }

        public void startRunning()
        {
            if (isRunning) {
                return;                
            }
//            if (!runningThrottle.isReady())
//                return;

//            Console.WriteLine("TuneMe.startRunning");
            isRunning = true;
            fface.Windower.SendKey(KeyCode.NP_Number8, true);
            Thread.Sleep(100);
        }

        public void turn(bool left)
        {
            if (isTurning)
            {
                if (left)
                {
                    if (turningLeft)
                    {
                    }
                    else
                    {
                        stopIt();
                        Console.WriteLine("turning left");
                        fface.Windower.SendKey(KeyCode.NP_Number4, true);
                        turningLeft = true;
                    }
                }
                else
                {
                    if (turningLeft)
                    {
                        stopIt();
                        Console.WriteLine("turning right");
                        fface.Windower.SendKey(KeyCode.NP_Number6, true);
                        turningLeft = false;
                    }
                    else
                    {
                    }
                }
            }
            else
            {
                if (left)
                {
                    Console.WriteLine("Start turning left");
                    fface.Windower.SendKey(KeyCode.NP_Number4, true);
                }
                else
                {
                    Console.WriteLine("Start turning right");
                    fface.Windower.SendKey(KeyCode.NP_Number6, true);
                }
                turningLeft = left;
                isTurning = true;
            }
        }
    };

}