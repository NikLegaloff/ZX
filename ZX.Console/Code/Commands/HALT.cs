namespace ZX.Console.Code.Commands;

public class Halt : Cmd
{
    public override byte[] Range => [0b01_110_110];
    public override void Execute(Z80 cpu)
    {
        throw new HaltException();
    }
    public override Cmd Init(byte shift) => new Halt();
    public override string ToString() => "HALT";
}

public class HaltException : Exception;
