using System;
using System.Linq;
using System.Text;

namespace BFES.Models
{
    public class STUFF_MATRIX
    {
        
        /// <summary>
        /// Desc:数据采集时间 
        /// Default:- 
        /// Nullable:False 
        /// </summary>
        public DateTime TIMESTAMP {get;set;}

        /// <summary>
        /// Desc:槽下拉料时间 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public DateTime A_BATCHHISDUMPTIME {get;set;}

        /// <summary>
        /// Desc:0表示新插入1表示已计算 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public bool A_FLAG {get;set;}

        /// <summary>
        /// Desc:0表示是矿石1表示是焦炭 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_TYPE {get;set;}

        /// <summary>
        /// Desc:料流阀开度 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_OPENDEGREE {get;set;}

        /// <summary>
        /// Desc:角度1 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE1 {get;set;}

        /// <summary>
        /// Desc:角度2 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE2 {get;set;}

        /// <summary>
        /// Desc:角度3 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE3 {get;set;}

        /// <summary>
        /// Desc:角度4 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE4 {get;set;}

        /// <summary>
        /// Desc:角度5 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE5 {get;set;}

        /// <summary>
        /// Desc:角度6 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE6 {get;set;}

        /// <summary>
        /// Desc:角度7 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE7 {get;set;}

        /// <summary>
        /// Desc:角度8 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE8 {get;set;}

        /// <summary>
        /// Desc:角度9 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE9 {get;set;}

        /// <summary>
        /// Desc:角度10 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE10 {get;set;}

        /// <summary>
        /// Desc:角度11 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_ANGLE11 {get;set;}

        /// <summary>
        /// Desc:角度1对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE1 {get;set;}

        /// <summary>
        /// Desc:角度2对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE2 {get;set;}

        /// <summary>
        /// Desc:角度3对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE3 {get;set;}

        /// <summary>
        /// Desc:角度4对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE4 {get;set;}

        /// <summary>
        /// Desc:角度5对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE5 {get;set;}

        /// <summary>
        /// Desc:角度6对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE6 {get;set;}

        /// <summary>
        /// Desc:角度7对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE7 {get;set;}

        /// <summary>
        /// Desc:角度8对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE8 {get;set;}

        /// <summary>
        /// Desc:角度9对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE9 {get;set;}

        /// <summary>
        /// Desc:角度10对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE10 {get;set;}

        /// <summary>
        /// Desc:角度11对应圈数 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CIRCLE11 {get;set;}

        /// <summary>
        /// Desc:设定料线平均值 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_LX {get;set;}

        /// <summary>
        /// Desc:料批号 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_CHARGE {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_KIND {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public Double A_WEIGHT {get;set;}

        /// <summary>
        /// Desc:- 
        /// Default:- 
        /// Nullable:True 
        /// </summary>
        public int A_ANGLECOUNT {get;set;}

    }
}
