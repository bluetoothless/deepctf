using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;
using System.Diagnostics;
using System.Linq;

public static class Runner
{ 
    private static string Id = "2022-10-12_07-56-29_id";
    private static string Environment_path = ".\\..\\.."; // \deepctf;
    private static bool Resume = false;
    private static bool graphics = false;
    private static Process process;

    public static bool Check_id()
    {
        return Directory.Exists(Environment_path + "\\results\\" + Id);
    }

    public static void Get_new_Id()
    {
        Id = DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss") + "_id";
    }

    public static string CD()
    {
        return "cd " + Environment_path;
    }

    public static string Buid_environment()
    {
        return ".\\deepCTFenv\\Scripts\\activate";
    }

    public static string MLagents_learn(int nrOfEnvs)
    {
        string Configuration_path = ".\\CTF_Data\\StreamingAssets\\configuration.yaml";
        string Env_path = "MainSceneBuild\\CTF";


        string resume = Resume && Check_id() ? " --resume" : "";
        string no_graphics = graphics ? "" : " --no-graphics";
        if (!Resume)
            Get_new_Id();

        return "mlagents-learn " + Configuration_path +
            " --run-id=" + Id + "" +
            " --env=" + Env_path +
            " --num-envs=" + nrOfEnvs +
            no_graphics + resume;
    }

    public static string GetCommand(int nrOfEnvs)
    {
        return Buid_environment() + " & " + MLagents_learn(nrOfEnvs);
    }

    public static void Execute(int nrOfEnvs)
    {
        string command =
            /*CD() + " ; " +*/
            Buid_environment() + " & " +
            MLagents_learn(nrOfEnvs);

        CMD_Execute(command);
    }

    public static void CMD_Execute(string command)
    {
        process = new Process();
        process.StartInfo.FileName = "cmd.exe";
        //process.StartInfo.Arguments = command;
        // UnityEngine.Debug.Log("cmd.exe " + command);
        process.Start();
    }

    public static void Finish()
    {
        // UnityEngine.Debug.Log("Finishing... process name: " + process.ProcessName 
            // +  ", process ID: " + process.Id);
        //var closingProcess = new Process();
        //closingProcess.StartInfo.FileName = "taskkill";
        //closingProcess.StartInfo.Arguments = "/F /PID " + process.Id;
        //closingProcess.StartInfo.CreateNoWindow = true;
        //closingProcess.Start();
        process.Kill();
        // UnityEngine.Debug.Log("Closed");
    }
}
