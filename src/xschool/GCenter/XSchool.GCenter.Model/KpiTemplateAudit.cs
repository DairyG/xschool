using XSchool.Core;

namespace XSchool.GCenter.Model
{
    /// <summary>
    /// 考核对象的审核人
    /// </summary>
    public class KpiTemplateAudit : IModel<int>
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 考核模板Id
        /// </summary>
        public int KpiTemplateId { get; set; }

        /// <summary>
        /// 考核对象类型，1-部门负责人，2-人员
        /// </summary>
        public KpiObjectType ObjectType { get; set; }

        /// <summary>
        /// 初审类型，1-部门负责人，2-上级部门负责人，3-其他
        /// </summary>
        public KpiAuditObjectType OneObjectType { get; set; }
        /// <summary>
        /// 初审Id
        /// </summary>
        public int OneAuditId { get; set; }

        /// <summary>
        /// 终审类型，2-上级部门负责人，3-其他
        /// </summary>
        public KpiAuditObjectType TwoObjectType { get; set; }
        /// <summary>
        /// 终审Id
        /// </summary>
        public int TwoAuditId { get; set; }
    }
}

