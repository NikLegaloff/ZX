namespace ZX.Console.Code;

public static class StrHelper
{
    public static string ToBits(this byte b) => Convert.ToString(b, 2).PadLeft(8, '0');
}
public class Registers : RegistersSet
{
    public RegistersSet Alt = new ();

    public byte I = 0;
    public byte R = 0;
    public ushort IX = 0;
    public ushort IY = 0;
    public ushort SP = 65535;
    public ushort PC = 0;

    public override string ToString()
    {
        return $"{base.ToString()}, {nameof(I)}: {I},{nameof(IX)}: {IX}, {nameof(IY)}: {IY}, {nameof(SP)}: {SP}, {nameof(PC)}: {PC}";
    }
}

public class Flags
{
    public byte Value=0;
    private bool _iff1=false;
    private bool _iff2 = false;

    public bool S
    {
        get => Value >> 7 > 0;
        set { if (value) Value |= 0b10000000; else Value &= 0b01111111; }
    }
    public bool Z
    {
        get => (Value >> 6) % 2 == 1;
        set { if (value) Value |= 0b01000000; else Value &= 0b10111111; }
    }
    public bool F5
    {
        get => (Value >> 5) % 2 == 1;
        set { if (value) Value |= 0b00100000; else Value &= 0b11011111; }
    }
    public bool H
    {
        get => (Value >> 4) % 2 == 1;
        set { if (value) Value |= 0b00010000; else Value &= 0b11101111; }
    }
    public bool F3
    {
        get => (Value >> 3) % 2 == 1;
        set { if (value) Value |= 0b00001000; else Value &= 0b11110111; }
    }
    public bool PV
    {
        get => (Value >> 2) % 2 == 1;
        set { if (value) Value |= 0b00000100; else Value &= 0b11111011; }
    }
    public bool N
    {
        get => (Value >> 1) % 2 == 1;
        set { if (value) Value |= 0b00000010; else Value &= 0b11111101; }
    }
    public bool C
    {
        get => Value % 2 == 1;
        set { if (value) Value |= 0b00000001; else Value &= 0b11111110; }
    }
    public void Set53(ushort val)
    {
        F5 = (val & 0b00100000) > 0;
        F3 = (val & 0b00001000) > 0;
    }
    public void SetCarry(ushort a, ushort b) => C = a + b > 65535;
    public void SetHalfCary(byte a, int b) => H = a % 16 + b % 16 >= 16;

    public void SetZS53(byte val, bool? negative=null)
    {
        Z = val == 0;
        S = (val & 0b10000000) > 0;
        Set53(val);
        if (negative != null) N = negative.Value;
    }
}

public class RegistersSet
{
    public byte A;
    public Flags F=new ();
    public ushort AF
    {
        get => (ushort)(A * 256 + F.Value);
        set
        {
            A= (byte)(value / 256);
            F.Value = (byte)(value % 256);
        }
    }

    public byte B;
    public byte C;
    public ushort BC
    {
        get => (ushort)(B * 256 + C);
        set
        {
            B = (byte)(value / 256);
            C = (byte)(value % 256);
        }
    }

    public byte D;
    public byte E;
    public ushort DE
    {
        get => (ushort)(D * 256 + E);
        set
        {
            D = (byte)(value / 256);
            E = (byte)(value % 256);
        }
    }

    public byte H;
    public byte L;
    public ushort HL
    {
        get => (ushort)(H * 256 + L);
        set
        {
            H = (byte)(value / 256);
            L = (byte)(value % 256);
        }

    }

    public override string ToString()
    {
        return $"{nameof(A)}: {A} {A.ToBits()}, {nameof(F)}: {F.Value.ToBits()}, {nameof(B)}: {B}, {nameof(C)}: {C}, {nameof(D)}: {D}, {nameof(E)}: {E}, {nameof(H)}: {H}, {nameof(L)}: {L}";
    }
}