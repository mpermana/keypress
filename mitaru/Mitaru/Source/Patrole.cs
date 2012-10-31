using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;

using System.Threading;
using System.Text;
using System.Text.RegularExpressions;

using FFACETools;

namespace Mitaru
{

    public class Patrole
    {
        String route;
        String wantedTarget;
        String wantedPlayer;

        String actionWhenFound = "/a <t>";
        String actionWhenStanding;
        String actionWhenFighting;

        bool inPatrol;
        bool inFighting;
        int patrolRouteCounter;

        double distanceWanted = 3;

        bool hasRoute;
        double[] routex = new double[0];
        double[] routez = new double[0];
        int[] wantedTargetId = new int[0];

        public TurnMe turnMe;
        Throttle throttleLog = new Throttle(3, true);

        bool throttleLogReady;
        bool hasDestination;
        bool destinationIsNpc;
        bool stopLookingForTargetAfterGivingUp;
        int destinationNpcId;
        int destinationRouteIndex;
        double destinationx;
        double destinationz;

        int currentRouteIndex;
        DateTime currentRouteStartTime;

        Dictionary<string, DateTime> idTod = new Dictionary<string, DateTime>();

        int timeoutForChasing = 20;
        int timeoutForARoute = 30;

        FFACETools.FFACE fface;
        [DllImport("fface.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern float SetNPCPosH(int InstanceID, int index, float value);

        public Patrole(FFACETools.FFACE fface)
        {
            this.turnMe = new TurnMe(fface);
            this.fface = fface;
        }


        static bool match(string pattern, string input)
        {
            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern.ToLower(), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            MatchCollection matches = regex.Matches(input.ToLower());
            return matches.Count > 0;
        }

        public void execWhenFound()
        {
            Program.execute(actionWhenFound);
        }

        int previousDestinationNpc;
        DateTime previousDestinationNpcFound;
        Throttle skipLookingTargetThrottle = new Throttle(10);
        Throttle waitInRouteBeforeGoingToNextOne = new Throttle(10);

        public void setDestinationNpc(int id)
        {
            /*
            if (throttleLogReady)
            Console.WriteLine("dest=" + String.Format("{0:X}", id) 
                + " st=" + fface.NPC.Status(id) + " " 
                + toString(fface.NPC.PosX(id)) + "," + toString(fface.NPC.PosZ(id))
                + " " + fface.NPC.Name(id)
                + " " + fface.NPC.IsActive(id)
                + " " + toString(fface.NPC.Distance(id))
                );
             * */

            bool alive = (match("standing", fface.NPC.Status(id).ToString()) ||
                match("fighting", fface.NPC.Status(id).ToString())) &&
                fface.NPC.IsActive(id);
            if (alive)
            {
                hasDestination = true;
                destinationIsNpc = true;
                inPatrol = false;
                destinationx = fface.NPC.PosX(id);
                destinationz = fface.NPC.PosZ(id);
                destinationNpcId = id;

                idTod[id.ToString()] = DateTime.Now;

                if (previousDestinationNpc != destinationNpcId)
                {
                    // first time seeing this, record the time
                    previousDestinationNpcFound = DateTime.Now;
                }
                else
                {
                    if (throttleLogReady)
                    {
                        DateTime now = DateTime.Now;
                        double elapsed = (now - previousDestinationNpcFound).TotalSeconds;
                        // Console.WriteLine("elapsed time chasing it: " + elapsed);
                        if (elapsed > timeoutForChasing)
                        {
                            Console.WriteLine("giving up !");
                            hasDestination = false;
                            previousDestinationNpc = 0;
                            destinationNpcId = 0;
                            skipLookingTargetThrottle.reset();
                            stopLookingForTargetAfterGivingUp = true;
                        }
                    }
                }

                previousDestinationNpc = destinationNpcId;
            }
            else
            {
                // destination dead
                hasDestination = false;
                destinationIsNpc = false;
                inPatrol = false;
                destinationNpcId = 0;
                previousDestinationNpc = 0;
            }
        }

        public void setDestinationRoute(int index)
        {
        }

        public void runIteration1()
        {
            throttleLogReady = throttleLog.isReady();
            if (Program.isWanted())
            {
                setDestinationNpc(fface.Target.ID);
            }
            if (!hasDestination)
            {
            }
        }

        public void runIteration()
        {
            // look for wantedTarget
            throttleLogReady = throttleLog.isReady();
            if (!match("fighting", fface.Player.Status.ToString()))
            {
                if (Program.isWanted())
                {
                    setDestinationNpc(fface.Target.ID);
                    if (!fface.Target.IsLocked)
                    {
                        Program.tryLock();
                    }
                    else if (match("standing", fface.Player.Status.ToString()))
                    {
                    }
                }
                else
                {
                    if (hasRoute)
                        Program.Fap();
                    if (hasDestination && destinationIsNpc)
                    {
                        setDestinationNpc(destinationNpcId);
                    }
                }
            }

            if (throttleLogReady)
            {
                //Console.WriteLine("hasDestination  = " + hasDestination);
                //Console.WriteLine("destinationIsNpc= " + destinationIsNpc);
            }

            // no npc destination = hasDestination && !destinationIsNpc || !hasDestination
            // if (match("standing", fface.Player.Status.ToString()))
            if ((hasDestination && !destinationIsNpc) || !hasDestination)
            {
                if (!stopLookingForTargetAfterGivingUp)
                {
                    if (throttleLogReady) Console.WriteLine("standing looking "+toString(fface.Player.PosX)
                            + "," + toString(fface.Player.PosZ));
                    foreach (int id in this.wantedTargetId)
                    {
                        if (throttleLogReady)
                        {
                            string tod = idTod.ContainsKey(id.ToString())?idTod[id.ToString()].ToString():"";
                            Console.WriteLine("  id=" + String.Format("{0:X}", id)
                                + "\t" + toString(fface.NPC.PosX(id))
                                + "," + toString(fface.NPC.PosZ(id))
                                + " " + (fface.NPC.IsActive(id) ? "O" : "X")
                                + " " + fface.NPC.Status(id).ToString()
                                + " " + tod);
                        }

                        if ((match("standing", fface.NPC.Status(id).ToString()) ||
                             match("fighting", fface.NPC.Status(id).ToString()))
                            && fface.NPC.IsActive(id))
                        {
                            setDestinationNpc(id);
                            break;
                        }
                    }
                }
                else
                {
                    if (throttleLogReady)
                        Console.WriteLine("skipping looking target due to throttle");
                    if (this.skipLookingTargetThrottle.isReady())
                        stopLookingForTargetAfterGivingUp = false;
                }
            }

            if (!hasDestination)
            {
                if (!inPatrol && hasRoute)
                {
                    // go to nearest patrole point
                    inPatrol = true;
                    int nearestRouteIndex = 0;
                    double minDistance = this.distance(this.routex[0], this.routez[0], fface.Player.PosX, fface.Player.PosZ);
                    Console.WriteLine("route#0.d=" + minDistance);
                    for (int i = 1; i < this.routex.Length; i++)
                    {
                        double distance = this.distance(this.routex[i], this.routez[i], fface.Player.PosX, fface.Player.PosZ);
                        Console.WriteLine("route#"+i+".d="+ distance);
                        if (distance < minDistance)
                        {
                            minDistance = distance;
                            nearestRouteIndex = i;
                        }
                    }
                    hasDestination = true;
                    destinationIsNpc = false;
                    destinationRouteIndex = nearestRouteIndex;
                    destinationx = routex[nearestRouteIndex];
                    destinationz = routez[nearestRouteIndex];
                    Console.WriteLine("going route#" + destinationRouteIndex + " d="+minDistance);
                    currentRouteIndex = destinationRouteIndex;
                    currentRouteStartTime = DateTime.Now;
                    turnMe.stopIt();
                }
            }

            if (match("fighting", fface.Player.Status.ToString()))
            {
                hasDestination = true;
                destinationx = fface.Target.PosX;
                destinationz = fface.Target.PosZ;
                destinationNpcId = fface.Target.ID;
                destinationIsNpc = true;
                inFighting = true;
                inPatrol = false;
                //Console.WriteLine("fighting ->");
                turnMe.startRunning();
            }
            else
            {
                inFighting = false;
            }


            // check if reaches destination
            if (hasDestination)
            {
                double distanceToDestination = distance(fface.Player.PosX, fface.Player.PosZ, destinationx, destinationz);

                if (throttleLogReady)
                {

                    Console.WriteLine(DateTime.Now.ToShortTimeString() + " dist" + toString(distanceToDestination) 
                        + " pl=" + toString(fface.Player.PosX) + "," + toString(fface.Player.PosZ) 
                        + " ds=" + toString(destinationx) + "," + toString(destinationz) + " dest=" + (destinationIsNpc?"npc="+String.Format("{0:X}",destinationNpcId):"route"+destinationRouteIndex));

                }
                if (inFighting)
                {
                }
                else if (distanceToDestination < distanceWanted)
                {
                    if (throttleLogReady)
                        Console.WriteLine("distanceToDestination=" + distanceToDestination + " < " + "distanceWanted=" + distanceWanted);
                    reachedGoal();
                    turnMe.stopIt();
                }
                else
                {
                    faceDestination();
                    // Console.WriteLine("!fighting & far");
                    turnMe.startRunning();
                }

            }

            // check if we spend too much time chasing this route
            if (hasDestination && !destinationIsNpc)
            {
                        DateTime now = DateTime.Now;
                        double elapsed = (now - currentRouteStartTime).TotalSeconds;
                        // Console.WriteLine("elapsed time in route " + elapsed);
                        if (elapsed > timeoutForARoute)
                        {
                            selectNextRoute();
                        }
            }

            //Thread.Sleep(5);

        }

        void faceDestination()
        {
            if (hasDestination)
            {
                double dx = destinationx - fface.Player.PosX;
                double dz = fface.Player.PosZ - destinationz;
                double h = Math.Atan2(dz, dx);
                double dd = Math.Sqrt(dx * dx + dz * dz);
                double dh = h - fface.Player.PosH;
                if (dh < -Math.PI) dh = 2 * Math.PI + dh;
                if (dh > Math.PI) dh = 2 * Math.PI - dh;
                double distanceError = 0.15;
                if (-distanceError < dh && dh < distanceError)
                {
                    //Console.WriteLine("dhok=" + toString(dh) + " distanceError=" + toString(distanceError));
                }
                else
                {
                    //Console.WriteLine("Start turning due to dh="+toString(dh)+" distanceError="+toString(distanceError));
                    turnMe.stopIt();

                    if (fface.Player.ViewMode != ViewMode.FirstPerson)
                    {
                        flipView();
                    }

                    SetNPCPosH(fface._InstanceID, fface.Player.ID, (float)h);
                    Thread.Sleep(400);
                    //Console.WriteLine("Turning Done");
                }
            }

        }

        void flipView()
        {
            fface.Windower.SendKeyPress(KeyCode.NP_Number5);
            Thread.Sleep(200);
        }

        String toString(double x)
        {
            return (x < 0 ? "" : "+") + String.Format("{0:000.00}", x);
        }

        void reachedGoal()
        {
            // Console.WriteLine("destinationIsNpc=" + destinationIsNpc);
            if (destinationIsNpc)
            {
                execWhenFound();
                hasDestination = false;
            }
            else if (waitInRouteBeforeGoingToNextOne.isReady())
            {
                selectNextRoute();
            }
        }

        void selectNextRoute()
        {
            destinationRouteIndex++;
            destinationRouteIndex = (destinationRouteIndex % routex.Length);
            // continue to next route
            destinationx = routex[destinationRouteIndex];
            destinationz = routez[destinationRouteIndex];

            currentRouteIndex = destinationRouteIndex;
            currentRouteStartTime = DateTime.Now;
            Console.WriteLine("selectNextRoute " + destinationRouteIndex + " " + toString(destinationx) + "," + toString(destinationz));
        }

        double distance(double x1, double y1, double x2, double y2)
        {
            double dx = x2 - x1;
            double dy = y2 - y1;
            return Math.Sqrt(dx * dx + dy * dy);
        }

        public void setRoute(String route)
        {
            if (null == route)
                route = "";

            string[] destinations = route.Split(';');
            hasRoute = destinations.Length > 0;
            routex = new double[destinations.Length];
            routez = new double[destinations.Length];
            int i = 0;
            foreach (string destination in destinations)
            {
                string[] xz = destination.Split(',');
                routex[i] = float.Parse(xz[0]);
                routez[i] = float.Parse(xz[1]);
                i++;
            }
        }

        public void setWantedTarget(String wantedTarget)
        {
            if (null == wantedTarget)
                return;
            this.wantedTarget = wantedTarget;
            string[] ids = wantedTarget.Split(',');
            this.wantedTargetId = new int[ids.Length];
            for (int i = 0; i < ids.Length; i++)
            {
                int id = Int32.Parse(ids[i], System.Globalization.NumberStyles.HexNumber);
                this.wantedTargetId[i] = id;
            }
        }

        public void setActionWhenFound(string actionWhenFound)
        {
            if (actionWhenFound != null)
            {
                this.actionWhenFound = actionWhenFound;
            }
        }
    }
}