using System;
using System.Linq;
using System.Text;

namespace BKYL.BFES.Plugin.BGBFES7
{
    public class MES_UTIL_QUALITY_MES
    {
        
        /// <summary>
        /// Desc:记录编号 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public string ID {get;set;}

        /// <summary>
        /// Desc:样品编号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_ID {get;set;}

        /// <summary>
        /// Desc:试样编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_MTRL_CODE {get;set;}

        /// <summary>
        /// Desc:试样名称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_MTRL_NAME {get;set;}

        /// <summary>
        /// Desc:检验时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime TEST_TIME {get;set;}

        /// <summary>
        /// Desc:工序简称 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PROCESS_UNIT_ABBR {get;set;}

        /// <summary>
        /// Desc:取样地点编码 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_LOC_CODE {get;set;}

        /// <summary>
        /// Desc:取样地点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SAMPLE_LOC_NAME {get;set;}

        /// <summary>
        /// Desc:取样时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? SAMPLE_TIME {get;set;}

        /// <summary>
        /// Desc:P 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string P_VAL {get;set;}

        /// <summary>
        /// Desc:S 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string S_VAL {get;set;}

        /// <summary>
        /// Desc:Pb 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PB_VAL {get;set;}

        /// <summary>
        /// Desc:Sn 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SN_VAL {get;set;}

        /// <summary>
        /// Desc:Bi 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BI_VAL {get;set;}

        /// <summary>
        /// Desc:Sb 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SB_VAL {get;set;}

        /// <summary>
        /// Desc:As 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AS_VAL {get;set;}

        /// <summary>
        /// Desc:Al2O3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AL2O3_VAL {get;set;}

        /// <summary>
        /// Desc:TiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TIO2_VAL {get;set;}

        /// <summary>
        /// Desc:CaF2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CAF2_VAL {get;set;}

        /// <summary>
        /// Desc:MnO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MNO_VAL {get;set;}

        /// <summary>
        /// Desc:TFe 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TFE_VAL {get;set;}

        /// <summary>
        /// Desc:FeO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FEO_VAL {get;set;}

        /// <summary>
        /// Desc:SiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SIO2_VAL {get;set;}

        /// <summary>
        /// Desc:CaO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CAO_VAL {get;set;}

        /// <summary>
        /// Desc:MgO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MGO_VAL {get;set;}

        /// <summary>
        /// Desc:F 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string F_VAL {get;set;}

        /// <summary>
        /// Desc:K2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string K2O_VAL {get;set;}

        /// <summary>
        /// Desc:Na2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NA2O_VAL {get;set;}

        /// <summary>
        /// Desc:Ig 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string IG_VAL {get;set;}

        /// <summary>
        /// Desc:碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R_VAL {get;set;}

        /// <summary>
        /// Desc:TFe/FeO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TFE_FEO_VAL {get;set;}

        /// <summary>
        /// Desc:Mt 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MT_VAL {get;set;}

        /// <summary>
        /// Desc:粒度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LIDU_VAL {get;set;}

        /// <summary>
        /// Desc:转鼓指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZGZS_VAL {get;set;}

        /// <summary>
        /// Desc:抗磨指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string KMZS_VAL {get;set;}

        /// <summary>
        /// Desc:筛分指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SFZS_VAL {get;set;}

        /// <summary>
        /// Desc:RDI+6.3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RDI_VAL {get;set;}

        /// <summary>
        /// Desc:RI 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RI_VAL {get;set;}

        /// <summary>
        /// Desc:熔融滴落试验 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RRDLSY_VAL {get;set;}

        /// <summary>
        /// Desc:胶质价 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JZJ_VAL {get;set;}

        /// <summary>
        /// Desc:膨胀倍数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PZBS_VAL {get;set;}

        /// <summary>
        /// Desc:抗压强度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string KYQD_VAL {get;set;}

        /// <summary>
        /// Desc:ZnO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZNO_VAL {get;set;}

        /// <summary>
        /// Desc:-200目 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MU200_VAL {get;set;}

        /// <summary>
        /// Desc:水份 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string H2O_VAL {get;set;}

