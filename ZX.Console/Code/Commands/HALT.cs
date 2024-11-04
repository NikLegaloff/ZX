namespace ZX.Console.Code.Commands;

public class HALT : Cmd
{
    public override byte[] Range => [0b01_110_110];
    public override void Execute(Z80 cpu, byte cmd)
    {
        throw new HALTException();
    }
}
public class HALTException : Exception
{

}
