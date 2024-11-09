namespace ZX.Console.Code.Commands;

public class ADD_A_N : ADD_SUB_BASE_NN
{
    public override byte[] Range => [0b11_000_110]; public override string ToString() => "ADD A, N"; public override Cmd Init(byte shift) => new ADD_A_N ();
    protected override bool IsCarry => false; protected override bool IsAdd => true;
}

public class ADC_A_N : ADD_SUB_BASE_NN
{
    public override byte[] Range => [0b11_001_110]; public override string ToString() => "ADC A, N"; public override Cmd Init(byte shift) => new ADC_A_N();
    protected override bool IsCarry => true; protected override bool IsAdd => true;
}

public class SUB_A_N : ADD_SUB_BASE_NN
{
    public override byte[] Range => [0b11_010_110]; public override string ToString() => "SUB A, N"; public override Cmd Init(byte shift) => new SUB_A_N();
    protected override bool IsCarry => false; protected override bool IsAdd => false;
}

public class SBC_A_N : ADD_SUB_BASE_NN
{
    public override byte[] Range => [0b11_011_110]; public override string ToString() => "SBC A, N"; public override Cmd Init(byte shift) => new SBC_A_N();
    protected override bool IsCarry => true; protected override bool IsAdd => false;
}



