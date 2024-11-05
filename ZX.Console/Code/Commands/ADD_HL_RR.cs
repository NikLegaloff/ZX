namespace ZX.Console.Code.Commands;

public class ADD_HL_RR : Cmd
{
    public ADD_HL_RR()
    {
        Ticks = 11;
    }

    private Reg16Code _code;
    public override byte[] Range => [0b00001001, 0b00011001, 0b00101001, 0b00111001];
    public override void Execute(Z80 cpu)
    {
        ushort val=0;
        switch (_code)
        {
            case Reg16Code.BC: val = cpu.Reg.BC ; break;
            case Reg16Code.DE: val = cpu.Reg.DE; break;
            case Reg16Code.HL: val = cpu.Reg.HL; break;
            case Reg16Code.SP: val = cpu.Reg.SP; break;
        }
        cpu.Reg.A.SetCarry(cpu.Reg.HL,val);
        cpu.Reg.A.Set53(val);
        cpu.Reg.A.SetHalfCary(cpu.Reg.L,(byte)val%256);
        cpu.Reg.HL += val;
    }

    public override string ToString() => $"ADD HL,{_code}";

    public override Cmd Init(byte shift) => new ADD_HL_RR { _code = (Reg16Code)shift };

}