using System;
using System.Runtime.InteropServices;

class Botnana
{
    [DllImport("libbotnana", EntryPoint="start")]
    public static extern void Start(string connection);

    static public void Main()
    {
        Botnana.Start("ws://127.0.0.1:2794");
    }
}
