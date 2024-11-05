namespace ZX.Console.Code;

public class Z80Instruction
{
    public Cmd Cmd { get; set; }
    public byte? Shift { get; set; }
    public byte? Arg8{ get; set; }
    public ushort? Arg16{ get; set; }
}
public class Z80Compiler
{
    List<Z80Instruction> _commands = new List<Z80Instruction>();

    public void Add(Cmd cmd,Byte? shift=null, byte? arg8 = null, ushort? arg16 = null)
    {
        
        Cmd toAdd = cmd;
        if (shift != null)
        {
            toAdd = cmd.Init(shift.Value);
        }
        _commands.Add(new Z80Instruction{Cmd = toAdd,Shift = shift, Arg8 = arg8,Arg16 = arg16});
    }

    public byte[] Compile()
    {
        var res = new List<byte>();
        foreach (var cmd in _commands)
        {
            if (cmd.Shift==null) res.Add(cmd.Cmd.Range[0]); else res.Add(cmd.Cmd.Range[cmd.Shift.Value]);
            if (cmd.Arg8!=null) res.Add(cmd.Arg8.Value);
            else if (cmd.Arg16 != null)
            {
                res.Add((byte)(cmd.Arg16.Value%256));
                res.Add((byte)(cmd.Arg16.Value/256));
            }

        }
        return res.ToArray();
    }

}