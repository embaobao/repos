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
    
    public partial class UserInfo
    {
        public int I_UID { get; set; }
        public string S_Desc { get; set; }
        public string S_Name { get; set; }
        public string S_Address { get; set; }
        public string S_EV_BD { get; set; }
        public string S_Set { get; set; }
        public Nullable<System.DateTime> TIME_CRATE { get; set; }
        public Nullable<System.DateTime> TIME_Used { get; set; }
        public Nullable<System.DateTime> TIME_LastUsed { get; set; }
        public Nullable<int> C_STA { get; set; }
        public Nullable<int> Gender { get; set; }
        public byte[] Icon { get; set; }
    }
}