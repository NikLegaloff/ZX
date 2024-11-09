namespace ZX.Console.Code.Commands;

public class RET_C : Cmd
{
    public override byte[] Range => [
        0b_11_000_000,
        0b_11_001_000,
        0b_11_010_000,
        0b_11_011_000,
        0b_11_100_000,
        0b_11_101_000,
        0b_11_110_000,
        0b_11_111_000,
    ];

    private FullConditionCode _code;

    public override void Execute(Z80 cpu)
    {
        
        bool jump = IsJump(cpu,_code);
        if (jump)
        {
            cpu.Reg.PC = Pop16(cpu);
            Ticks = 13;
        }
        else Ticks = 7;
    }

    public override string ToString() => $"RET {_code}";
    public override Cmd Init(byte shift) => new RET_C { _code = (FullConditionCode)shift };
}

public enum FullConditionCode { NZ, Z, NC, C, PO, PE, P, M }
