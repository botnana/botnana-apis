using System;
using System.Runtime.InteropServices;

internal class Native {
    [DllImport("libbotnana")]
    internal static extern SenderHandle start(string connection);

    [DllImport("libbotnana")]
    internal static extern void poll(SenderHandle sender);

    [DllImport("libbotnana")]
    internal static extern void sender_free(IntPtr sender);

}

internal class SenderHandle : SafeHandle {

    public SenderHandle() : base(IntPtr.Zero, true) {}

    public override bool IsInvalid
    {
        get { return false; }
    }

    protected override bool ReleaseHandle() {
        Native.sender_free(handle);
        return true;
    }
}

class Botnana : IDisposable
{
    private SenderHandle sender;

    public Botnana(string connection)
    {
        sender = Native.start(connection);
    }
    public void Poll() {
        Native.poll(sender);
    }
    public void Dispose()
    {
        sender.Dispose();
    }

    static public void Main()
    {
        var botnana = new Botnana("ws://127.0.0.1:2794");
        botnana.Poll();
    }
}