        /// <summary>
        /// Desc:Ad 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AD_VAL {get;set;}

        /// <summary>
        /// Desc:Aad 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AAD_VAL {get;set;}

        /// <summary>
        /// Desc:Vad 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string VAD_VAL {get;set;}

        /// <summary>
        /// Desc:St,d 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ST_D_VAL {get;set;}

        /// <summary>
        /// Desc:St,ad 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ST_AD_VAL {get;set;}

        /// <summary>
        /// Desc:FCd 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FCD_VAL {get;set;}

        /// <summary>
        /// Desc:Vdaf 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string VDAF_VAL {get;set;}

        /// <summary>
        /// Desc:Q 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Q_VAL {get;set;}

        /// <summary>
        /// Desc:G值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string G_VAL {get;set;}

        /// <summary>
        /// Desc:Y值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Y_VAL {get;set;}

        /// <summary>
        /// Desc:M10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M10_VAL {get;set;}

        /// <summary>
        /// Desc:M40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M40_VAL {get;set;}

        /// <summary>
        /// Desc:CRI 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CRI_VAL {get;set;}

        /// <summary>
        /// Desc:CSR 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CSR_VAL {get;set;}

        /// <summary>
        /// Desc:焦末含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JMHL_VAL {get;set;}

        /// <summary>
        /// Desc:灰份熔点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HFRD_VAL {get;set;}

        /// <summary>
        /// Desc:奥亚指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AYZS_VAL {get;set;}

        /// <summary>
        /// Desc:哈氏可磨指数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HSKMZS_VAL {get;set;}

        /// <summary>
        /// Desc:镜质组反射率Rmax 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JZZFSL_VAL {get;set;}

        /// <summary>
        /// Desc:筛分>80 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF80_VAL {get;set;}

        /// <summary>
        /// Desc:筛分60-80 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF6080_VAL {get;set;}

        /// <summary>
        /// Desc:筛分40-60 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF4060_VAL {get;set;}

        /// <summary>
        /// Desc:筛分25-40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF2540_VAL {get;set;}

        /// <summary>
        /// Desc:筛分<25 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF25_VAL {get;set;}

        /// <summary>
        /// Desc:PH 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PH_VAL {get;set;}

        /// <summary>
        /// Desc:Cl- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CL_VAL {get;set;}

        /// <summary>
        /// Desc:COD 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string COD_VAL {get;set;}

        /// <summary>
        /// Desc:浊度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZD_VAL {get;set;}

        /// <summary>
        /// Desc:总磷 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZL_VAL {get;set;}

        /// <summary>
        /// Desc:水硬度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SYD_VAL {get;set;}

        /// <summary>
        /// Desc:总硬度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZYD_VAL {get;set;}

        /// <summary>
        /// Desc:电导率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DDL_VAL {get;set;}

        /// <summary>
        /// Desc:Ca2+(CaCO3计) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CA2_VAL {get;set;}

        /// <summary>
        /// Desc:挥发氨 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HFA_VAL {get;set;}

        /// <summary>
        /// Desc:全氨 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QA_VAL {get;set;}

        /// <summary>
        /// Desc:氨氮1  
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AD1_VAL {get;set;}

        /// <summary>
        /// Desc:氨氮2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AD2_VAL {get;set;}

        /// <summary>
        /// Desc:酚 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FEN_VAL {get;set;}

        /// <summary>
        /// Desc:总铁 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZT_VAL {get;set;}

        /// <summary>
        /// Desc:油 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YOU_VAL {get;set;}

        /// <summary>
        /// Desc:浓缩倍数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NSBS_VAL {get;set;}

        /// <summary>
        /// Desc:硫代硫酸铵 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LDLSA_VAL {get;set;}

        /// <summary>
        /// Desc:硫氰酸铵 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LQSA_VAL {get;set;}

        /// <summary>
        /// Desc:对苯二酚 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DBEF_VAL {get;set;}

        /// <summary>
        /// Desc:悬浮硫 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string XFL_VAL {get;set;}

        /// <summary>
        /// Desc:PDS 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PDS_VAL {get;set;}

