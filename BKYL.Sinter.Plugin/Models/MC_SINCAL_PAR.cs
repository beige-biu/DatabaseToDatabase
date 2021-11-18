using System;
using System.Linq;
using System.Text;

namespace BKYL.Sinter.Plugin
{
    public class MC_SINCAL_PAR
    {
        
        /// <summary>
        /// Desc:时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:燃料调整仓索引（只能输入燃料仓仓号） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? PAR_FUEL_SILO_INDEX1 {get;set;}

        /// <summary>
        /// Desc:熔剂调整仓索引(只能输入石灰石、生石灰、白云石的仓仓号） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? PAR_LIMESTONE_SILO_INDEX1 {get;set;}

        /// <summary>
        /// Desc:计算出的燃料的配比最大值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BILL_FUEL_MAX {get;set;}

        /// <summary>
        /// Desc:计算出的燃料的配比最小值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BILL_FUEL_MIN {get;set;}

        /// <summary>
        /// Desc:计算出的灰石的配比最大值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BILL_LIMESTONE_MAX {get;set;}

        /// <summary>
        /// Desc:计算出的灰石的配比最小值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_BILL_LIMESTONE_MIN {get;set;}

        /// <summary>
        /// Desc:(目标碱度+R调整值)最大值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_R_A_MAX {get;set;}

        /// <summary>
        /// Desc:(目标碱度+R调整值)最小值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_R_A_MIN {get;set;}

        /// <summary>
        /// Desc:(目标含碳+碳调整值)最大值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_C_A_MAX {get;set;}

        /// <summary>
        /// Desc:(目标含碳+碳调整值)最小值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_C_A_MIN {get;set;}

        /// <summary>
        /// Desc:倒推取数时间间隔（R，FeO倒推） 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_T1 {get;set;}

        /// <summary>
        /// Desc:料位上上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_UP {get;set;}

        /// <summary>
        /// Desc:料位上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP {get;set;}

        /// <summary>
        /// Desc:正常料位上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_NORMAL_W_UP {get;set;}

        /// <summary>
        /// Desc:正常料位下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_NORMAL_W_LOW {get;set;}

        /// <summary>
        /// Desc:料位下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW {get;set;}

        /// <summary>
        /// Desc:料位下下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_LOW {get;set;}

        /// <summary>
        /// Desc:仓位过高时追加增加的分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_C_AD {get;set;}

        /// <summary>
        /// Desc:仓位过低时追加减小的分仓系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_C_DE {get;set;}

        /// <summary>
        /// Desc:1#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_1 {get;set;}

        /// <summary>
        /// Desc:1#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_1 {get;set;}

        /// <summary>
        /// Desc:2#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_2 {get;set;}

        /// <summary>
        /// Desc:2#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_2 {get;set;}

        /// <summary>
        /// Desc:3#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_3 {get;set;}

        /// <summary>
        /// Desc:3#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_3 {get;set;}

        /// <summary>
        /// Desc:4#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_4 {get;set;}

        /// <summary>
        /// Desc:4#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_4 {get;set;}

        /// <summary>
        /// Desc:5#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_5 {get;set;}

        /// <summary>
        /// Desc:5#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_5 {get;set;}

        /// <summary>
        /// Desc:6#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_6 {get;set;}

        /// <summary>
        /// Desc:6#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_6 {get;set;}

        /// <summary>
        /// Desc:7#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_7 {get;set;}

        /// <summary>
        /// Desc:7#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_7 {get;set;}

        /// <summary>
        /// Desc:8#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_8 {get;set;}

        /// <summary>
        /// Desc:8#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_8 {get;set;}

        /// <summary>
        /// Desc:9#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_9 {get;set;}

        /// <summary>
        /// Desc:9#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_9 {get;set;}

        /// <summary>
        /// Desc:10#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_10 {get;set;}

        /// <summary>
        /// Desc:10#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_10 {get;set;}

        /// <summary>
        /// Desc:11#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_11 {get;set;}

        /// <summary>
        /// Desc:11#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_11 {get;set;}

        /// <summary>
        /// Desc:12#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_12 {get;set;}

        /// <summary>
        /// Desc:12#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_12 {get;set;}

        /// <summary>
        /// Desc:13#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_13 {get;set;}

        /// <summary>
        /// Desc:13#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_13 {get;set;}

        /// <summary>
        /// Desc:14#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_14 {get;set;}

        /// <summary>
        /// Desc:14#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_14 {get;set;}

        /// <summary>
        /// Desc:15#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_15 {get;set;}

        /// <summary>
        /// Desc:15#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_15 {get;set;}

        /// <summary>
        /// Desc:16#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_16 {get;set;}

        /// <summary>
        /// Desc:16#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_16 {get;set;}

        /// <summary>
        /// Desc:17#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_17 {get;set;}

        /// <summary>
        /// Desc:17#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_17 {get;set;}

        /// <summary>
        /// Desc:18#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_18 {get;set;}

        /// <summary>
        /// Desc:18#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_18 {get;set;}

        /// <summary>
        /// Desc:19#料仓下料口小时下料上限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_UP_19 {get;set;}

        /// <summary>
        /// Desc:19#料仓下料口小时下料下限值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_LOW_19 {get;set;}

        /// <summary>
        /// Desc:混匀矿仓下料故障判断标准 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_SL_FAULT_JUDGE {get;set;}

