/**
 * example:
 * 
 * mitaru -loop -name cutegirl -exec "/a <t>" -find "(jacket)|(crawler)|(dragon); /tell mitaru shit"
 * mitaru -send Tab
 * mitaru -send Enter
 * mitaru -send NP_Number8
 * mitaru -send script\buynpc.txt
 * 
 * start with {1}, dont use {0}, {0} is for all
 * mitaru -loop -name cutegirl -exec "/echo hahahha {1}" -scan "uses (.*)"
 * 
 * mitaru -follow -loop  -exec "/a <t>" -find "attercop|worker|soldier"
 * 
 * mitaru -loop -name cutegirl -exec "/ws \"tachi: kasha\" <t>" -eval "tp > 100 & hpp < 10"
 * mitaru -loop -exec "/ma \"blizzard v\" <t>" -eval "hpp < 40"
 * mitaru -loop -exec "/item \"bewitching tusk\" <t>" -norotate -find "\?" -name cutegirl
 *
 * simple follow:
 * mitaru -follow -name cute -find sheep -distance 20 -loop
 *
 * "vulnerable to earth elemental magic"
 */
using System;
using System.Diagnostics;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

using FFACETools;

namespace Mitaru
{

    class NameCommand
    {
        public static Dictionary<string, NameCommand> map = new Dictionary<string, NameCommand>();

        public string name;
        public string command;
        public bool spell;
        public bool onTpReady;
        public StatusEffect statusEffectFilter = StatusEffect.Unknown;
        public StatusEffect[] statusEffectFilters;
        public bool statusOfPlayer = true; // false means status of target
        public bool allowCastOnLowMP = false;

        int id;

        byte getIndex()
        {
            for (byte index = 0; index < 20; index++)
                if (id == (int)Program.fface.Timer.GetAbilityID(index))
                {
                    return index;
                }
            return 0;
        }

        /* for WS */
        public NameCommand(string weaponSkill)
        {
            this.name = weaponSkill.Replace('_', ' ');
            this.command = "/ws \""+weaponSkill+"\" <t>";
            onTpReady = true;
            add();
        }

        public NameCommand(string name, string command, bool spell, int id)
        {
            this.name = name.Replace('_', ' ')
                .Replace(" Ichi",": Ichi")
                .Replace(" Ni", ": Ni")
                .Replace(" San",": San");
            this.command = command.Replace('_', ' ')
                .Replace(" Ichi", ": Ichi")
                .Replace(" Ni", ": Ni")
                .Replace(" San", ": San");
            this.spell = spell;
            this.id = id;
            add();
        }

        private void add() {
            string key = name.ToLower();
            map[key] = this;
            // map.Add(key, this);

            string[] names = key.Split();
            string initial = "";
            foreach (string n in names)
            {
                initial += n.ToLower()[0];
            }
            
            map[initial] = this;

        }

        public int GetTime()
        {
            int time = 0;
            //WriteLine("id=" + id + ",spell=" + spell);
            if (onTpReady)
            {
                if (Program.fface.Player.TPCurrent >= 100)
                    return 0;
                return 2;
            }

            if (spell)
            {
                time = Program.fface.Timer.GetSpellRecast((SpellList)id);
                // WriteLine("spell recast " + id + "=" + time);
            }
            else
            {
                time = Program.fface.Timer.GetAbilityRecast(getIndex());
                /*
                for (int i = 0; i < 20; i++)
                    WriteLine("t=" + Program.fface.Timer.GetAbilityRecast((byte)i) + ",id=" + Program.fface.Timer.GetAbilityID((byte)i));
                 */
            }
            //WriteLine("name=" + name + ",time=" + time);
            
            return time;
        }

        // if has required status
        public bool hasRequiredStatus(FFACE fface,bool not = true)
        {
            if (spell && fface.Player.MPCurrent < 200 && !allowCastOnLowMP)
            {
                Console.WriteLine("not enough mp");
                return false;
            }

            if (statusEffectFilters == null && statusEffectFilter.Equals(StatusEffect.Unknown))
            {
                bool result = true;                
                return result;
            }

            StatusEffect[] se = fface.Player.StatusEffects;
            foreach (StatusEffect s in se)
            {
                if (!s.Equals(StatusEffect.Unknown))
                {
                    if (statusEffectFilters != null)
                    {
                        foreach (StatusEffect seToCheck in statusEffectFilters)
                        {
                            if (s.Equals(seToCheck)) 
                            {
                                bool result = true ^ not;
                                return result;
                            }
                        }
                    }
                    else
                    {
                        if (s.Equals(statusEffectFilter))
                        {
                            bool result = true ^ not;
                            return result;
                        }
                    }
                }
            }
            return false ^ not;
        }

        public void Cast()
        {
            Program.SendString(command);
        }
    };

