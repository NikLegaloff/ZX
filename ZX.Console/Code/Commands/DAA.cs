using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ZX.Console.Code.Commands;

public class CCF : Cmd
{
    public override byte[] Range => [0b00_111_111];

    public override void Execute(Z80 cpu)
    {

        // f.h = f.c;
        // f.n = 0;
        // f.c = !f.c;
        cpu.Reg.F.H = cpu.Reg.F.C;
        cpu.Reg.F.N = false;
        cpu.Reg.F.C = !cpu.Reg.F.C;
        cpu.Reg.F.Set53(cpu.Reg.A);
    }

    public override Cmd Init(byte shift) => new CCF();
    public override string ToString() => "CCF";
}
public class SCF : Cmd
{
    public override byte[] Range => [0b00_110_111];

    public override void Execute(Z80 cpu)
    {
        cpu.Reg.F.C = true;
        cpu.Reg.F.H = false;
        cpu.Reg.F.N = false;
        cpu.Reg.F.Set53(cpu.Reg.A);
    }

    public override Cmd Init(byte shift) => new SCF();
    public override string ToString() => "SCF";
}

public class CPL : Cmd
{
    public override byte[] Range => [0b00_101_111];

    public override void Execute(Z80 cpu)
    {
        var a = cpu.Reg.A; 
        a ^= 0xff;
        cpu.Reg.A = a;
        cpu.Reg.F.H = true;
        cpu.Reg.F.N = true;
        cpu.Reg.F.Set53(a);
    }

    public override Cmd Init(byte shift) => new CPL();
    public override string ToString() => "CPL";
}

public class DAA : Cmd
{
    public override byte[] Range => [0b00_100_111];
    public override void Execute(Z80 cpu)
    {
        var a = cpu.Reg.A;
        var fh = cpu.Reg.F.H;
        var fc = cpu.Reg.F.C;
        var fn = cpu.Reg.F.N;

        var t = 0;
        if (fh || (a & 0x0F) > 0x09)
        {
            t++;
        }
        if (fc || a > 0x99)
        {
            t += 2;
            fc = true;
        }

        if (fn && !fh)
        {
            fh = false;
        }
        else if (fn && fh)
        {
            fh = (a & 0x0F) < 0x06;
        }
        else
        {
            fh = (a & 0x0F) > 0x09;
        }

        switch (t)
        {
            case 1: a = (byte)(a + (fn ? 0xFA : 0x06)) ; break;
            case 2: a = (byte)(a + (fn ? 0xA0 : 0x60)); break;
            case 3: a = (byte)(a + (fn ? 0x9A : 0x66)) ; break;
        }

        cpu.Reg.A = a;
        cpu.Reg.F.SetZS53(a);
        cpu.Reg.F.PV = GetParity(a);
        cpu.Reg.F.H = fh;
        cpu.Reg.F.C = fc;
    }
    public override Cmd Init(byte shift) => new DAA();
    public override string ToString() => "DAA";

}