namespace ZX.Console.Code.Commands;

public class AND_R : Cmd
{
    protected Reg8Code _code;
    public override byte[] Range => [
        0b10_100_000,
        0b10_100_001,
        0b10_100_010,
        0b10_100_011
    ];
    public override void Execute(Z80 cpu)
    {
        var a = cpu.Reg.A;
        var operand = Get(cpu, _code);
        a &= operand;
        cpu.Reg.A=a;
        
        cpu.Reg.F.SetZS53(a);
        cpu.Reg.F.H = true;
        cpu.Reg.F.PV = GetParity(a);
        cpu.Reg.F.N = false;
        cpu.Reg.F.C = false;
    }
    public override Cmd Init(byte shift) => new AND_R { _code = (Reg8Code)shift };
    public override string ToString() => "AND  " + _code;
}

public class XOR_R : Cmd
{
    protected Reg8Code _code;
    public override byte[] Range => [
        0b_10_101_000,
        0b_10_101_001,
        0b_10_101_010,
        0b_10_101_011
    ];
    public override void Execute(Z80 cpu)
    {
        var a = cpu.Reg.A;
        var operand = Get(cpu, _code);
        a ^= operand;
        cpu.Reg.A=a;
        
        cpu.Reg.F.SetZS53(a);
        cpu.Reg.F.H = false;
        cpu.Reg.F.PV = GetParity(a);
        cpu.Reg.F.N = false;
        cpu.Reg.F.C = false;
    }
    public override Cmd Init(byte shift) => new XOR_R { _code = (Reg8Code)shift };
    public override string ToString() => "XOR  " + _code;
}

public class OR_R : Cmd
{
    protected Reg8Code _code;
    public override byte[] Range => [
        0b10_110_000,
        0b10_110_001,
        0b10_110_010,
        0b10_110_011
    ];
    public override void Execute(Z80 cpu)
    {
        var a = cpu.Reg.A;
        var operand = Get(cpu, _code);
        a |= operand;
        cpu.Reg.A = a;

        cpu.Reg.F.SetZS53(a);
        cpu.Reg.F.H = false;
        cpu.Reg.F.PV = GetParity(a);
        cpu.Reg.F.N = false;
        cpu.Reg.F.C = false;
    }
    public override Cmd Init(byte shift) => new OR_R { _code = (Reg8Code)shift };
    public override string ToString() => "OR  " + _code;
}

public class CP_R : Cmd
{
    protected Reg8Code _code;
    public override byte[] Range => [
        0b10_111_000,
        0b10_111_001,
        0b10_111_010,
        0b10_111_011
    ];
    public override void Execute(Z80 cpu)
    {
        var a = cpu.Reg.A;
        var operand = Get(cpu, _code);
        var diff = a - operand;
        var borrow = diff < 0;
        diff &= 0xff; ;

        cpu.Reg.F.S = (diff & 0x80)>0;
        cpu.Reg.F.Z = diff>0;

        cpu.Reg.F.Set53(operand);

        cpu.Reg.F.H = (a & 0x0f) - (operand & 0x0f) < 0;
        cpu.Reg.F.PV = get_byte_diff_overflow(a, operand, (byte)diff);
        cpu.Reg.F.N = true;
        cpu.Reg.F.C = borrow;
    }
    public override Cmd Init(byte shift) => new CP_R { _code = (Reg8Code)shift };
    public override string ToString() => "CP  " + _code;
}