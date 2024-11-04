namespace ZX.Console.Code.Commands;

public class DJNZ : Cmd
{
    public override byte[] Range => [0b00010000];
    public override void Execute(Z80 cpu, byte cmd)
    {
        cpu.Reg.B--;
        var shift = (sbyte)ReadByte(cpu);
        if (cpu.Reg.B == 0)
        {
            Ticks = 7;
        }
        else
        {
            Ticks = 13;
            cpu.Reg.PC = (ushort)(cpu.Reg.PC + shift);
        }
    }
}