        /// <summary>
        /// Desc:悬浮物 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string XFW_VAL {get;set;}

        /// <summary>
        /// Desc:酚酞碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FTJD_VAL {get;set;}

        /// <summary>
        /// Desc:钠离子 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NALIZI_VAL {get;set;}

        /// <summary>
        /// Desc:铜离子 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TONGLIZI_VAL {get;set;}

        /// <summary>
        /// Desc:易释放氰化物 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YSFQHW_VAL {get;set;}

        /// <summary>
        /// Desc:总氰化物 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZQHW_VAL {get;set;}

        /// <summary>
        /// Desc:H2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string H2_VAL {get;set;}

        /// <summary>
        /// Desc:O2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string O2_VAL {get;set;}

        /// <summary>
        /// Desc:N2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string N2_VAL {get;set;}

        /// <summary>
        /// Desc:CH4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CH4_VAL {get;set;}

        /// <summary>
        /// Desc:CO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CO_VAL {get;set;}

        /// <summary>
        /// Desc:CO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CO2_VAL {get;set;}

        /// <summary>
        /// Desc:密度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MIDU_VAL {get;set;}

        /// <summary>
        /// Desc:萘含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NAI_VAL {get;set;}

        /// <summary>
        /// Desc:粘度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NIANDU_VAL {get;set;}

        /// <summary>
        /// Desc:浓度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NONGDU_VAL {get;set;}

        /// <summary>
        /// Desc:浓度（照相级） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NONGDU_ZXJ_VAL {get;set;}

        /// <summary>
        /// Desc:含量（工业级） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HANLIANG_GYJ_VAL {get;set;}

        /// <summary>
        /// Desc:活性 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HUOXING_VAL {get;set;}

        /// <summary>
        /// Desc:水不溶物 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SBRW_VAL {get;set;}

        /// <summary>
        /// Desc:甲苯不容物 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JBBRW_VAL {get;set;}

        /// <summary>
        /// Desc:磷酸根离子 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LSGLZ_VAL {get;set;}

        /// <summary>
        /// Desc:游离酸 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YLS_VAL {get;set;}

        /// <summary>
        /// Desc:苯 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BEN_VAL {get;set;}

        /// <summary>
        /// Desc:230℃前馏出量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL230_VAL {get;set;}

        /// <summary>
        /// Desc:300℃前馏出量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL300_VAL {get;set;}

        /// <summary>
        /// Desc:75℃前馏出量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL75_VAL {get;set;}

        /// <summary>
        /// Desc:180℃前馏出量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL180_VAL {get;set;}

        /// <summary>
        /// Desc:馏出96%（容）温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LC96WD_VAL {get;set;}

        /// <summary>
        /// Desc:外观 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string WG_VAL {get;set;}

        /// <summary>
        /// Desc:15℃结晶物 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JJW15_VAL {get;set;}

        /// <summary>
        /// Desc:初馏点 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CLD_VAL {get;set;}

        /// <summary>
        /// Desc:N 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string N_VAL {get;set;}

        /// <summary>
        /// Desc:游离酸（H2SO4）含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YLS_H2SO4_VAL {get;set;}

        /// <summary>
        /// Desc:Si 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SI_VAL {get;set;}

        /// <summary>
        /// Desc:Ca 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CA_VAL {get;set;}

        /// <summary>
        /// Desc:Mg 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MG_VAL {get;set;}

        /// <summary>
        /// Desc:Ba 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BA_VAL {get;set;}

        /// <summary>
        /// Desc:Al 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AL_VAL {get;set;}

        /// <summary>
        /// Desc:C 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_VAL {get;set;}

        /// <summary>
        /// Desc:Mn 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MN_VAL {get;set;}

        /// <summary>
        /// Desc:Alt 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALT_VAL {get;set;}

        /// <summary>
        /// Desc:Als 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALS_VAL {get;set;}

        /// <summary>
        /// Desc:Zn 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZN_VAL {get;set;}

        /// <summary>
        /// Desc:Cu 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CU_VAL {get;set;}

        /// <summary>
        /// Desc:Nb 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NB_VAL {get;set;}

