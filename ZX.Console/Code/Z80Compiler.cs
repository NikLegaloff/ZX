namespace ZX.Console.Code;

public class Z80Instruction
{
    public Cmd Cmd { get; set; }
    public byte? Shift { get; set; }
    public byte? ArgLow{ get; set; }
    public byte? ArgHi{ get; set; }
}
public class Z80Compiler
{
    List<Z80Instruction> _commands = new();
    public void Add(Cmd cmd, byte? shift=null, byte? argLow = null, byte? argHi = null)
    {
        var toAdd = cmd;
        if (shift != null) toAdd = cmd.Init(shift.Value);
        _commands.Add(new Z80Instruction{Cmd = toAdd,Shift = shift, ArgLow = argLow, ArgHi = argHi });
    }

    public byte[] Compile()
    {
        var res = new List<byte>();
        foreach (var cmd in _commands)
        {
            res.Add(cmd.Shift == null ? cmd.Cmd.Range[0] : cmd.Cmd.Range[cmd.Shift.Value]);
            if (cmd.ArgLow!=null) res.Add(cmd.ArgLow.Value);
            if (cmd.ArgHi != null) res.Add(cmd.ArgHi.Value);
        }
        return res.ToArray();
    }

}