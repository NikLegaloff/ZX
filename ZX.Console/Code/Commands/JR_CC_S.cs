namespace ZX.Console.Code.Commands;

public class JR_CC_S : Cmd
{
    public override byte[] Range => [0b001_00_000,0b00101000,0b001_10_000,0b001_11_000];

    private ShortConditionCode _code;

    public override void Execute(Z80 cpu)
    {
        
        bool jump = IsJump(cpu,_code);
        var shift = (sbyte)ReadByte(cpu);
        if (jump)
        {
            cpu.Reg.PC = (ushort)(cpu.Reg.PC + shift);
            Ticks = 13;
        }
        else Ticks = 7;
        
    }

    

    public override string ToString() => $"JR {_code}, s";
    public override Cmd Init(byte shift) => new JR_CC_S { _code = (ShortConditionCode)shift };
}

public enum ShortConditionCode { NZ, Z, NC, C }