    public class Program
    {
        [DllImport("user32.dll")]
        public static extern short GetKeyState(int keyCode);
        [DllImport("fface.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern float SetNPCPosH(int InstanceID, int index, float value);

        bool CapsLock = (((ushort)GetKeyState(0x14)) & 0xffff) != 0;
        bool NumLock = (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
        bool ScrollLock = (((ushort)GetKeyState(0x91)) & 0xffff) != 0;

        static bool getNumLock() {
            return (((ushort)GetKeyState(0x90)) & 0xffff) != 0;
        }

        static System.Diagnostics.Process process = null;
        static string name = "Mitaru";
        static string command;
        static string parameter;
        static string execParameter; // 2012-10-18 commented out = "/a <t>";
        static string execArgument; // example "defeats the ({0})"
        static string evalArgument;
        static string findArgument;
        static string castArgument;
        static string synthArgument;
        static string partiesArgument = "<p1>,<p2>,<p3>,<p4>,<p5>,"+
                           "<a10>,<a11>,<a12>,<a13>,<a14>,<a15>,"+
                           "<a20>,<a21>,<a22>,<a23>,<a24>,<a25>";
        static Weekday dayArgument;
        static float distanceArgument = 3;
        static string[] args;
        static int parameterIndex;
        static bool terminated = false;
        static bool loop = false;
        static bool follow = false;
        static bool exec = false;
        static bool rotate = true;
        static bool findAlternate = false;
        static bool immediate = false;
        static Match matchArgument;
        static string noexec = "Fighting";
        static int timeOut = 30;
        static string scanArgument;
        static float maxDistance = 50;
        static bool checkYDiff = true;
        static string castTarget = null;
        static bool exactMatch = false;
        static bool enableExactMatch = false;
        static string npcArgument;
        static string goArgument;
        static string patroleArgument;
        static string patroleTarget;
        static string target;

        static bool homing = false;
        static float homeX;
        static float homeZ;



        
        public static FFACE fface;
        public static FFACE.WindowerTools windower;
        public static List<FFACE> ffaceList = new List<FFACE>();

        public static string prefix = "";
        static void WriteLine(String line)
        {
            Console.WriteLine(prefix + "\t[" + DateTime.Now + "] " + line);
        }

        public static void SendString(string line)
        {
            WriteLine(line);
            windower.SendString(line);
        }

        public static void execute(String message)
        {
            bool isKeyCode = false;
            foreach (KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
            {
                if (match(message, keyCode.ToString()))
                {
                    isKeyCode = true;
                    WriteLine("keyCode=" + keyCode.ToString());
                    KeyPress(keyCode);
                    break;
                }
            }

            if (message.StartsWith("wait", StringComparison.OrdinalIgnoreCase) || message.StartsWith("sleep", StringComparison.OrdinalIgnoreCase)
                 || message.StartsWith("/wait", StringComparison.OrdinalIgnoreCase) || message.StartsWith("/sleep", StringComparison.OrdinalIgnoreCase))
            {
                int delay = Int32.Parse(message.Split()[1]);
                WriteLine("Sleep=" + delay);
                Thread.Sleep(delay);
            }
            else if (message.StartsWith(":pos", StringComparison.OrdinalIgnoreCase))
            {
                // reverse log file with tac | cut -b 22-36
                SendString(ToString(fface.Player.Position));
                Console.WriteLine("Pos:" + Shit(fface.Player.Position.X) + "," + Shit(fface.Player.Position.Z)+";");
            }
            else if (message.StartsWith("::", StringComparison.OrdinalIgnoreCase))
            {
                string comm = message.Substring(2);
                string[] args = comm.Split(' ');
                Console.WriteLine("comm="+comm);
                GetArgs(args);
                Console.WriteLine("command="+command);
                commando();
                Console.WriteLine("finished"+command);
            }
            else if (!isKeyCode)
            {
                SendString(message);
                Thread.Sleep(1000);
            }
        }

        static bool match(string pattern, string input)
        {
            if (null == input)
                return false;

            if (exactMatch)
            {
                return pattern.Equals(input);
            }

           System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern.ToLower(), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
           MatchCollection matches = regex.Matches(input.ToLower());
           //WriteLine("pattern: " + pattern.ToLower() + ", input=" + input.ToLower() + ", match=" + matches.Count);
           return matches.Count > 0;
        }

        static void doExec()
        {
            if (exec)
            {
                string[] execs = execParameter.Split(';');
                try
                {
                    execs = System.IO.File.ReadAllLines(execParameter);
                }
                catch (Exception)
                {
                }
                foreach (string e in execs) 
                {
                    string message = e.Trim();
                    if (message[0] == '#')
                        continue;
                    if (null != matchArgument)
                    {
                        string[] objects = new string[matchArgument.Groups.Count];
                        for (int i = 0; i < objects.Length; i++)
                        {
                            objects[i] = matchArgument.Groups[i].Value;
                        }
                        message = String.Format(message, objects);
                    }

                    execute(message);
                }
            }
        }

        static bool far()
        {
            float dx = fface.Target.PosX - fface.Player.PosX;
            float dz = fface.Target.PosZ - fface.Player.PosZ;
            double dd = Math.Sqrt(dx * dx + dz * dz);
            double dh = Math.Abs(fface.Target.PosH - fface.Player.PosH);
            return dd > distanceArgument;
        }

        static bool facingEnemy()
        {
            float dx = fface.Target.PosX - fface.Player.PosX;
            float dz = fface.Target.PosZ - fface.Player.PosZ;
            double dd = Math.Sqrt(dx * dx + dz * dz);
            double dh = Math.Abs(fface.Target.PosH - fface.Player.PosH);
            return dh > 2.5 && dh < 3.4;
        }

        static Throttle throttleLock = new Throttle(25);
        static DateTime lastLockOn = DateTime.Now;

        public static void tryLock()
        {
            if (!fface.Target.IsLocked && !terminated && isWanted() && throttleLock.isReady())
            {
                WriteLine("Locking on wanted " + fface.Target.Name + " id=" + String.Format("{0:X}",fface.Target.ID));
                SendString("/lockon");
            }
         }

        static void tryLock2()
        {
            if (fface.Target.IsLocked)
                return;

            WriteLine("while (!fface.Target.IsLocked) target=" + fface.Target.Name);
            DateTime t3 = DateTime.Now;
            while (!fface.Target.IsLocked)
            {
                DateTime t4 = DateTime.Now;
                if ((t4 - t3).TotalSeconds > 5)
                {
                    WriteLine("Timeout: while (!fface.Target.IsLocked)" + fface.Target.Name);
                    break;
                }
                if (terminated)
                    break;

                double timeSinceLock = (DateTime.Now - lastLockOn).TotalSeconds;
                if (timeSinceLock < 10)
                {
                    // too soon to lock
                    WriteLine("timeSinceLock=" + timeSinceLock+",sleeping");
                    Thread.Sleep((int)(10 - timeSinceLock + 0.5) * 1000);
                    WriteLine("/lockon");
                }
                SendString("/lockon");
                lastLockOn = DateTime.Now;
                Thread.Sleep(100);
            }
        }

        static void eval()
        {
            do
            {
                bool requireTarget = evalArgument.IndexOf("hpp") != -1;
                bool hasTarget = fface.Target.ID != 0;
                Thread.Sleep(1000);
                // tp > 10 & hpp > 10
                //fface.Player.TPCurrent
                if (requireTarget && !hasTarget)
                {
                    continue;
                }

                string expression = evalArgument
                    .Replace("target.hp", fface.Target.HPPCurrent.ToString()) 
                    .Replace("target.status",fface.Target.Status.ToString())
                    .Replace("target.name", fface.Target.Name.ToString())
                    .Replace("player.hp", fface.Player.HPPCurrent.ToString())
                    .Replace("player.status", fface.Player.Status.ToString())
                    ;

                string hppcurrent = fface.Target.HPPCurrent.ToString();
                
                expression = expression.Replace("hpp", hppcurrent);
                expression = expression.Replace("tp", fface.Player.TPCurrent.ToString());
                string[] tokens = expression.Split();
                int arg0 = Int32.Parse(tokens[0]);
                int arg1 = Int32.Parse(tokens[2]);
                Stack<int> args = new Stack<int>();
                Stack<string> ops = new Stack<string>();
                Stack<string> evalops = new Stack<string>();
                Stack<bool> evaluates = new Stack<bool>();
                bool evaluate = false;
                int needArgs = 0;
                int needEvaluates = 0;
                for (int i = 0; i < tokens.Length; i++)
                {
                    int value = 0;
                    if (Int32.TryParse(tokens[i], out value))
                    {
                        args.Push(value);
                    }
                    else if ("&".Equals(tokens[i]) || "&&".Equals(tokens[i]))
                    {
                        needEvaluates = 2;
                        evalops.Push(tokens[i]);
                    }
                    else
                    {
                        ops.Push(tokens[i]);
                        needArgs = 2;
                    }

//                    WriteLine("args=" + args.Count);
//                    WriteLine("needArgs=" + needArgs);
                    if (args.Count >= needArgs && ops.Count > 0)
                    {
                        string op = ops.Pop();
                        arg1 = args.Pop();
                        arg0 = args.Pop(); ;
                        if (op.Equals(">"))
                        {
                            evaluate = arg0 > arg1;
                        }
                        else if (op.Equals("="))
                        {
                            evaluate = arg0 == arg1;
                        }
                        else if (op.Equals("<"))
                        {
                            evaluate = arg0 < arg1;
                        }
                        //WriteLine("push " + arg0 +" "+ op + " " + arg1 + " evaluate=" + evaluate);
                        evaluates.Push(evaluate);
                    }
                    if (evaluates.Count >= needEvaluates && evalops.Count > 0)
                    {
                        string op = evalops.Pop();
                        bool barg1 = evaluates.Pop();
                        bool barg0 = evaluates.Pop();
                        if (op.Equals("&"))
                        {
                            evaluate = barg0 && barg1;
                        } else if (op.Equals("|")) {
                            evaluate = barg0 || barg1;
                        }

                        evaluates.Push(evaluate);
                    }
                }
                if (evaluates.Count > 0)
                    evaluate = evaluates.Pop();
                WriteLine(expression + "\t" + evaluate);
                if (evaluate)
                {
                    WriteLine("evaluated " + evalArgument);
                    doExec();
                }
            } while (loop);
        }

        static void RotateView()
        {
            if (rotate && getNumLock())
            {
                KeyPress(KeyCode.NP_Number2);
                KeyPress(KeyCode.F8Key);
                windower.SendKey(KeyCode.LeftArrow, true);
                Thread.Sleep(600);
                windower.SendKey(KeyCode.LeftArrow, false);
            }

        }

        public static void Fap()
        {
            if (!getNumLock())
            {
                WriteLine("NumLock Off");
                return;
            }

            if (match("player", findArgument))
            {
                string[] players = findArgument.Split(',');
                foreach (string player in players)
                {
                    string[] playerName = player.Split(':');
                    if (playerName.Length > 1)
                    {
                        SendString("/ta "+playerName[1]);
                    }
                    else
                    {
                        SendString("/ta <p1>");
                        SendString("/ta <p2>");
                        SendString("/ta <p3>");
                        SendString("/ta <p4>");
                        SendString("/ta <p5>");
                    }
                }
            }
            else
            {
                KeyPress(KeyCode.TabKey);
            }
        }

        static void Unlock()
        {
            while (fface.Target.IsLocked && !terminated)
            {
                WriteLine("Unlocking " + fface.Target.Name);
                if (!loop)
                    break;
                KeyPress(KeyCode.NP_MultiplyKey);
            }
        }

        static void DoHoming()
        {
            string status = fface.Player.Status.ToString();
            if (homing && !terminated && match("Standing", status))
            {
                Unlock();
                WriteLine("Homing to " + homeX + "," + homeZ);
                // fface.Navigator.GotoXZ(() => homeX, () => homeZ, false);
                fface.Navigator.Goto(homeX, homeZ, false);
                KeyPress(KeyCode.NP_Number8);
                KeyPress(KeyCode.NP_Number2);
            }
        }

        static int findI = 0;
        static void printWantedTarget()
        {
            string [] targetNames = findArgument.Split(',');
            string ffaceTargetName = fface.Target.Name;
                if (findAlternate)
                {
                    string target = targetNames[findI % targetNames.Length];
                    WriteLine("Looking for "+target);
                }
                else
                {
                    Console.Write("Looking fors ");
                    foreach (string targetName in targetNames)
                    {
                        Console.Write(targetName + " ");
                    }
                    Console.WriteLine();
                }
        }
        static int previousIsWantedTargetId = 0;
        static bool previousIsWantedResult = false;

        public static bool isWanted()
        {
            if (null == findArgument)
                return false;

            if (match("player",findArgument))
            {
                WriteLine("match player");
                return true;
            }

            int targetId = fface.Target.ID;
            if (targetId == 0)
            {
                previousIsWantedTargetId = 0;
                return false;
            }

            if (match("dead", fface.Target.Status.ToString()))
            {
                previousIsWantedTargetId = 0;
                return false;
            }

            if (targetId == previousIsWantedTargetId)
            {
                return previousIsWantedResult;
            }
            previousIsWantedTargetId = targetId;

            string id = String.Format("{0:X}", targetId);
            string [] targetNames = findArgument.Split(',');
            string ffaceTargetName = fface.Target.Name;
            bool result = false;

            if (findAlternate)
            {
                string target = targetNames[findI % targetNames.Length];
                result = match(target, ffaceTargetName) | match(target, id); 
            }
            else
            {
                foreach (string target in targetNames)
                {
                    result = result | match(target, ffaceTargetName) | target.ToLower().Equals(id.ToLower());
                }
            }

            double distance = 0;
            if (result)
            {
                // if target.y's difference with player too big, dont chase it
                float yDifference = Math.Abs(fface.Target.PosY - fface.Player.PosY);
                if (checkYDiff && yDifference > 20)
                {
                    Console.WriteLine("Reject " + fface.Target.Name + " due to yDifference=" + yDifference);
                    result = false;
                }

                distance = xzdistance(fface.Target.Position, fface.Player.Position);
                if (distance > maxDistance)
                {
                    Console.WriteLine("Reject " + fface.Target.Name + " due to maxDistance < " + distance);
                    result = false;
                }
            }
            previousIsWantedResult = result;

            if (result)
            {
                // WriteLine("Wanted: " + String.Format("{000:X}",targetId) + " " + String.Format("{00.000}",distance) + fface.Target.Name);
            }

            return result;
        }

        static bool isInParty(int serverId)
        {
            Dictionary<byte, FFACE.PartyMemberTools> partyMember = fface.PartyMember;
            for (byte i = 0; i < 18; i++)
            {
                if (partyMember[i].Active)
                {
                    if (serverId == partyMember[i].ServerID)
                        return true;                    
                }
            }
            return false;
        }

        static bool isClaimed(bool byOthersOnly=true)
        {
            bool claimed = false;
            int claimId = 0;
            
            int targetId = fface.Target.ID;
            if (targetId != 0)
            {
                claimed = fface.NPC.IsClaimed(targetId);
                claimId = fface.NPC.ClaimedID(targetId);
                if (claimed && byOthersOnly)                    
                {
                    if (isInParty(claimId))
                    {
                        // WriteLine("claimed by party member " + claimId);
                        return false;
                    }
                }
            }
            // WriteLine("claimed=" + claimed + ",claimId=" + claimId);
            return claimed;
        }
        static void FindNearestWanted()
        {
            if (fface.Player.Status != FFACETools.Status.Standing)
                return;

            Fap();

            if (immediate && isWanted())
                return;

            int nextTargetId;
            int neareastId = 0;
            double nearestDistance = 0;
            int loopCount = 0;
            List<int> seen = new List<int>();
            do
            {
                Fap();
                nextTargetId = fface.Target.ID;
                if (seen.Contains(nextTargetId))
                    break;
                seen.Add(nextTargetId);
                if (nextTargetId != 0)
                {
                    if (isWanted())
                    {
                        double targetDistanceX = fface.Target.PosX - fface.Player.PosX;
                        double targetDistanceY = fface.Target.PosX - fface.Player.PosY;
                        double targetDistanceZ = fface.Target.PosX - fface.Player.PosZ;
                        double targetDistance = targetDistanceX * targetDistanceX + targetDistanceY * targetDistanceY + targetDistanceZ * targetDistanceZ;
                        if (neareastId == 0 || targetDistance < nearestDistance)
                        {
                            neareastId = fface.Target.ID;
                            nearestDistance = targetDistance;
                        }
                        if (immediate)
                            return;
                    }
                    loopCount++;
                }
                if (terminated)
                    break;
            } while (true);
            for (int i = 0; i < loopCount; i++)
            {
                Fap();
                if (fface.Target.ID == neareastId)
                {
                    break;
                }
            }
            
        }
        static bool inTown()
        {
            Zone[] towns = {Zone.Port_Jeuno,Zone.Lower_Jeuno,Zone.Upper_Jeuno,Zone.Ru_Lude_Gardens};
            return towns.Contains(fface.Player.Zone);
        }
        static void find()
        {
            string targetName = findArgument;

            homeX = fface.Player.PosX;
            homeZ = fface.Player.PosZ;
            fface.Navigator.DistanceTolerance = 10;
            WriteLine("home is " + homeX + "," + homeZ);
            int counter = 0;
            do
            {
                counter++;
                if (counter % 200 == 0)
                    WriteLine("Find " + findArgument);
                Thread.Sleep(100);
                while (!terminated && inTown())
                {
                    Thread.Sleep(1000);
                    continue;
                }

                if (terminated)
                    break;

                FindNearestWanted();
                int targetId = fface.Target.ID;
                bool noTarget = !isWanted() || isClaimed();

                DoHoming();
                if (match("Standing", fface.Player.Status.ToString()) && noTarget)
                {
                    Throttle throttleRotate = new Throttle(5,true);
                    while (match("Standing", fface.Player.Status.ToString()))
                    {
                        Thread.Sleep(100);
                        if (isWanted())
                        {   // added 2012/10/10
                            if (!isClaimed())
                            break;
                        }
                        if (terminated)
                            break;

                        if (throttleRotate.isReady())
                        {
                            RotateView();
                            Thread.Sleep(1000);
                        }

                        if (fface.Menu.IsOpen)
                            Thread.Sleep(1000);
                        else
                        {
                            if (fface.Target.IsLocked)
                                SendString("/lockon");
                            Fap();
                        }
                    }
                }
                tryLock();

                bool timedOut = false;
                if (follow)
                {
                    short npcid = (short)fface.Target.ID;
                    if (fface.Target.IsLocked)
                    {
                        float dx = fface.Target.PosX - fface.Player.PosX;
                        float dz = fface.Target.PosZ - fface.Player.PosZ;
                        double dd = Math.Sqrt(dx * dx + dz * dz);
                        double dh = Math.Abs(fface.Target.PosH - fface.Player.PosH);

                        DateTime t1 = DateTime.Now;
                        if (!facingEnemy() && match("Fighting", fface.Player.Status.ToString()))
                        {
                            WriteLine("!facing & fighting, adjusting");
                            KeyPress(KeyCode.NP_Number2);
                            KeyPress(KeyCode.NP_Number4);
                            KeyPress(KeyCode.NP_Number8);
                        }
                        if (far() && fface.Target.IsLocked)
                        {
                            WriteLine("Far & Locked, chasing target" + fface.Player.Status);                        

                            windower.SendKey(KeyCode.NP_Number8, true);
                            double lastdd = dd;
                            while (far() && fface.Target.IsLocked)
                            {
                                dx = fface.Target.PosX - fface.Player.PosX;
                                dz = fface.Target.PosZ - fface.Player.PosZ;
                                dd = Math.Sqrt(dx * dx + dz * dz);
                                double dddd = Math.Abs(lastdd - dd);
                                dh = Math.Abs(fface.Target.PosH - fface.Player.PosH);
                                DateTime t2 = DateTime.Now;
                                TimeSpan time = t2 - t1;
                                if (time.TotalSeconds > timeOut)
                                {
                                    WriteLine("Timed out, giving up");
                                    SendString("/lockon");
                                    Thread.Sleep(100);
                                    SendString("/a off");
                                    Thread.Sleep(100);
                                    KeyPress(KeyCode.EscapeKey);
                                    KeyPress(KeyCode.NP_Number2);
                                    timedOut = true;
                                    break;
                                }
                                //WriteLine("Running dd=" + string.Format("{0:0.0}", dd)
                                //    + " dh=" + string.Format("{0:0.00}", dh) + " time=" + time.TotalSeconds + " dddd=" + dddd);
                                //fface.Navigator.GotoNPCXZ(npcid);
                                Thread.Sleep(100);
                                lastdd = dd;
                                if (terminated)
                                    break;
                            }
                            windower.SendKey(KeyCode.NP_Number8, false);
                            WriteLine("Done chasing ...");
                            if (!timedOut)
                            {
                                findI++;
                                printWantedTarget();
                            }
                        }
                    }
                }
                
                if (terminated)
                    break;

                if (isWanted())
                {
                    if (!match(noexec, fface.Player.Status.ToString()) ||
                        !follow)
                    {
                        if (isWeakened())
                        {
                            WriteLine("weakened ... waiting");
                            Thread.Sleep(10000);
                            continue;
                        }
                        doExec();
                    }
                }
            } while (loop);

        }

        static DateTime lastKeyPress;
        static void KeyPress(KeyCode key)
        {
            if (terminated)
                return;
            windower.SendKey(key,true);
            Thread.Sleep(70);
            windower.SendKey(key,false);
            Thread.Sleep(230);
            lastKeyPress = DateTime.Now;
        }

        static void SelectItem(string name,int count = 0)
        {
            name = name.Trim();
            KeyPress(KeyCode.LeftArrow); Thread.Sleep(50);
            KeyPress(KeyCode.LeftArrow); Thread.Sleep(50);
            KeyPress(KeyCode.LeftArrow); Thread.Sleep(50);
            KeyPress(KeyCode.LeftArrow); Thread.Sleep(50);
            KeyPress(KeyCode.LeftArrow); Thread.Sleep(50);

                        
            bool equal = fface.Item.SelectedItemName.Equals(name, StringComparison.OrdinalIgnoreCase);
            
            int previousIndex = fface.Item.SelectedItemIndex;
            WriteLine("SelectItem=" + name);
            uint itemCount = 0;
            while (!equal && !terminated)
            {
                KeyPress(KeyCode.DownArrow);                
                Thread.Sleep(50);
                while (!terminated && fface.Item.SelectedItemName.Length == 0)
                {
                    WriteLine(":" + fface.Item.SelectedItemName);
                    Thread.Sleep(100);
                }
                
                DateTime startTime = DateTime.Now;
                while (!terminated && fface.Item.SelectedItemIndex == previousIndex)
                {
                    Thread.Sleep(50);
                    if ((DateTime.Now - startTime).Duration().TotalSeconds > 5)
                        throw new Exception("Can't find " + name);
                }
                previousIndex = fface.Item.SelectedItemIndex;
                itemCount = fface.Item.GetItemCountByIndex(previousIndex, InventoryType.Inventory);
                WriteLine("  SelectedItemName=" + fface.Item.SelectedItemName+" ("+itemCount+")");
                equal = fface.Item.SelectedItemName.Equals(name, StringComparison.OrdinalIgnoreCase);
                if (count > 1 && equal)
                {
                    equal = itemCount >= count;
                    if (!equal)
                    {
                        WriteLine("Skipping due to count less than required: " + name);
                    }
                }
            }



            KeyPress(KeyCode.EnterKey);
            Thread.Sleep(500);

            if (count == 0)
            {
                Thread.Sleep(200);
                KeyPress(KeyCode.EnterKey);
                Thread.Sleep(500);
                return;
            }

            WriteLine("count=" + count);
            if (count > 1)
            {
                for (int i = 1; i < count; i++)
                {
                    KeyPress(KeyCode.UpArrow);
                    Thread.Sleep(200);
                }
                KeyPress(KeyCode.EnterKey);
                Thread.Sleep(500);
            }
        }

        static int synthx = 0;
        static int synthy = 0;

        static Throttle throttleSynthTime = new Throttle(30);

        /**
         * 0 1 2 3 8
         * 4 5 6 7
         */        
        static void SelectSynthSlot(int index)
        {
            int x = index % 4;
            int y = index / 4;
            if (index == 8)
            {
                x = 4;
                y = 0;
            }
            //WriteLine("index=" + index + ",x=" + x + ",y=" + y + ",sx=" + synthx + ",sy=" + synthy);
            while (x > synthx && !terminated)
            {
                KeyPress(KeyCode.RightArrow);
                Thread.Sleep(90);
                synthx++;
            }
            while (x < synthx && !terminated)
            {
                KeyPress(KeyCode.LeftArrow);
                Thread.Sleep(90);
                synthx--;
            }
            while (y > synthy && !terminated)
            {
                KeyPress(KeyCode.DownArrow);
                Thread.Sleep(90);
                synthy++;
            }
            while (y < synthy && !terminated)
            {
                KeyPress(KeyCode.UpArrow);
                Thread.Sleep(90);
                synthy--;
            }

            if (index == 8)
            {
                throttleSynthTime.isReady(true);                
            }
            KeyPress(KeyCode.EnterKey);
            Thread.Sleep(500);
        }

        static void WaitStatus(string status)
        {
            while (!terminated && !match(status,fface.Player.Status.ToString()))
            {
                Thread.Sleep(100);
            }
        }

        static void WaitMenu(string help)
        {
            while (!terminated && !match(help, fface.Menu.Help))
            {
                WriteLine("WaitMenu=" + help + "," + fface.Menu.Help);
                Thread.Sleep(100);
            }
        }

        static void synth()
        {
            // WriteLine("example: mitaru -synth \"Ice Crystal,Grape Juice,Holy Water:1,White Honey:1\"");
            FFACE.TimerTools.VanaTime time = fface.Timer.GetVanaTime();
            Console.WriteLine("dayArgument=" + dayArgument.ToString());
                while (!match(dayArgument.ToString(),fface.Timer.GetVanaTime().ToString()) && !terminated)
                {
                    Console.WriteLine(fface.Timer.GetVanaTime().ToString());
                    Thread.Sleep(3000);
                }
            
            while (!terminated)
            {
                WriteLine("Begin Synth");
                while (fface.Menu.IsOpen && loop && !match("Synthesis",fface.Menu.Selection))
                {
                    KeyPress(KeyCode.EscapeKey);
                }
                WriteLine("Waiting for standup");

                WaitStatus("Standing");

                string recipe = "Ice Crystal,Grape Juice,Holy Water:1,White Honey:1";
                recipe = synthArgument;
                string[] ingredients = recipe.Split(',');
                try
                {
                    ingredients = System.IO.File.ReadAllLines(synthArgument);
                }
                catch (Exception e)
                {
                }

                windower.SendKey(KeyCode.LeftCtrlKey, true);
                windower.SendKeyPress(KeyCode.LetterI);
                windower.SendKey(KeyCode.LeftCtrlKey, false);


                SelectItem(ingredients[0]);
                if (terminated)
                    return;
                WaitMenu("Select the items");

                KeyPress(KeyCode.RightArrow); Thread.Sleep(10);
                KeyPress(KeyCode.RightArrow); Thread.Sleep(10);
                KeyPress(KeyCode.RightArrow); Thread.Sleep(10);
                KeyPress(KeyCode.RightArrow); Thread.Sleep(10);


                FFACE.NPCTRADEINFO npcTradeInfo = new FFACE.NPCTRADEINFO();
                npcTradeInfo.items = new FFACE.TRADEITEM[ingredients.Length-1];
                bool[] used = new bool[80];
                for (int i = 1; i < ingredients.Length; i++)
                {
                    string[] ingredient = ingredients[i].Split(':');
                    int count = 1;
                    ingredient[0] = ingredient[0].Trim();
                    if (ingredient.Length > 1)
                        count = Int32.Parse(ingredient[1]);
                    bool found = false;
                    for (int j = 0; j < 80; j++)
                    {
                        FFACE.ItemTools.InventoryItem item = null;
                        try
                        {
                            item = fface.Item.GetInventoryItem(j);
                            
                            //WriteLine("item " + j + ": " + item);
                            // WriteLine("ingredient[0]=" + ingredient[0]);
                            //WriteLine("FFACE.ParseResources.GetItemName(item.ID)=" +FFACE.ParseResources.GetItemName(item.ID));
                            if (match(ingredient[0], FFACE.ParseResources.GetItemName(item.ID)) &&
                                (item.Count >= count) && !used[j] && item.Flag != 5)
                            {
                                WriteLine("\t" + ingredient[0]);
                                //WriteLine("item.Flag=" + item.Flag);
                                //WriteLine("item.Extra=" + item.Extra);
                                //WriteLine("index=" + item.Index);
                                //WriteLine("used[j]=" + used[j]+",j="+j);
                                //WriteLine("ID="+item.ID);
                                //WriteLine("count=" + count);
                                used[j] = true;
                                npcTradeInfo.items[i - 1].Count = (byte)count;
                                npcTradeInfo.items[i - 1].Index = item.Index;
                                npcTradeInfo.items[i - 1].ItemID = item.ID;
                                
                                //WriteLine("npcTradeInfo=" + npcTradeInfo);
                                found = true;
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            continue;
                        }
                    }
                    if (!found)
                    {
                        WriteLine(ingredient[0] + " not found");
                        return;
                    }


                }
                fface.Menu.SetCraftItems(npcTradeInfo);
                KeyPress(KeyCode.EnterKey);

                Thread.Sleep(3000);

                while (fface.Player.IsSynthing())
                {
                    Console.Write(".");
                    Thread.Sleep(100);
                }

                if (!loop)
                    break;
            }
        }


        static void GetArgs(string[] args)
        {
            string env = System.Environment.GetEnvironmentVariable("name");
            if (null != env)
            {
                name = env;
                WriteLine("name from env=" + name);
            }

            for (int i = 0; i < args.Length; i++)
            {               
                if (args[i].StartsWith("-"))
                {
                    if (i + 1 < args.Length)
                    {
                        parameter = args[i + 1];
                        parameterIndex = i + 1;
                    }
                }

                if (args[i].Equals("-find"))
                {
                    command = args[i];
                    findArgument = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-eval"))
                {
                    command = args[i];
                    evalArgument = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-scan"))
                {
                    command = args[i];
                    scanArgument = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-synth"))
                {
                    command = args[i];
                    synthArgument = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-send"))
                {
                    command = args[i];
                    parameter = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-menu"))
                {
                    command = args[i];
                    parameter = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-move"))
                {
                    command = args[i];
                    i += 3;
                }
                else if (args[i].Equals("-cast"))
                {
                    command = args[i];
                    castArgument = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-day"))
                {
                    string day = args[i + 1];
                    i++;
                    foreach (Weekday weekDay in Enum.GetValues(typeof(Weekday)))
                    {
                        if (match(weekDay.ToString(), day))
                            dayArgument = weekDay;
                    }
                }
                else if (args[i].Equals("-hotchick"))
                {
                    command = args[i];
                }
                else if (args[i].Equals("-inventory"))
                {
                    command = args[i];
                }
                else if (args[i].Equals("-script"))
                {
                    command = args[i];
                }
                else if (args[i].Equals("-locked"))
                {
                    command = args[i];
                }
                else if (args[i].Equals("-parties"))
                {
                    partiesArgument = args[i + 1];
                    i++;
                }
                

                if (args[i].Equals("-name"))
                {
                    name = args[i+1];
                    i++;
                }
                else if (args[i].Equals("-exec"))
                {
                    exec = true;
                    execParameter = args[i + 1];
                    i += 1;
                }
                else if (args[i].Equals("-loop"))
                {
                    loop = true;
                }
                else if (args[i].Equals("-follow"))
                {
                    follow = true;
                }
                else if (args[i].Equals("-homing"))
                {
                    homing = true;
                }                    
                else if (args[i].Equals("-norotate"))
                {
                    rotate = false;
                }
                else if (args[i].Equals("-distance"))
                {
                    distanceArgument = float.Parse(args[i + 1]);
                }
                else if (args[i].Equals("-alternate"))
                {
                    findAlternate = true;
                }
                else if (args[i].Equals("-immediate"))
                {
                    immediate = true;
                }
                else if (args[i].Equals("-maxDistance"))
                {
                    maxDistance = float.Parse(args[i + 1]);
                }
                else if (args[i].Equals("-castTarget"))
                {
                    castTarget = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-exactMatch"))
                {
                    enableExactMatch = true;
                }
                else if (args[i].Equals("-npc"))
                {
                    command = args[i];
                    npcArgument = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-go"))
                {
                    command = args[i];
                    goArgument = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-patrole"))
                {
                    command = args[i];
                    patroleArgument = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-patroleTarget"))
                {
                    patroleTarget = args[i + 1];
                    i++;
                }
                else if (args[i].Equals("-locate"))
                {
                    command = args[i];
                }
                else if (args[i].Equals("-target"))
                {
                    patroleTarget = args[i + 1];
                    target = args[i + 1];
                    i++;
                }

                checkYDiff = System.Environment.GetEnvironmentVariable("checky") != null;
            }
         }

        static void SetProcess()
        {
            Process[] processes = System.Diagnostics.Process.GetProcessesByName("pol");
            for (int i = 0; i < processes.Length; i++)
            {
                process = processes[i];
                fface = new FFACE(process.Id);
                prefix = fface.Player.Name;
                dayArgument = fface.Timer.GetVanaTime().DayType;
                ffaceList.Add(fface);
                if (match(name, fface.Player.Name))
                {
                    break;
                }
            }
            WriteLine("Process=" + process.Id);
            WriteLine("Name=" + fface.Player.Name);
            windower = fface.Windower;
            if (enableExactMatch)
                exactMatch = true;
        }

        static bool isWeakened()
        {
            StatusEffect[] statusEffects = fface.Player.StatusEffects;
            foreach (StatusEffect statusEffect in fface.Player.StatusEffects)
            {
                if (match("weak",statusEffect.ToString()))
                    return true;
            }
            return false;
        }

        static void PrintPosition()
        {
            FFACE.Position position = fface.Player.Position;            

            WriteLine("x=" + position.X);
            WriteLine("y=" + position.Y);
            WriteLine("z=" + position.Z);
            WriteLine("h=" + position.H);
            WriteLine("Zone=" + fface.Player.Zone);

            StatusEffect[] statusEffects = fface.Player.StatusEffects;
            foreach (StatusEffect statusEffect in fface.Player.StatusEffects)
            {
                String stringStatus = statusEffect.ToString();
                if (!match("unknown",stringStatus))
                    Console.Write(statusEffect+" ");
            }
            Console.WriteLine();

            if (fface.Menu.IsOpen)
            {
                WriteLine("Menu.Selection=" + fface.Menu.Selection);
                WriteLine("Menu.Name=" + fface.Menu.Name);
                WriteLine("Menu.Help=" + fface.Menu.Help);
            }

            WriteLine("Player.Status="+fface.Player.Status);
            WriteLine("Player.ID=" + String.Format("{0:X}", fface.Player.ID));
            WriteLine("Target.Name=" + fface.Target.Name);
            WriteLine("Target.ID=" + String.Format("{0:X}", fface.Target.ID));
            WriteLine("Target.HPPCurrent=" + fface.Target.HPPCurrent);
            WriteLine("Target.Status=" + fface.Target.Status);
            WriteLine("Target.IsLocked=" + fface.Target.IsLocked);
            WriteLine("Target.PosX=" + fface.Target.PosX);
            WriteLine("Target.PosY=" + fface.Target.PosY);
            WriteLine("Target.PosZ=" + fface.Target.PosZ);
            WriteLine("Item.SelectedItemNum=" + fface.Item.SelectedItemNum);
            WriteLine("Item.SelectedItemID=" + fface.Item.SelectedItemID);
            WriteLine("Item.SelectedItemName=" + fface.Item.SelectedItemName);
            WriteLine("Item.SelectedItemIndex=" + fface.Item.SelectedItemIndex);

            

            bool claimed = false;
            int claimId = 0;
            int targetId = fface.Target.ID;
            if (targetId != 0)
            {
                claimed = fface.NPC.IsClaimed(targetId);
                claimId = fface.NPC.ClaimedID(targetId);
            }

            isClaimed();

            WriteLine("Question=" + fface.Menu.DialogText.Question);
            WriteLine("fface.Menu.DialogOptionIndex="+fface.Menu.DialogOptionIndex);
            foreach (string option in fface.Menu.DialogText.Options)
            {
                WriteLine("- "+option);
            }
            WriteLine("fface.Menu.Name=" + fface.Menu.lastTradeMenuStatus);
            WriteLine("fface.Menu.Help=" + fface.Menu.Help);

            Dictionary<byte, FFACE.PartyMemberTools> partyMember = fface.PartyMember;
            for (byte i = 0; i < 18; i++)
            {
                if (partyMember[i].Active)
                {
                    int serverId = partyMember[i].ServerID;
                    WriteLine(partyMember[i].Name + " " + partyMember[i].ServerID + " " + partyMember[i].ID);
                }
            }


            for (int i = 0; i < 4000; i++)
            {
                if (fface.NPC.IsActive(i) && fface.NPC.Name(i).Length > 0) 
                {
                    Console.WriteLine("npc" + i + "=" + ToString(fface.NPC.GetPosition(i)) + " " + String.Format("{0:X}", i) + " " + (fface.NPC.IsActive(i) ? "O" : "X") + " " + fface.NPC.Status(i) + "\t" + fface.NPC.Name(i));
                }
            }

        }

        static void Move()
        {
            float x, y, z;
            x = float.Parse(args[parameterIndex]);
            y = float.Parse(args[parameterIndex+1]);
            z = float.Parse(args[parameterIndex+2]);
            WriteLine("Moving to " + x + "," + z);
            fface.Navigator.DistanceTolerance = 10;
            // fface.Navigator.GotoXZ(() => x, () => z, true);
        }

        /*
   When you press CTRL+C, the read operation is interrupted and the 
   console cancel event handler, myHandler, is invoked. Upon entry 
   to the event handler, the Cancel property is false, which means 
   the current process will terminate when the event handler terminates. 
   However, the event handler sets the Cancel property to true, which 
   means the process will not terminate and the read operation will resume.
*/
        protected static void myHandler(object sender, ConsoleCancelEventArgs args)
        {
            // Announce that the event handler has been invoked.
            loop = false;
            terminated = true;
            WriteLine("Terminating cleanly");

            /*
            fface.Windower.SendKey(KeyCode.NP_Number4, false);
            fface.Windower.SendKey(KeyCode.NP_Number6, false);
            fface.Windower.SendKey(KeyCode.NP_Number8, false);
            fface.Windower.SendKey(KeyCode.NP_Number5, false);
             */
            fface.Windower.SendKey(KeyCode.TabKey, false);
            fface.Windower.SendKey(KeyCode.LeftArrow, false);
            fface.Windower.SendKey(KeyCode.RightArrow, false);

            args.Cancel = true;
        }

        static void SetMyHandler()
        {
            // Turn off the default system behavior when CTRL+C is pressed. When 
            // Console.TreatControlCAsInput is false, CTRL+C is treated as an
            // interrupt instead of as input.
            try
            {
                Console.TreatControlCAsInput = false;

            // Establish an event handler to process key press events.
            Console.CancelKeyPress += new ConsoleCancelEventHandler(myHandler);
            }
            catch (Exception e)
            {
                // under java cant set handler
            }
        }

        static void scan()
        {
            WriteLine("["+DateTime.Now+"] Scan()="+scanArgument);
            while (!terminated)
            {
                try
                {
                    FFACE.ChatTools.ChatLine Line = fface.Chat.GetNextLine();
                    if (!string.IsNullOrEmpty(Line.Text))
                    {
                        
                        WriteLine(Line.Now + Line.Text);
                        Regex regex = new Regex(scanArgument, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                        matchArgument = regex.Match(Line.Text);
                        if (matchArgument.Success)
                        {
                            //execParameter = matches;
                            string m = execArgument = matchArgument.Groups[1].Value;
                            //WriteLine("m=" + m);
                            doExec();
                        }
                        //WriteLine(Line.Text);
                    }
                }
                catch (Exception e)
                {
                }
                Thread.Sleep(100);

            }
        }

        static NameCommand ResolveCommand(string name)
        {
            Dictionary<string, NameCommand>.KeyCollection keys = NameCommand.map.Keys;
            NameCommand command = null;
            int compare = 0x7fffffff;
            foreach (string key in keys)
            {
                if (Program.match(name, key))
                {
                    int value = Math.Abs(NameCommand.map[key].name.CompareTo(name));
                    if (value < compare)
                    {
                        command = NameCommand.map[key];
                        compare = value;
                    }
                   
                }
            }
            return command;
        }

        string Normalize(string name)
        {
            string lower = name.ToLowerInvariant();
            string[] split = lower.Split();
            string normalized = "";
            foreach (string s in split)
            {
                normalized += s;
            }
            return normalized;
        }

        static Dictionary<string, string> spellMap = new Dictionary<string, string>();

        static void InitNameCommand()
        {            
            AbilityList[] abilitySelf = {AbilityList.Fan_Dance};
            AbilityList[] abilityTarget = {AbilityList.Jump,AbilityList.Bully,AbilityList.Steal};

            foreach (AbilityList ability in Enum.GetValues(typeof(AbilityList)))
            {
                String name = ability.ToString();
                String target = castTarget==null?"<me>":castTarget;
                if (abilitySelf.Contains(ability))
                    target = "<me>";
                if (abilityTarget.Contains(ability))
                    target = "<t>";
                String command = "/ja \"" + name + "\" " + target;
                //Console.Write(name+"\n\t");
                NameCommand nameCommand = new NameCommand(name,command, false, (int)ability);

                //Console.WriteLine("automatch: ");
                foreach (StatusEffect statusEffect in Enum.GetValues(typeof(StatusEffect)))
                {
                    if (statusEffect.ToString().Equals(ability.ToString())) {
                        nameCommand.statusEffectFilter = statusEffect;
                    }
                }
            }
            Console.WriteLine();

            new NameCommand("Celerity", "/ja \"celerity\" <me>", false, (int)AbilityList.Stratagems);
            new NameCommand("Penury", "/ja \"Penury\" <me>", false, (int)AbilityList.Stratagems);

            new NameCommand("Quickstep", "/ja \"Quickstep\" <t>", false, (int)AbilityList.Steps);
            new NameCommand("Box Step", "/ja \"Box Step\" <t>", false, (int)AbilityList.Steps);
            new NameCommand("Stutter Step", "/ja \"Stutter Step\" <t>", false, (int)AbilityList.Steps);

            new NameCommand("Animated Flourish", "/ja \"Animated Flourish\" <t>", false, (int)AbilityList.Flourishes_I);
            new NameCommand("Desperate Flourish", "/ja \"Desperate Flourish\" <t>", false, (int)AbilityList.Flourishes_I);
            new NameCommand("Violent Flourish", "/ja \"Violent Flourish\" <t>", false, (int)AbilityList.Flourishes_I);

            new NameCommand("Drain Samba", "/ja \"Drain Samba\" <me>", false, (int)AbilityList.Sambas).statusEffectFilter = StatusEffect.Drain_Samba;
            new NameCommand("Drain Samba II", "/ja \"Drain Samba II\" <me>", false, (int)AbilityList.Sambas).statusEffectFilter = StatusEffect.Drain_Samba;
            new NameCommand("Aspir Samba", "/ja \"Aspir Samba\" <me>", false, (int)AbilityList.Sambas).statusEffectFilter = StatusEffect.Drain_Samba;
            new NameCommand("Haste Samba", "/ja \"Haste Samba\" <me>", false, (int)AbilityList.Sambas).statusEffectFilter = StatusEffect.Drain_Samba;
            
            SpellList[] spellSelf = { SpellList.Stoneskin,
                                        SpellList.Phalanx,
                                        SpellList.Phalanx_II,
                                        SpellList.Mages_Ballad,
                                        SpellList.Mages_Ballad_II,
                                        SpellList.Mages_Ballad_III,
                                        SpellList.Sentinels_Scherzo,
                                        SpellList.Chocobo_Mazurka,
                                        SpellList.Advancing_March,
                                        SpellList.Victory_March,
                                        SpellList.Battery_Charge,
                                        SpellList.Refresh,
                                        SpellList.Animating_Wail,
                                        SpellList.Utsusemi_Ichi,
                                        SpellList.Utsusemi_Ni,
                                        SpellList.Utsusemi_San,
                                        SpellList.Regeneration,
                                        SpellList.Plenilune_Embrace,
                                        SpellList.Magic_Fruit
                                    };
            // spellMap[string in the enum] = string_that_will_be_used_when_castin6g
            spellMap[SpellList.Sentinels_Scherzo.ToString()] = "Sentinel's Scherzo";
            spellMap[SpellList.Mages_Ballad.ToString()] = "Mage's Ballad";
            spellMap[SpellList.Mages_Ballad_II.ToString()] = "Mage's Ballad II";
            spellMap[SpellList.Mages_Ballad_III.ToString()] = "Mage's Ballad III";
            spellMap[SpellList.Quad_Continuum.ToString()] = "Quad. Continuum";
                        
            foreach (SpellList spell in Enum.GetValues(typeof(SpellList)))
            {
                String name = spell.ToString();
                if (spellMap.ContainsKey(name))
                    name = spellMap[name];
                String target = castTarget == null ? "<t>" : castTarget;
                if (spellSelf.Contains(spell))
                {
                    target = "<me>";
                }
                String command = "/ma \"" + name + "\" " + target;
                NameCommand nc = new NameCommand(name, command, true, (int)spell);
                if (spell.Equals(SpellList.Battery_Charge) || spell.Equals(SpellList.Refresh)) {
                    nc.statusEffectFilter = StatusEffect.Refresh;
                }
                if (spell.Equals(SpellList.Utsusemi_Ichi) || 
                    spell.Equals(SpellList.Utsusemi_Ni) ||
                    spell.Equals(SpellList.Utsusemi_San))
                {
                    nc.statusEffectFilters =  new StatusEffect[]
                        {StatusEffect.Utsusemi_1_Shadow_Left,
                        StatusEffect.Utsusemi_2_Shadows_Left,
                        StatusEffect.Utsusemi_3_Shadows_Left,
                        StatusEffect.Utsusemi_4_Shadows_Left};
                }

                if ((SpellList.Monomi_Ichi <= spell && spell <= SpellList.Tonko_San) ||
                    (SpellList.Foe_Requiem <= spell && spell <= SpellList.Foe_Lullaby_II) ||
                    spell.Equals(SpellList.Battery_Charge) || spell.Equals(SpellList.Refresh) ||
                    spell.Equals(SpellList.Refresh_II)
                    )
                {
                    nc.allowCastOnLowMP = true;
                }

                if (SpellList.Regeneration.Equals(spell) || SpellList.Plenilune_Embrace.Equals(spell) || SpellList.Regen_IV.Equals(spell) 
                    || SpellList.Regen_III.Equals(spell) 
                    || SpellList.Regen_II.Equals(spell) 
                    || SpellList.Regen.Equals(spell))
                {
                    nc.statusEffectFilter = StatusEffect.Regen;
                }

            }
            Console.WriteLine();
            foreach(string ws in Constants.weaponSkillList) {
                new NameCommand(ws);
            }
        }

        static void Inventory()
        {
            Console.WriteLine("<br>");
            Console.WriteLine("<select multiple=true size=80>");
            for (int j = 0; j < 80; j++)
            {
                FFACE.ItemTools.InventoryItem item = null;
                try
                {
                    item = fface.Item.GetInventoryItem(j);
                    Console.WriteLine("<option>");
                    Console.WriteLine(item.Location.ToString() + ":" + j + " " + FFACE.ParseResources.GetItemName(item.ID));
                    
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            Console.WriteLine("</select><br>");
            Console.WriteLine("<select multiple=true size=80>");
            for (int j = 0; j < 80; j++)
            {
                FFACE.ItemTools.InventoryItem item = null;
                try
                {
                    item = fface.Item.GetSatchelItem(j);
                    Console.WriteLine("<option>");
                    Console.WriteLine(item.Location.ToString() + ":" + j + " " + FFACE.ParseResources.GetItemName(item.ID));
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            Console.WriteLine("</select><br>");
            Console.WriteLine("<select multiple=true size=80>");
            for (int j = 0; j < 80; j++)
            {
                FFACE.ItemTools.InventoryItem item = null;
                try
                {
                    item = fface.Item.GetSackItem(j);
                    Console.WriteLine("<option>");
                    Console.WriteLine(item.Location.ToString() + ":" + j + " " + FFACE.ParseResources.GetItemName(item.ID));
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            Console.WriteLine("</select><br>");
        }

        static void Cast()
        {
            InitNameCommand();
            string[] names = castArgument.Split(',');
            try
            {
                do
                {
                    List<NameCommand> list = new List<NameCommand>();
                    foreach (string name in names)
                    {
                        NameCommand command = ResolveCommand(name);
                        if (null == command)
                        {
                            WriteLine("Can't resolve " + name);
                            return;
                        }
                        list.Add(command);
                    }
                    while (!terminated)
                    {
                        int minTime = 1;
                        foreach (NameCommand command in list) {
                            int time = command.GetTime();
                            if (time < minTime)
                                minTime = time;
                            if (0 == time)
                            {
                                if (command.hasRequiredStatus(fface))
                                {
                                    command.Cast();
                                    Thread.Sleep(1000);
                                    if (0 == command.GetTime())
                                        continue;
                                    Thread.Sleep(2000);
                                    continue;
                                }
                                else
                                {
                                    Thread.Sleep(3000);
                                    continue;
                                }                                
                            }
                            if (terminated)
                                break;
                        }
                        Thread.Sleep(minTime*1000);
                    }
                } while (!terminated && loop);
            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }

        static void commando()
        {
            if ("-send".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                execParameter = parameter;
                exec = true;
                do
                {
                    doExec();
                }
                while (loop);
            }
            else if ("-find".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                find();
            }
            else if ("-move".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("moving");
                Move();
            }
            else if ("-enter".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                KeyPress(KeyCode.EnterKey);
            }
            else if ("-select".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                WriteLine("finding " + parameter);
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(parameter, System.Text.RegularExpressions.RegexOptions.IgnoreCase);

                while (true)
                {
                    MatchCollection matches = regex.Matches(fface.Menu.Selection);
                    if (matches.Count > 0)
                        break;
                    Thread.Sleep(100);
                    windower.SendKeyPress(KeyCode.DownArrow);
                }

            }
            else if ("-scan".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                scan();
            }
            else if ("-eval".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                eval();
            }
            else if ("-synth".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    synth();
                }
                catch (Exception e)
                {
                    WriteLine("failed synth: " + e.Message);
                    return;
                }

            }
            else if ("-menu".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                menu();
            }
            else if ("-cast".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                Cast();
            }
            else if ("-hotchick".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                Hotchick();
            }
            else if ("-inventory".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                Inventory();
            }
            else if ("-locked".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                Locked();
            }
            else if ("-npc".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                npc();
            }
            else if ("-go".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                go();
            }
            else if ("-patrole".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                patrole();
            }
            else if ("-locate".Equals(command, StringComparison.OrdinalIgnoreCase))
            {
                locate();
            }
            else
            {
                PrintPosition();
            }
        }

        static void Main(string[] args)
        {
            //args = new string[] {"-loop","-scan","mitaru"};

            Program.args = args;
            GetArgs(args);
            SetProcess();
            SetMyHandler();

            WriteLine("command="+command);

            commando();

            WriteLine("Exit");
            //  SendString("/a <t>");
        }

        static void menu()
        {
            SelectItem(parameter);
            KeyPress(KeyCode.EnterKey);
            PrintPosition();

        }

        delegate string StringCallback();

        static void Wait(string pattern,StringCallback callback)
        {
            WriteLine("pattern="+pattern);
            while (!terminated)
            {
                if (fface.Target.ID == 0)
                {
                    WriteLine("  target.id=0");
                    break;
                }
                string value = callback();
                WriteLine(pattern + "~" + value);
                if (match(pattern, value))
                {
                    WriteLine("  value=" + value);
                    break;
                }
                Thread.Sleep(100);
            }
        }
        static void WaitMenu(bool open)
        {
            while (!terminated && (fface.Menu.IsOpen != open))
            {
                Thread.Sleep(100);
            }
        }


        static void Hotchick()
        {
            bool cruor_buffed = false;
            bool abyssea_visitant = false;
            bool in_abyssea = false;
            while (!terminated)
            {
                string target = fface.Target.Name;

                if (match("Conflux Surveyor", target) && !abyssea_visitant)
                {
                    // enter
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(3000);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(2000);
                    windower.SendKeyPress(KeyCode.DownArrow);
                    Thread.Sleep(200);
                    windower.SendKeyPress(KeyCode.DownArrow);
                    Thread.Sleep(200);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(1000);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(1000);
                    windower.SendKeyPress(KeyCode.DownArrow);
                    Thread.Sleep(200);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(1000);
                    windower.SendKeyPress(KeyCode.UpArrow);
                    Thread.Sleep(200);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(1000);
                    abyssea_visitant = true;
                }
                if (match("Cruor Prospector", target) && !cruor_buffed)
                {
                    // enter
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(3000);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(2000);
                    windower.SendKeyPress(KeyCode.RightArrow);
                    Thread.Sleep(200);
                    windower.SendKeyPress(KeyCode.DownArrow);
                    Thread.Sleep(200);
                    windower.SendKeyPress(KeyCode.DownArrow);
                    Thread.Sleep(200);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(500);
                    windower.SendKeyPress(KeyCode.RightArrow);
                    Thread.Sleep(500);
                    windower.SendKeyPress(KeyCode.RightArrow);
                    Thread.Sleep(500);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(500);
                    windower.SendKeyPress(KeyCode.UpArrow);
                    Thread.Sleep(200);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(1000);
                    cruor_buffed = true;
                }
                if (match("Mew", target) && !in_abyssea)
                {
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(1500);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(1500);
                    windower.SendKeyPress(KeyCode.UpArrow);
                    Thread.Sleep(200);
                    windower.SendKeyPress(KeyCode.EnterKey);
                    Thread.Sleep(1000);
                    in_abyssea = true;
                    abyssea_visitant = false;
                    cruor_buffed = false;
                }
                Thread.Sleep(100);
            }
        }

        static double xzdistance(FFACE.Position p1, FFACE.Position p2)
        {
            float dx = p1.X - p2.X;
            float dz = p1.Z - p2.Z;

            return Math.Sqrt(dx * dx + dz * dz);
        }
        static void Locked()
        {
            string[] parties = partiesArgument.Split(',');
            while (!terminated)
            {
                if (fface.Target.ID != 0 && fface.Target.IsLocked)
                {
                    bool mob = "16".Equals(fface.Target.Type.ToString());
                    double distanceToFollow = mob ? 3 : 7;
                    double xzd = xzdistance(fface.Target.Position, fface.Player.Position);
                    if (fface.Target.IsLocked && xzd > distanceToFollow)
                    {
                        WriteLine("Following target " + fface.Target.Name + " " + xzd);
                        int i = 0;
                        while (fface.Target.IsLocked && xzd > distanceToFollow)
                        {
                            xzd = xzdistance(fface.Target.Position, fface.Player.Position);
                            windower.SendKey(KeyCode.NP_Number8, true);
                            Thread.Sleep(100);
                            windower.SendKey(KeyCode.NP_Number8, false);
                            i++;
                            if ((i % 10) == 0)
                            {
                                KeyPress(KeyCode.NP_Number4);
                                WriteLine("Left");
                            }
                            if (i == 100)
                            {
                                WriteLine("Giving up");
                                KeyPress(KeyCode.NP_Number2);
                                KeyPress(KeyCode.NP_Number4);
                                KeyPress(KeyCode.NP_Number4);
                                KeyPress(KeyCode.NP_Number4);
                                KeyPress(KeyCode.NP_Number8);
                                break;
                            }
                        }
                    }
                }
                else
                {
                    foreach (string target in parties)
                    {
                        SendString("/ta "+target);
                        if (fface.Target.ID 
                            != 0)
                        {
                            SendString("/lockon");
                            break;
                        }
                    }
                }
                Thread.Sleep(100);
            }
        }

        static string Shit(double x)
        {
            return (x < 0?"":"+")+String.Format("{0:000.00}", x);

        }
        static string ToString(FFACE.Position p)
        {
            return Shit(p.X) + "," + Shit(p.Z) + " " + String.Format("{0:0.000}",p.H);
        }


        static void go() 
        {

            string[] destinations = goArgument.Split(';');

            TurnMe turnMe = new TurnMe(fface);
            foreach (string destination in destinations)
            {
                string[] xy = destination.Split(',');
                double distanceToGoal = 1;
                FFACE.Position p = new FFACE.Position();
                p.X = float.Parse(xy[0]);
                p.Z = float.Parse(xy[1]);

                Console.WriteLine("player     =" + ToString(fface.Player.Position));
                Console.WriteLine("destination=" + ToString(p));
                
                Throttle throttleLog = new Throttle(3, true);
                while (!terminated)
                {
                    double dx = p.X - fface.Player.PosX;
                    double dz = p.Z - fface.Player.PosZ;
                    double h = Math.Atan2(-dz, dx);
                    double dd = Math.Sqrt(dx * dx + dz * dz);
                    double dh = h - fface.Player.PosH;
                    if (dh < -Math.PI) dh = 2 * Math.PI + dh;
                    if (dh > Math.PI) dh = 2 * Math.PI - dh;


                    if (dd <= distanceToGoal)
                    {
                        // WriteLine("stopIt dd=" + dd + " distanceToGoal=" + distanceToGoal);
                        // turnMe.stopIt();
                        break;
                    }

                    double directionError = 0.14;
                    /*
                    if (dd < 20)
                    {
                        directionError = 0.1+(dd / 100.0);
                    }
                    if (dd < 10)
                    {
                        directionError = 3*dd/10.0;
                    }
                    if (dd < 6)
                    {
                        directionError = Math.PI;
                    }
                     */

                    if (throttleLog.isReady())
                    {
                        WriteLine("please face " + String.Format("{0:0.00}", h) + " dd = " + String.Format("{00:0.00}", dd) + " dh = " + String.Format("{00:0.00}", dh));
                        // WriteLine("directionError=" + directionError);
                    }



                    if ((-directionError < dh) && (dh < directionError))
                    {
                        // turnMe.stopTurning();
                    }
                    else
                    {
                        WriteLine("Turning dh="+dh);
                        turnMe.stopIt();
                        Thread.Sleep(100);
                        SetNPCPosH(fface._InstanceID, fface.Player.ID, (float)h);
                        Thread.Sleep(100);
                        if (fface.Player.ViewMode != ViewMode.FirstPerson)
                        {
                            fface.Windower.SendKeyPress(KeyCode.NP_Number5);
                            Thread.Sleep(100);
                        }
                        //WriteLine("Turning Done");
                    }

                    if (dd > distanceToGoal)
                    {
                        turnMe.startRunning();
                    }
                }

                if (terminated)
                {
                    break;
                }
            }

            turnMe.stopIt();

        }

        static void toggleView()
        {
            if (fface.Player.ViewMode != ViewMode.FirstPerson)
            {
                fface.Windower.SendKeyPress(KeyCode.NP_Number5);
                Thread.Sleep(100);
            }
        }

        static void npc()
        {
            string[] ids = npcArgument.Split(',');

            Throttle throttleLog = new Throttle(10, true);

            while (!terminated)
            {
                bool throttleLogIsReady = throttleLog.isReady();

                if (throttleLogIsReady)
                {
                    Console.WriteLine("player=" + ToString(fface.Player.Position));
                    Console.WriteLine("target=" + ToString(fface.Target.Position) + " " + String.Format("{0:X}", fface.Target.ID) + "\t" + fface.Target.Name);
                }
                foreach (string id in ids)
                {
                    int i = Int32.Parse(id, System.Globalization.NumberStyles.HexNumber);
                    if (throttleLogIsReady)
                        Console.WriteLine("npc   =" + ToString(fface.NPC.GetPosition(i)) + " " + String.Format("{0:X}", i) + " " + (fface.NPC.IsActive(i)?"O":"X") + " " + fface.NPC.Status(i) + "\t" + fface.NPC.Name(i));

                    TurnMe turnMe = new TurnMe(fface);
                    while ((match("standing", fface.NPC.Status(i).ToString()) || match("fighting", fface.NPC.Status(i).ToString()))
                        && fface.Player.ViewMode == FFACETools.ViewMode.FirstPerson && fface.NPC.IsActive(i))
                    {
                        throttleLogIsReady = throttleLog.isReady();
                        double distanceToGoal = distanceArgument;

                        if (terminated)
                            break;

                        FFACE.Position p = fface.NPC.GetPosition(i);

                        if (match("fighting", fface.Player.Status.ToString()))
                        {
                            p = fface.Target.Position;
                        }

                        double dx = p.X - fface.Player.PosX;
                        double dz = fface.Player.PosZ - p.Z;
                        double h = Math.Atan2(dz, dx);
                        double dd = Math.Sqrt(dx * dx + dz * dz);
                        if (throttleLogIsReady)
                        {
                            Console.WriteLine("goal h=" + h + " dd=" + dd + " dest= " + p.X + "," + p.Z);
                        }

                        if (dd < distanceToGoal)
                        {
                            if (execParameter != null)
                            {
                                doExec();
                            }
                            break;
                        }

                        double dh = h - fface.Player.PosH;
                        if (dh < -Math.PI) dh = 2 * Math.PI + dh;
                        if (dh > Math.PI) dh = 2 * Math.PI - dh;

                        double distanceError = 0.15;
                        bool goalFar = dd > distanceToGoal;
                        if (!goalFar || (-distanceError < dh && dh < distanceError))
                        {

                        }
                        else
                        {
                            turnMe.stopIt();
                            Thread.Sleep(100);
                            SetNPCPosH(fface._InstanceID, fface.Player.ID, (float)h);
                            Thread.Sleep(100);
                        }

                        if (dd > distanceToGoal)
                        {
                            turnMe.startRunning();
                        }
                    }
                    turnMe.stopIt();
                }
            }
        }

        static void patrole()
        {
            Patrole patrole = new Patrole(fface);
            if (patroleArgument.Length > 0)
                patrole.setRoute(patroleArgument);
            patrole.setWantedTarget(patroleTarget);
            Console.WriteLine("execParameter=" + execParameter);
            patrole.setActionWhenFound(execParameter);
            findArgument = patroleTarget;
            while (!terminated)
            {
                patrole.runIteration();
            }
            patrole.turnMe.stopIt();
        }

        static void locate()
        {
            Console.WriteLine("target="+target);
            string[] ids = target.Split(',');
            Throttle throttleLog = new Throttle(10, true);

            while (!terminated)
            {
                bool throttleLogIsReady = throttleLog.isReady();

                if (throttleLogIsReady)
                {
                    Console.WriteLine("player=" + ToString(fface.Player.Position));
                    Console.WriteLine("target=" + ToString(fface.Target.Position) + " " + String.Format("{0:X}", fface.Target.ID) + "\t" + fface.Target.Name);
                }
                foreach (string id in ids)
                {
                    int i = Int32.Parse(id, System.Globalization.NumberStyles.HexNumber);
                    if (throttleLogIsReady)
                        Console.WriteLine("npc   =" + ToString(fface.NPC.GetPosition(i)) + " " + String.Format("{0:X}", i) + " " + (fface.NPC.IsActive(i) ? "O" : "X") + " " + fface.NPC.Status(i) + "\t" + fface.NPC.Name(i));

                    if ((match("standing", fface.NPC.Status(i).ToString()) || match("fighting", fface.NPC.Status(i).ToString()))
                        && fface.NPC.IsActive(i))
                    {
                        if (execParameter != null && throttleLogIsReady)
                            doExec();
                    }
                }
            }
        }


    } // end class


} // end namespace