        /// <summary>
        /// Desc:Ti 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TI_VAL {get;set;}

        /// <summary>
        /// Desc:Ni 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NI_VAL {get;set;}

        /// <summary>
        /// Desc:Cr 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CR_VAL {get;set;}

        /// <summary>
        /// Desc:Mo 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MO_VAL {get;set;}

        /// <summary>
        /// Desc:V 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string V_VAL {get;set;}

        /// <summary>
        /// Desc:B 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string B_VAL {get;set;}

        /// <summary>
        /// Desc:W 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string W_VAL {get;set;}

        /// <summary>
        /// Desc:Ce 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CE_VAL {get;set;}

        /// <summary>
        /// Desc:Zr 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZR_VAL {get;set;}

        /// <summary>
        /// Desc:La 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LA_VAL {get;set;}

        /// <summary>
        /// Desc:Nd 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ND_VAL {get;set;}

        /// <summary>
        /// Desc:P2O5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string P2O5_VAL {get;set;}

        /// <summary>
        /// Desc:H 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string H_VAL {get;set;}

        /// <summary>
        /// Desc:O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string O_VAL {get;set;}

        /// <summary>
        /// Desc:酚含量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FHL_VAL {get;set;}

        /// <summary>
        /// Desc:----------STAND  标准字符串： (上线，下限) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string P_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string S_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PB_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SN_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BI_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SB_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AL2O3_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TIO2_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CAF2_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MNO_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TFE_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FEO_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SIO2_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CAO_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MGO_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string F_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string K2O_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NA2O_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string IG_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TFE_FEO_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MT_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LIDU_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZGZS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string KMZS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SFZS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RDI_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RI_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RRDLSY_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JZJ_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PZBS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string KYQD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZNO_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MU200_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string H2O_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AAD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string VAD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ST_D_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ST_AD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FCD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string VDAF_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Q_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string G_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Y_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M10_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M40_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CRI_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CSR_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JMHL_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HFRD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AYZS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HSKMZS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JZZFSL_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF80_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF6080_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF4060_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF2540_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF25_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PH_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CL_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string COD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZL_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SYD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZYD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DDL_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CA2_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HFA_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QA_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AD1_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AD2_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FEN_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZT_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YOU_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NSBS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LDLSA_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LQSA_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DBEF_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string XFL_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PDS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string XFW_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FTJD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NALIZI_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TONGLIZI_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YSFQHW_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZQHW_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string H2_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string O2_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string N2_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CH4_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CO_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CO2_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MIDU_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NAI_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NIANDU_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NONGDU_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NONGDU_ZXJ_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HANLIANG_GYJ_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HUOXING_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SBRW_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JBBRW_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LSGLZ_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YLS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BEN_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL230_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL300_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL75_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL180_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LC96WD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string WG_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JJW15_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CLD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string N_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YLS_H2SO4_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SI_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CA_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MG_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BA_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AL_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MN_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALT_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZN_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CU_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NB_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TI_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NI_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CR_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MO_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string V_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string B_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string W_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CE_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZR_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LA_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ND_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string P2O5_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string H_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string O_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FHL_STAND {get;set;}

