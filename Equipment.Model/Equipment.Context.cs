﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace Equipment.Model
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class equipmentEntities : DbContext
    {
        public equipmentEntities()
            : base("name=equipmentEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<td_Network> td_Network { get; set; }
        public virtual DbSet<td_User> td_User { get; set; }
        public virtual DbSet<ts_Dept> ts_Dept { get; set; }
        public virtual DbSet<td_Security> td_Security { get; set; }
        public virtual DbSet<V_Network_Dept> V_Network_Dept { get; set; }
        public virtual DbSet<V_GenuineSoftware_Dept> V_GenuineSoftware_Dept { get; set; }
        public virtual DbSet<V_Security_Dept> V_Security_Dept { get; set; }
        public virtual DbSet<V_ComputerRoom_Dept> V_ComputerRoom_Dept { get; set; }
        public virtual DbSet<td_ComputerRoom> td_ComputerRoom { get; set; }
        public virtual DbSet<td_GenuineSoftware> td_GenuineSoftware { get; set; }
        public virtual DbSet<td_ConstructionProject> td_ConstructionProject { get; set; }
        public virtual DbSet<td_ResourceCatalog> td_ResourceCatalog { get; set; }
        public virtual DbSet<td_Terminal> td_Terminal { get; set; }
        public virtual DbSet<V_ConstructionProject_Dept> V_ConstructionProject_Dept { get; set; }
        public virtual DbSet<V_ResourceCatalog_Dept> V_ResourceCatalog_Dept { get; set; }
        public virtual DbSet<V_Terminal_Dept> V_Terminal_Dept { get; set; }
        public virtual DbSet<td_Network_1> td_Network_1 { get; set; }
        public virtual DbSet<td_GenuineSoftware_1> td_GenuineSoftware_1 { get; set; }
        public virtual DbSet<td_Security_1> td_Security_1 { get; set; }
        public virtual DbSet<td_ComputerRoom_sbsl_1> td_ComputerRoom_sbsl_1 { get; set; }
        public virtual DbSet<td_Security_xxsl_1> td_Security_xxsl_1 { get; set; }
        public virtual DbSet<td_ComputerRoom_1> td_ComputerRoom_1 { get; set; }
        public virtual DbSet<td_Menu> td_Menu { get; set; }
        public virtual DbSet<td_Service_1> td_Service_1 { get; set; }
        public virtual DbSet<td_Service_2> td_Service_2 { get; set; }
    }
}
