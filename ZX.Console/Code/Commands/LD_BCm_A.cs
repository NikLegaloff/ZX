namespace ZX.Console.Code.Commands;

public class LD_BCm_A : Cmd
{
    public override byte[] Range => [0b00_000_010];
    public override void Execute(Z80 cpu)
    {
        cpu.Memory[cpu.Reg.BC] = cpu.Reg.A;
    }

    public override Cmd Init(byte shift) => new LD_BCm_A(){Ticks = 7};
    public override string ToString() => $"LD (BC), A";

}

public class LD_A_BCm : Cmd
{
    public override byte[] Range => [0b00_001_010];
    public override void Execute(Z80 cpu)
    {
        cpu.Reg.A = cpu.Memory[cpu.Reg.BC];
    }

    public override Cmd Init(byte shift) => new LD_A_BCm() { Ticks = 7 };
    public override string ToString() => $"LD A, (BC)";

}

public class LD_DEm_A : Cmd
{
    public override byte[] Range => [0b00_010_010];
    public override void Execute(Z80 cpu)
    {
        cpu.Memory[cpu.Reg.DE] = cpu.Reg.A;
    }

    public override Cmd Init(byte shift) => new LD_DEm_A() { Ticks = 7 };
    public override string ToString() => $"LD (DE), A";

}

public class LD_A_DEm : Cmd
{
    public override byte[] Range => [0b00_011_010];
    public override void Execute(Z80 cpu)
    {
        cpu.Reg.A = cpu.Memory[cpu.Reg.DE];
    }

    public override Cmd Init(byte shift) => new LD_A_DEm() { Ticks = 7 };
    public override string ToString() => $"LD A, (DE)";

}