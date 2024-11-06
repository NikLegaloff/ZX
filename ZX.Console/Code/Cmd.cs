using System.Numerics;
using ZX.Console.Code.Commands;

namespace ZX.Console.Code;

public abstract class Cmd
{
    public byte Ticks = 4;
    public abstract byte[] Range { get; }
    public abstract void Execute(Z80 cpu);

    protected byte ReadByte(Z80 cpu) => cpu.Memory[cpu.Reg.PC++];
    protected ushort ReadShort(Z80 cpu) => (ushort)(ReadByte(cpu) +  ReadByte(cpu)*256);
    protected byte GetBits45(byte b) => (byte)((b >> 3) % 4);
    protected byte GetBits56(byte b) => (byte)((b >> 4) % 4);

    public abstract Cmd Init(byte shift);

    protected bool GetParity(byte value)
    {
        return BitOperations.PopCount((uint)value) % 2 == 0;
    }

    protected bool get_byte_sum_overflow(byte op1, byte op2, byte sum)
    {
        if (((op1 ^ op2) & 0x80) > 0)
        {
            return false;
        }
        else
        {
            if (((op1 ^ sum) & 0x80) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    protected bool get_byte_diff_overflow(byte op1, byte op2, byte diff)
    {
        if (((op1 ^ op2) & 0x80) > 0)
        {
            if (((op1 ^ diff) & 0x80) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    protected ushort POP16(Z80 cpu)
    {
        return (ushort)(256 * cpu.Memory[cpu.Reg.SP--] * 256 + cpu.Memory[cpu.Reg.SP--]);
    }

    protected ushort Get(Z80 cpu, Reg16Code code)
    {
        switch (code)
        {
            case Reg16Code.BC: return cpu.Reg.BC;
            case Reg16Code.DE: return cpu.Reg.DE;
            case Reg16Code.HL: return cpu.Reg.HL;
            case Reg16Code.SP: return cpu.Reg.SP;
        }
        throw new Exception("UnknownCode" + code);
    }
    protected byte Get(Z80 cpu, Reg8Code code)
    {
        switch (code)
        {
            case Reg8Code.B: return cpu.Reg.B;
            case Reg8Code.C: return cpu.Reg.C;
            case Reg8Code.D: return cpu.Reg.D;
            case Reg8Code.E: return cpu.Reg.E;
            case Reg8Code.H: return cpu.Reg.H;
            case Reg8Code.L: return cpu.Reg.L;
            case Reg8Code.HLm: return cpu.Memory[cpu.Reg.HL];
            case Reg8Code.A: return cpu.Reg.A;
        }
        throw new Exception("UnknownCode" + code);
    }

    protected void Set(Z80 cpu, Reg16Code code, ushort val)
    {
        switch (code)
        {
            case Reg16Code.BC: cpu.Reg.BC = val; break;
            case Reg16Code.DE: cpu.Reg.DE = val; break;
            case Reg16Code.HL: cpu.Reg.HL = val; break;
            case Reg16Code.SP: cpu.Reg.SP = val; break;
            default: throw new Exception("UnknownCode" + code);
        }
    }
    protected void Set(Z80 cpu, Reg8Code code, byte val)
    {
        switch (code)
        {
            case Reg8Code.B:  cpu.Reg.B=val; break;
            case Reg8Code.C:  cpu.Reg.C=val; break;
            case Reg8Code.D:  cpu.Reg.D=val; break;
            case Reg8Code.E:  cpu.Reg.E=val; break;
            case Reg8Code.H:  cpu.Reg.H=val; break;
            case Reg8Code.L:  cpu.Reg.L=val; break;
            case Reg8Code.HLm:cpu.Memory[cpu.Reg.HL]=val; break;
            case Reg8Code.A:  cpu.Reg.A=val; break;
            default: throw new Exception("UnknownCode" + code);
        }
    }

}