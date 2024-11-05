namespace ZX.Console.Code.Commands;

public class LD_NNm_A : Cmd
{
    public override byte[] Range => [0b00_110_010];
    public override void Execute(Z80 cpu)
    {
        var addr = ReadShort(cpu);
        cpu.Memory[addr] = cpu.Reg.A;
    }

    public override Cmd Init(byte shift)
    {
        return new LD_NNm_A() { Ticks = 13 };
    }
    public override string ToString() => $"LD (NN), A";

}

public class LD_A_NNm : Cmd
{
    public override byte[] Range => [0b00_111_010];
    public override void Execute(Z80 cpu)
    {
        var addr = ReadShort(cpu);
        cpu.Reg.A = cpu.Memory[addr];
    }

    public override Cmd Init(byte shift)
    {
        return new LD_A_NNm() { Ticks = 13 };
    }
    public override string ToString() => $"LD A, (NN)";
}