        /// <summary>
        /// Desc:----------FLAG  是否合格的标记，1：合格  0：不合格 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string P_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string S_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PB_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SN_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BI_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SB_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AL2O3_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TIO2_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CAF2_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MNO_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TFE_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FEO_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SIO2_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CAO_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MGO_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string F_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string K2O_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NA2O_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string IG_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TFE_FEO_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MT_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LIDU_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZGZS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string KMZS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SFZS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RDI_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RI_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RRDLSY_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JZJ_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PZBS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string KYQD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZNO_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MU200_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string H2O_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AAD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string VAD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ST_D_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ST_AD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FCD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string VDAF_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Q_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string G_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string Y_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M10_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string M40_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CRI_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CSR_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JMHL_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HFRD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AYZS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HSKMZS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JZZFSL_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF80_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF6080_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF4060_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF2540_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SF25_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PH_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CL_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string COD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZL_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SYD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZYD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DDL_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CA2_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HFA_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QA_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AD1_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AD2_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FEN_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZT_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YOU_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NSBS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LDLSA_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LQSA_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string DBEF_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string XFL_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PDS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string XFW_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FTJD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NALIZI_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TONGLIZI_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YSFQHW_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZQHW_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string H2_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string O2_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string N2_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CH4_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CO_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CO2_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MIDU_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NAI_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NIANDU_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NONGDU_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NONGDU_ZXJ_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HANLIANG_GYJ_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string HUOXING_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SBRW_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JBBRW_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LSGLZ_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YLS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BEN_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL230_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL300_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL75_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string QLCL180_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LC96WD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string WG_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string JJW15_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CLD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string N_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YLS_H2SO4_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SI_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CA_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MG_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string BA_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string AL_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string C_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MN_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALT_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZN_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CU_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NB_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TI_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string NI_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CR_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string MO_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string V_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string B_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string W_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CE_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ZR_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string LA_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ND_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string P2O5_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string H_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string O_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string FHL_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CREATOR_ID {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string CREATOR_NAME {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? CREATE_TIME {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string UPDATE_ID {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string UPDATE_NAME {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime? UPDATE_TIME {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string REMARK {get;set;}

        /// <summary>
        /// Desc:筛分>40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE1 {get;set;}

        /// <summary>
        /// Desc:筛分40-25 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE2 {get;set;}

        /// <summary>
        /// Desc:筛分25-16 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE3 {get;set;}

        /// <summary>
        /// Desc:筛分16-10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE4 {get;set;}

        /// <summary>
        /// Desc:筛分10-5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE5 {get;set;}

        /// <summary>
        /// Desc:总量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE6 {get;set;}

        /// <summary>
        /// Desc:平均粒径 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE7 {get;set;}

        /// <summary>
        /// Desc:配鼓40-25 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE8 {get;set;}

        /// <summary>
        /// Desc:配鼓25-16 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE9 {get;set;}

        /// <summary>
        /// Desc:配鼓16-10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE10 {get;set;}

        /// <summary>
        /// Desc:入鼓量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE11 {get;set;}

        /// <summary>
        /// Desc:出鼓量 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE12 {get;set;}

        /// <summary>
        /// Desc:>6.3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE13 {get;set;}

        /// <summary>
        /// Desc:筛分8-16 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE14 {get;set;}

        /// <summary>
        /// Desc:配鼓10-40 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE15 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE16 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE17 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE18 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE19 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE20 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE21 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE22 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE23 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE24 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE25 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE26 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE27 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE28 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE29 {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ATTRIBUTE30 {get;set;}

        /// <summary>
        /// Desc:综合是否合格判定   是否合格的标记，1：合格  0：不合格 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALL_FLAG {get;set;}

        /// <summary>
        /// Desc:所有合格项 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALL_PASS_ITEM {get;set;}

        /// <summary>
        /// Desc:所有不和各项 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string ALL_NOPASS_ITEM {get;set;}

        /// <summary>
        /// Desc:K2O+NA2O 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string K2O_NA2O_VAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string K2O_NA2O_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string K2O_NA2O_FLAG {get;set;}

        /// <summary>
        /// Desc:R2 二元碱度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R2_VAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R2_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string R2_FLAG {get;set;}

        /// <summary>
        /// Desc:SIO2+AL2O3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SIO2_AL2O3_VAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SIO2_AL2O3_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string SIO2_AL2O3_FLAG {get;set;}

        /// <summary>
        /// Desc:压差 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YACHA_VAL {get;set;}

        /// <summary>
        /// Desc:TD 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TD_VAL {get;set;}

        /// <summary>
        /// Desc:TS 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TS_VAL {get;set;}

        /// <summary>
        /// Desc:RDI+3.15 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RDI315_VAL {get;set;}

        /// <summary>
        /// Desc:"膨胀率" 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PZL_VAL {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YACHA_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TD_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TS_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RDI315_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PZL_STAND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string YACHA_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TD_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string TS_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string RDI315_FLAG {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public string PZL_FLAG {get;set;}

    }
}
