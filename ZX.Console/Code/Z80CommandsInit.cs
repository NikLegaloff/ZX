using ZX.Console.Code.Commands;

namespace ZX.Console.Code;

public class Z80CommandsInit(Dictionary<byte, Cmd> commands)
{
    public void Init()
    {

        var all = new Cmd[]
        {
            new Nop(),
            new Ex_Af_Afp(),
            new DJNZ(),
            new JR_S(),
            new JR_CC_S(),
            new LD_RR_NN(),
            new ADD_HL_RR(),
            new Halt(),
            new LD_BCm_A(), new LD_A_BCm(),
            new LD_DEm_A(), new LD_A_DEm(),
            new LD_NN_HL(),new LD_HL_NN(),
            new LD_NNm_A(),new LD_A_NNm(),
            new INC_RR(),new DEC_RR(),
            new INC_R(),new DEC_R(),
            new LD_R_N(),new RLRCA(),
            new DAA(),new CPL(),new SCF(),new CCF(),
            new LD_D_R(),
            new ADD_A_R(), new ADC_A_R(),new SUB_A_R(), new SBC_A_R(),
            new AND_R(), new OR_R(),new XOR_R(), new CP_R(),
            new RET_C(), new POP_RS(), new RET(),new EXX(),new JP_HL(), new LD_SP_HL(),
            new JP_C_NN(),new JP_NN(), new OUT_N_A(), new IN_N_A(),
            new EX_SPm_HL(), new EX_DE_HL(), new EI(), new DI(),
            new CALL_C_NN(),new PUSH_RS(),new CALL_NN(),
            new ADD_A_N(),new ADC_A_N(),new SUB_A_N(),new SBC_A_N(),
            new AND_N(),new XOR_N(),new OR_N(),new CP_N(),new RST_P()
        };

        foreach(var cmd in all) Init(cmd);
    }

    private void Init(Cmd cmd)
    {
        byte i = 0;
        foreach (var b in cmd.Range) commands.Add(b, cmd.Init(i++));
    }

}