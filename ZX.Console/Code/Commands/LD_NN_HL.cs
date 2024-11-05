namespace ZX.Console.Code.Commands;

public class LD_NN_HL : Cmd
{
    public override byte[] Range => [0b00_100_010];
    public override void Execute(Z80 cpu)
    {
        var addr = ReadShort(cpu);
        cpu.Memory[addr] = cpu.Reg.L;
        cpu.Memory[(ushort)(addr+1)] = cpu.Reg.H;
    }

    public override Cmd Init(byte shift)
    {
        return new LD_NN_HL(){Ticks = 16};
    }
    public override string ToString() => $"LD (NN), HL";

}
public class LD_HL_NN : Cmd
{
    public override byte[] Range => [0b00_101_010];
    public override void Execute(Z80 cpu)
    {
        var addr = ReadShort(cpu);
        cpu.Reg.L = cpu.Memory[addr] ;
        cpu.Reg.H = cpu.Memory[(ushort)(addr+1)] ;
    }

    public override Cmd Init(byte shift)
    {
        return new LD_HL_NN(){Ticks = 16};
    }
    public override string ToString() => $"LD HL, (NN)";
}
