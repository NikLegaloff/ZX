namespace ZX.Console.Code.Commands;

public class JR_CC_S : Cmd
{
    public override byte[] Range => [0b001_00_000,0b00101000,0b001_10_000,0b001_11_000];

    public override void Execute(Z80 cpu, byte cmd)
    {
        var condition = (ShortConditionCode)GetBits45(cmd);
        bool jump = condition == ShortConditionCode.Z && cpu.Reg.A.Z || 
                    condition == ShortConditionCode.NZ && !cpu.Reg.A.Z || 
                    condition == ShortConditionCode.C && cpu.Reg.A.C || 
                    condition == ShortConditionCode.NC && !cpu.Reg.A.C;
        var shift = (sbyte)ReadByte(cpu);
        if (jump)
        {
            cpu.Reg.PC = (ushort)(cpu.Reg.PC + shift);
            Ticks = 13;
        }
        else Ticks = 7;
        
    }
}