        /// <summary>
        /// Desc:"仓位超限延时计算周期5.2.1.1" 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? PAR_SL_OR_DELAY_T {get;set;}

        /// <summary>
        /// Desc:"故障仓延时计算周期5.2.1.2" 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? PAR_SL_FAULT_DELAY_T {get;set;}

        /// <summary>
        /// Desc:"混匀矿仓下料自动调整计算周期、在用混匀矿仓仓号变化延时计算周期、人工修改参数延时计算周期、人工修改分仓系数延时计算周期" 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int? PAR_SL_CAL_T {get;set;}

        /// <summary>
        /// Desc:8号仓料位上上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_UP_8 {get;set;}

        /// <summary>
        /// Desc:8号仓料位上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_8 {get;set;}

        /// <summary>
        /// Desc:8号仓料位下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_8 {get;set;}

        /// <summary>
        /// Desc:8号仓料位下下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_LOW_8 {get;set;}

        /// <summary>
        /// Desc:9号仓料位上上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_UP_9 {get;set;}

        /// <summary>
        /// Desc:9号仓料位上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_9 {get;set;}

        /// <summary>
        /// Desc:9号仓料位下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_9 {get;set;}

        /// <summary>
        /// Desc:9号仓料位下下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_LOW_9 {get;set;}

        /// <summary>
        /// Desc:10号仓料位上上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_UP_10 {get;set;}

        /// <summary>
        /// Desc:10号仓料位上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_10 {get;set;}

        /// <summary>
        /// Desc:10号仓料位下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_10 {get;set;}

        /// <summary>
        /// Desc:10号仓料位下下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_LOW_10 {get;set;}

        /// <summary>
        /// Desc:11号仓料位上上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_UP_11 {get;set;}

        /// <summary>
        /// Desc:11号仓料位上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_11 {get;set;}

        /// <summary>
        /// Desc:11号仓料位下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_11 {get;set;}

        /// <summary>
        /// Desc:11号仓料位下下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_LOW_11 {get;set;}

        /// <summary>
        /// Desc:12号仓料位上上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_UP_12 {get;set;}

        /// <summary>
        /// Desc:12号仓料位上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_12 {get;set;}

        /// <summary>
        /// Desc:12号仓料位下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_12 {get;set;}

        /// <summary>
        /// Desc:12号仓料位下下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_LOW_12 {get;set;}

        /// <summary>
        /// Desc:13号仓料位上上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_UP_13 {get;set;}

        /// <summary>
        /// Desc:13号仓料位上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_13 {get;set;}

        /// <summary>
        /// Desc:13号仓料位下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_13 {get;set;}

        /// <summary>
        /// Desc:13号仓料位下下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_LOW_13 {get;set;}

        /// <summary>
        /// Desc:14号仓料位上上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_UP_14 {get;set;}

        /// <summary>
        /// Desc:14号仓料位上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_14 {get;set;}

        /// <summary>
        /// Desc:14号仓料位下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_14 {get;set;}

        /// <summary>
        /// Desc:14号仓料位下下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_LOW_14 {get;set;}

        /// <summary>
        /// Desc:15号仓料位上上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_UP_15 {get;set;}

        /// <summary>
        /// Desc:15号仓料位上限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_UP_15 {get;set;}

        /// <summary>
        /// Desc:15号仓料位下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_15 {get;set;}

        /// <summary>
        /// Desc:15号仓料位下下限 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_W_LOW_LOW_15 {get;set;}

        /// <summary>
        /// Desc:1号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_1 {get;set;}

        /// <summary>
        /// Desc:2号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_2 {get;set;}

        /// <summary>
        /// Desc:3号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_3 {get;set;}

        /// <summary>
        /// Desc:4号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_4 {get;set;}

        /// <summary>
        /// Desc:5号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_5 {get;set;}

        /// <summary>
        /// Desc:6号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_6 {get;set;}

        /// <summary>
        /// Desc:7号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_7 {get;set;}

        /// <summary>
        /// Desc:8号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_8 {get;set;}

        /// <summary>
        /// Desc:9号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_9 {get;set;}

        /// <summary>
        /// Desc:10号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_10 {get;set;}

        /// <summary>
        /// Desc:11号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_11 {get;set;}

        /// <summary>
        /// Desc:12号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_12 {get;set;}

        /// <summary>
        /// Desc:13号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_13 {get;set;}

        /// <summary>
        /// Desc:14号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_14 {get;set;}

        /// <summary>
        /// Desc:15号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_15 {get;set;}

        /// <summary>
        /// Desc:16号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_16 {get;set;}

        /// <summary>
        /// Desc:17号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_17 {get;set;}

        /// <summary>
        /// Desc:18号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_18 {get;set;}

        /// <summary>
        /// Desc:19号下料口下料偏差报警值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_B_DEV_19 {get;set;}

        /// <summary>
        /// Desc:烧结矿目标TFe 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_AIM_TFE {get;set;}

        /// <summary>
        /// Desc:烧结矿目标SiO2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_AIM_SIO2 {get;set;}

        /// <summary>
        /// Desc:烧结矿目标CaO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_AIM_CAO {get;set;}

        /// <summary>
        /// Desc:烧结矿目标MgO 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_AIM_MGO {get;set;}

        /// <summary>
        /// Desc:产量修正系数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double? PAR_K {get;set;}

    }
}
