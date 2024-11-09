using System;

namespace ZX.Console.Code;

public class Z80Bus
{
    private byte[] _data = new byte[65536];

    Dictionary<ushort, List<Action<byte>>> _listeners = new Dictionary<ushort, List<Action<byte>>>();
    Dictionary<ushort, Func<byte>> _devices = new Dictionary<ushort, Func<byte>>();

    public Z80Bus()
    {
        for (int i = 0; i < 65536; i++) _data[i] = 0;
    }

    public void AddDevice(ushort a, Func<byte> action)
    {
        if (_devices.ContainsKey(a)) throw new Exception("Address " + a + " busy");
        _devices.Add(a, action);
    }
    public void AddListener(ushort port, Action<byte> action)
    {
        if (!_listeners.ContainsKey(port)) _listeners.Add(port, new List<Action<byte>>{action}); else _listeners[port].Add(action);
    }


    public byte Get(ushort a)
    {
        if (_devices.ContainsKey(a)) return _devices[a]();
        return _data[a];
    }

    public void Set(ushort a, byte b)
    {
        if (_listeners.ContainsKey(a)) 
            foreach (var action in _listeners[a]) action(b);
        else 
            _data[a] = b;
        
    }
}