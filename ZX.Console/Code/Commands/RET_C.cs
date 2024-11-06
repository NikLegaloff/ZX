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
        
        bool jump =    _code == FullConditionCode.Z  &&  cpu.Reg.F.Z
                    || _code == FullConditionCode.NZ && !cpu.Reg.F.Z
                    || _code == FullConditionCode.C  &&  cpu.Reg.F.C
                    || _code == FullConditionCode.NC && !cpu.Reg.F.C
                    || _code == FullConditionCode.PO && !cpu.Reg.F.PV
                    || _code == FullConditionCode.PE &&  cpu.Reg.F.PV 
                    || _code == FullConditionCode.P && !cpu.Reg.F.S 
                    || _code == FullConditionCode.M &&  cpu.Reg.F.S;
        if (jump)
        {
            cpu.Reg.PC = POP16(cpu);
            Ticks = 13;
        }
        else Ticks = 7;
    }


    public override string ToString() => $"RET {_code}";
    public override Cmd Init(byte shift) => new RET_C { _code = (FullConditionCode)shift };
}

public enum FullConditionCode { NZ, Z, NC, C, PO, PE, P, M }
