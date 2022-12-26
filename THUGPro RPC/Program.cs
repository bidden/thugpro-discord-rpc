using DiscordRPC;
using DiscordRPC.Logging;
using DiscordRPC.Message;
using Memory;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace THUGPro_RPC
{
    internal class Program
    {
        static DiscordRpcClient client;

        static Mem meme = new Mem();

        static int GetProcId(string name)
        {
            foreach (Process proc in Process.GetProcessesByName(name))
            {
                return proc.Id;
            }
            return 0;
        }
        static float multiplier()
        {
            int procId = GetProcId(name: "THUGPro");
            if (procId == 0)
            {
                Environment.Exit(-1);
            }
            meme.OpenProcess(procId);
            return meme.ReadFloat("THUGPro.exe+0x003D1210,0xBB0");
        }
        static int score()
        {
            int procId = GetProcId(name: "THUGPro");
            if (procId == 0)
            {
                Environment.Exit(-1);
            }
            meme.OpenProcess(procId);
            return meme.ReadInt("THUGPro.exe+0x003D1210,0xB64");
        }
        static int multiplied_1()
        {
            int procId = GetProcId(name: "THUGPro");
            if (procId == 0)
            {
                Environment.Exit(-1);
            }
            meme.OpenProcess(procId);
            return meme.ReadInt("THUGPro.exe+0x003D1210,0xB70");
        }
        static int multiplied_2()
        {
            int procId = GetProcId(name: "THUGPro");
            if (procId == 0)
            {
                Environment.Exit(-1);
            }
            meme.OpenProcess(procId);
            return meme.ReadInt("THUGPro.exe+0x003D1210,0xB74");
        }
        static async Task Main(string[] args)
        {
            client = new DiscordRpcClient("1056912132899356732");

            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning };

            client.OnReady += (sender, e) =>
            {
                Console.WriteLine("Received Ready from user {0}", e.User.Username);
            };

            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine("Received Update! {0}", e.Presence);
            };

            client.Initialize();
            new Thread(async () =>
            {
                while (true)
                {
                    await Task.Delay(3000);
                    client.SetPresence(new RichPresence()
                    {
                        Details = $"Normal Score -> {score()} x {multiplier()}",
                        State = $"Multiplied Score -> {multiplied_2()}",
                        Assets = new Assets()
                        {
                            LargeImageKey = "512",
                            LargeImageText = "github.com/bidden",
                        }
                    });
                }
            }).Start();
            Console.ReadKey();
        }
    }
}
