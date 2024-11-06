namespace ZX.Console.Code;

public class Memory
{
    public Memory(byte[] rom)
    {
        int i = 0;
        foreach (var b in rom) _data[i++] = b;
    }

    public void LoadFile(string name, ushort addr)
    {
        var i = addr;
        foreach (var b in File.ReadAllBytes(name)) _data[i++]=b; 
    }

    public byte[] _data = new byte[65536];
    public byte this[ushort a]
    {
        get
        {
            return _data[a];
        }
        set
        {
            if (a < 16384) return;
            _data[a] = value;
        }
    }

    
}