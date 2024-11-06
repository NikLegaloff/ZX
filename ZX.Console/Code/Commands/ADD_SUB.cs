using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZX.Console.Code.Commands;

public class ADD_A_R : ADD_SUB_BASE
{
    public override byte[] Range => [
        0b10000_000,
        0b10000_001,
        0b10000_010,
        0b10000_011,
        0b10000_100,
        0b10000_101,
        0b10000_110,
        0b10000_111
    ];

    public override string ToString() => "ADD A, " + _code;
    public override Cmd Init(byte shift) => new ADD_A_R { _code = (Reg8Code)shift };
    protected override bool IsCarry => false;
    protected override bool IsAdd => true;
}
public class ADC_A_R : ADD_SUB_BASE
{
    public override byte[] Range => [
        0b10001_000,
        0b10001_001,
        0b10001_010,
        0b10001_011,
        0b10001_100,
        0b10001_101,
        0b10001_110,
        0b10001_111
    ];

    public override string ToString() => "ADC A, " + _code;
    public override Cmd Init(byte shift) => new ADC_A_R { _code = (Reg8Code)shift };
    protected override bool IsCarry => true;
    protected override bool IsAdd => true;
}

public class SUB_A_R : ADD_SUB_BASE
{
    public override byte[] Range => [
        0b10010_000,
        0b10010_001,
        0b10010_010,
        0b10010_011,
        0b10010_100,
        0b10010_101,
        0b10010_110,
        0b10010_111
    ];

    public override string ToString() => "SUB A, " + _code;
    public override Cmd Init(byte shift) => new SUB_A_R { _code = (Reg8Code)shift };
    protected override bool IsCarry => false;
    protected override bool IsAdd => false;
}
public class SBC_A_R : ADD_SUB_BASE
{
    public override byte[] Range => [
        0b10011_000,
        0b10011_001,
        0b10011_010,
        0b10011_011,
        0b10011_100,
        0b10011_101,
        0b10011_110,
        0b10011_111
    ];

    public override string ToString() => "SBC A, " + _code;
    public override Cmd Init(byte shift) => new SBC_A_R { _code = (Reg8Code)shift };
    protected override bool IsCarry => true;
    protected override bool IsAdd => false;
}

public abstract class ADD_SUB_BASE : Cmd
{
    protected Reg8Code _code;
    protected abstract bool IsCarry { get; }
    protected abstract bool IsAdd { get; }

    public override void Execute(Z80 cpu)
    {
        var a = cpu.Reg.A;
        var operand = Get(cpu, _code);
        var carry_value = IsCarry && cpu.Reg.F.C ? 1 : 0;
        var old_a = a;

        if (IsAdd)
        {
            var res =a+ operand + carry_value;
            var care = (a & 0x100)>0;

            cpu.Reg.A = (byte)(res & 0xff);

            cpu.Reg.F.SetZS53(a);
            cpu.Reg.F.H = (((old_a & 0x0f) + (operand & 0x0f) + carry_value) & 0x10)>0;
            cpu.Reg.F.PV = get_byte_sum_overflow(old_a, operand, a);
            cpu.Reg.F.N = false;
            cpu.Reg.F.C = care;
        }
        else
        { // subtraction
            int res =a-(operand + carry_value);
            var borrow = res < 0;
            cpu.Reg.A=(byte)(res&0xff);
            cpu.Reg.F.SetZS53(a);
            cpu.Reg.F.H = (old_a & 0x0f) - (operand & 0x0f) - carry_value < 0;
            cpu.Reg.F.PV = get_byte_diff_overflow(old_a, operand, a);
            cpu.Reg.F.N = true;
            cpu.Reg.F.C = borrow;
        }
    }

}