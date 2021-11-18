using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BKYL.QT.Plugin.Models
{
    public class R_ROASTER_PLC_01M
    {

        /// <summary>
        /// Desc:插入时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP { get; set; }

        /// <summary>
        /// Desc:入机小时生球量(焙烧机给料量) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_FQ_W { get; set; }

        /// <summary>
        /// Desc:03风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_03 { get; set; }

        /// <summary>
        /// Desc:04风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_04 { get; set; }

        /// <summary>
        /// Desc:05风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_05 { get; set; }

        /// <summary>
        /// Desc:06风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_06 { get; set; }

        /// <summary>
        /// Desc:07风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_07 { get; set; }

        /// <summary>
        /// Desc:08风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_08 { get; set; }

        /// <summary>
        /// Desc:09风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_09 { get; set; }

        /// <summary>
        /// Desc:10风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_10 { get; set; }

        /// <summary>
        /// Desc:11风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_11 { get; set; }

        /// <summary>
        /// Desc:12风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_12 { get; set; }

        /// <summary>
        /// Desc:13风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_13 { get; set; }

        /// <summary>
        /// Desc:14风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_14 { get; set; }

        /// <summary>
        /// Desc:15风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_15 { get; set; }

        /// <summary>
        /// Desc:16风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_16 { get; set; }

        /// <summary>
        /// Desc:17风箱温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_TE_17 { get; set; }

        /// <summary>
        /// Desc:二冷段烟罩温度(左148) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PH_L_TE { get; set; }

        /// <summary>
        /// Desc:二冷段烟罩温度(右152) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PH_R_TE { get; set; }

        /// <summary>
        /// Desc:焙烧段烟罩压力3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PH_TP { get; set; }

        /// <summary>
        /// Desc:鼓干段烟罩温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_FT_BT_TE { get; set; }

        /// <summary>
        /// Desc:铺底料料位(铺底料重) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_UC_BL_W { get; set; }

        /// <summary>
        /// Desc:卸料仓(卸料仓料量) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PH_BL_W { get; set; }

        /// <summary>
        /// Desc:带式机机速(焙烧机机速) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PH_BL_EP { get; set; }

        /// <summary>
        /// Desc:台车料厚A 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_T_H_A { get; set; }

        /// <summary>
        /// Desc:台车料厚B 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_T_H_B { get; set; }

        /// <summary>
        /// Desc:台车料厚C 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_T_H_C { get; set; }

        /// <summary>
        /// Desc:台车料厚D 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_T_H_D { get; set; }

        /// <summary>
        /// Desc:台车料厚E 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_T_H_E { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机温度(鼓干鼓风机出口温度) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_FT_DD_TE { get; set; }

        /// <summary>
        /// Desc:回热风机温度(回热风机出口温度) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_FT_RH_TE { get; set; }

        /// <summary>
        /// Desc:回热风机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_FT_RH_ST { get; set; }

        /// <summary>
        /// Desc:鼓干鼓风机压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_FT_DD_TP { get; set; }

        /// <summary>
        /// Desc:主引风机温度  
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_FT_ML_TE { get; set; }

        /// <summary>
        /// Desc:主引风机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_FT_ML_ST { get; set; }

        /// <summary>
        /// Desc:冷却风机转速 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_FT_CL_ST { get; set; }

        /// <summary>
        /// Desc:抽干段压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PH_PI_TP { get; set; }

        /// <summary>
        /// Desc:1#烧嘴预热控制段1温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER01_TE { get; set; }

        /// <summary>
        /// Desc:2#烧嘴预热控制段1温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER02_TE { get; set; }

        /// <summary>
        /// Desc:3#烧嘴预热控制段2温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER03_TE { get; set; }

        /// <summary>
        /// Desc:4#烧嘴预热控制段2温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER04_TE { get; set; }

        /// <summary>
        /// Desc:5#烧嘴预热控制段3温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER05_TE { get; set; }

        /// <summary>
        /// Desc:6#烧嘴预热控制段3温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER06_TE { get; set; }

        /// <summary>
        /// Desc:7#烧嘴预热控制段4温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER07_TE { get; set; }

        /// <summary>
        /// Desc:8#烧嘴预热控制段4温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER08_TE { get; set; }

        /// <summary>
        /// Desc:9#烧嘴预热控制段5温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER09_TE { get; set; }

        /// <summary>
        /// Desc:10#烧嘴预热控制段6温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER10_TE { get; set; }

        /// <summary>
        /// Desc:11#烧嘴预热控制段6温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER11_TE { get; set; }

        /// <summary>
        /// Desc:12#烧嘴预热控制段6温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER12_TE { get; set; }

        /// <summary>
        /// Desc:13#烧嘴预热控制段6温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER13_TE { get; set; }

        /// <summary>
        /// Desc:14#烧嘴预热控制段6温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER14_TE { get; set; }

        /// <summary>
        /// Desc:15#烧嘴预热控制段6温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER15_TE { get; set; }

        /// <summary>
        /// Desc:16#烧嘴预热控制段7温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER16_TE { get; set; }

        /// <summary>
        /// Desc:17#烧嘴预热控制段7温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER17_TE { get; set; }

        /// <summary>
        /// Desc:18#烧嘴预热控制段7温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER18_TE { get; set; }

        /// <summary>
        /// Desc:19#烧嘴预热控制段7温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER19_TE { get; set; }

        /// <summary>
        /// Desc:20#烧嘴预热控制段7温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER20_TE { get; set; }

        /// <summary>
        /// Desc:21#烧嘴预热控制段7温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER21_TE { get; set; }

        /// <summary>
        /// Desc:22#烧嘴预热控制段7温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER22_TE { get; set; }

        /// <summary>
        /// Desc:23#烧嘴预热控制段7温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER23_TE { get; set; }

        /// <summary>
        /// Desc:24#烧嘴预热控制段7温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER24_TE { get; set; }

        /// <summary>
        /// Desc:25#烧嘴预热控制段8温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER25_TE { get; set; }

        /// <summary>
        /// Desc:26#烧嘴预热控制段8温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER26_TE { get; set; }

        /// <summary>
        /// Desc:27#烧嘴预热控制段8温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER27_TE { get; set; }

        /// <summary>
        /// Desc:28#烧嘴预热控制段8温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER28_TE { get; set; }

        /// <summary>
        /// Desc:29#烧嘴预热控制段8温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER29_TE { get; set; }

        /// <summary>
        /// Desc:30#烧嘴预热控制段8温度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BURNER30_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩6区温度(风箱回热预热段温度6-28) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT601_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩6区温度(风箱回热预热段温度6-29) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT602_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩5区温度(风箱回热预热段温度5-29) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT501_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩5区温度(风箱回热预热段温度5-30) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT502_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩4区温度(风箱回热预热段温度4-31) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT401_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩4区温度(风箱回热预热段温度4-32) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT402_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩3区温度(风箱回热预热段温度3-33) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT301_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩3区温度(风箱回热预热段温度3-34) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT302_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩2区温度(风箱回热预热段温度2-35) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT201_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩2区温度(风箱回热预热段温度2-36) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT202_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩1区温度(风箱回热预热段温度1-37) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT101_TE { get; set; }

        /// <summary>
        /// Desc:预热段烟罩1区温度(风箱回热预热段温度1-38) 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_PREHEAT102_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度7 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER701_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度7 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER702_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度6 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER601_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度6 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER602_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER501_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER502_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER401_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER402_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER301_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER302_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER201_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER202_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER101_TE { get; set; }

        /// <summary>
        /// Desc:焙烧机预热段烟罩温度1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ROASTER102_TE { get; set; }

        /// <summary>
        /// Desc:瞬时湿返率 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_WR { get; set; }

        /// <summary>
        /// Desc:1风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS01_TP { get; set; }

        /// <summary>
        /// Desc:2风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS02_TP { get; set; }

        /// <summary>
        /// Desc:3风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS03_TP { get; set; }

        /// <summary>
        /// Desc:4风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS04_TP { get; set; }

        /// <summary>
        /// Desc:5风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS05_TP { get; set; }

        /// <summary>
        /// Desc:6风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS06_TP { get; set; }

        /// <summary>
        /// Desc:7风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS07_TP { get; set; }

        /// <summary>
        /// Desc:8风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS08_TP { get; set; }

        /// <summary>
        /// Desc:9风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS09_TP { get; set; }

        /// <summary>
        /// Desc:10风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS10_TP { get; set; }

        /// <summary>
        /// Desc:11风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS11_TP { get; set; }

        /// <summary>
        /// Desc:12风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS12_TP { get; set; }

        /// <summary>
        /// Desc:13风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS13_TP { get; set; }

        /// <summary>
        /// Desc:14风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS14_TP { get; set; }

        /// <summary>
        /// Desc:15风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS15_TP { get; set; }

        /// <summary>
        /// Desc:16风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS16_TP { get; set; }

        /// <summary>
        /// Desc:17风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS17_TP { get; set; }

        /// <summary>
        /// Desc:18风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS18_TP { get; set; }

        /// <summary>
        /// Desc:19风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS19_TP { get; set; }

        /// <summary>
        /// Desc:20风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS20_TP { get; set; }

        /// <summary>
        /// Desc:21风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS21_TP { get; set; }

        /// <summary>
        /// Desc:22风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS22_TP { get; set; }

        /// <summary>
        /// Desc:23风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS23_TP { get; set; }

        /// <summary>
        /// Desc:24风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS24_TP { get; set; }

        /// <summary>
        /// Desc:25风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS25_TP { get; set; }

        /// <summary>
        /// Desc:26风箱压力 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_BELLOWS26_TP { get; set; }

    }
}
