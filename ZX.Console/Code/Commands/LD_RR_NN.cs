namespace ZX.Console.Code.Commands;

public enum Reg16Code{BC,DE,HL,SP}

public class LD_RR_NN : Cmd
{
    public LD_RR_NN()
    {
        Ticks = 10;
    }
    private Reg16Code _code;

    public override byte[] Range => [0b00000001, 0b00010001, 0b00100001, 0b00110001 ];
    public override void Execute(Z80 cpu)
    {
        var val = ReadShort(cpu);
        switch (_code)
        {
            case Reg16Code.BC: cpu.Reg.BC=val; break;
            case Reg16Code.DE: cpu.Reg.DE=val; break;
            case Reg16Code.HL: cpu.Reg.HL=val; break;
            case Reg16Code.SP: cpu.Reg.SP=val; break;
        }
    }
    public override Cmd Init(byte shift) => new LD_RR_NN() { _code = (Reg16Code)shift };
    public override string ToString() => $"LD {_code}, NN";

}