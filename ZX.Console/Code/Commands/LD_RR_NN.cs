namespace ZX.Console.Code.Commands;

public enum Reg16Code{BC,DE,HL,SP}

public class ADD_HL_RR : Cmd
{
    public ADD_HL_RR()
    {
        Ticks = 11;
    }

    public override byte[] Range => [0b00001001, 0b00011001, 0b00101001, 0b00111001];
    public override void Execute(Z80 cpu, byte cmd)
    {
        var reg = (Reg16Code)GetBits56(cmd);
        ushort val=0;
        switch (reg)
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
}

public class LD_RR_NN : Cmd
{
    public LD_RR_NN()
    {
        Ticks = 10;
    }
    public override byte[] Range => [0b00000001, 0b00010001, 0b00100001, 0b00110001 ];
    public override void Execute(Z80 cpu, byte cmd)
    {
        var val = ReadShort(cpu);
        var bits56 = GetBits56(cmd);
        var reg = (Reg16Code)bits56;
        switch (reg)
        {
            case Reg16Code.BC: cpu.Reg.BC=val; break;
            case Reg16Code.DE: cpu.Reg.DE=val; break;
            case Reg16Code.HL: cpu.Reg.HL=val; break;
            case Reg16Code.SP: cpu.Reg.SP=val; break;
        }
    }
}