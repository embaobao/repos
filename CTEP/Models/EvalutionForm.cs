//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace CTEP.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class EvalutionForm
    {
        public int ID { get; set; }
        public string S_Desc { get; set; }
        public string S_Name { get; set; }
        public Nullable<int> I_CREATE_UID { get; set; }
        public Nullable<int> I_Week { get; set; }
        public Nullable<int> I_Type { get; set; }
        public Nullable<int> I_BD_ID { get; set; }
        public Nullable<int> I_Course_ID { get; set; }
        public string S_CREATE_UName { get; set; }
        public Nullable<System.DateTime> TIME_Star_EV { get; set; }
        public Nullable<System.DateTime> TIME_END_EV { get; set; }
        public Nullable<double> F_Score { get; set; }
        public Nullable<int> C_STA { get; set; }
        public byte[] Icon { get; set; }
    }
}
