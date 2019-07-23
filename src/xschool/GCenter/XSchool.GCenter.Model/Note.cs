using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using XSchool.Core;

namespace XSchool.GCenter.Model
{
    # region 通知公告
    /// <summary>
    /// 通知公告
    /// </summary>
    public class Note : IModel<int>
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 公告标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 发布人Id
        /// </summary>
        public int PublisherId { get; set; }
        /// <summary>
        /// 发布日期
        /// </summary>
        public DateTime CreateDate { get; set; }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 公告附件Url地址
        /// </summary>
        public string EnclosureUrl { get; set; }
        /// <summary>
        /// 是否强制阅读
        /// </summary>
        public int IsNeedRead { get; set; }
        /// <summary>
        /// 阅读次数
        /// </summary>
        public int ReadCount { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string PublisherName { get; set; }
        /// <summary>
        /// 发布内容
        /// </summary>
        public string DepartmentName { get; set; }
        /// <summary>
        /// 树结构typeId
        /// </summary>
        public string SelType { get; set; }
    }
    /// <summary>
    /// 通知公告查询条件
    /// </summary>
    public class NoteSearch
    {
        /// <summary>
        /// 公告标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 是否已读
        /// </summary>
        public int IsRead { get; set; }
    }
    #endregion

    #region 阅读范围
    /// <summary>
    /// 阅读范围
    /// </summary>
    public class NoteReadRange : IModel<int>
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int IsRead { get; set; }
        public DateTime ReadDate { get; set; }
        public OrgType TypeId { get; set; }
        public  int UserId { get; set; }
        public  string UserName { get; set; }
        public int DptId { get; set; }
        public string DptName { get; set; }
        public int CompanyId { get; set; }
        public string CompanyName { get; set; }
        public int PositionId { get; set; }
        public string PositionName { get; set; }
    }
    #endregion

    #region 组织架构Model
    /// <summary>
    /// 树结构人员信息
    /// </summary>
    public class User
    {
        public int id { get; set; }
        public string name { get; set; }
        public int dpt_id { get; set; }
        public string dpt_name { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
    }
    /// <summary>
    /// 树结构部门
    /// </summary>
    public class Dep
    {
        public int id { get; set; }
        public string name { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
    }
    /// <summary>
    /// 树结构公司
    /// </summary>
    public class Com
    {
        public int id { get; set; }
        public string name { get; set; }
    }
    /// <summary>
    /// 树结构职位
    /// </summary>
    public class Position
    {
        public int id { get; set; }
        public string name { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
    }
    /// <summary>
    /// 树结构部门职位
    /// </summary>
    public class dpt_position
    {
        public int id { get; set; }
        public string name { get; set; }
        public int job_id { get; set; }
        public int dpt_id { get; set; }
        public string dpt_name { get; set; }
        public int company_id { get; set; }
        public string company_name { get; set; }
    }
    public enum OrgType
    {
        [Description("人员")]
        User=1,
        [Description("部门")]
        Dep = 2,
        [Description("公司")]
        Com = 3,
        [Description("职位")]
        Position = 4,
        [Description("岗位")]
        DepPosition = 5
    }
    public class ChooseUser
    {
        public string sel_type { get; set; }
        public List<User> user { get; set; }
        public List<Dep> department { get; set; }
        public List<Com> company { get; set; }
        public List<Position> position { get; set; }
        public List<dpt_position> dpt_position { get; set; }
    }
    #endregion

    #region 通知公告详情
    public class DetailNote
    {
        public Note noteDetail { get; set; }
        public ChooseUser chooseUser { get; set; }
    }
    #endregion

    #region 通知公告查询Model
    public class SelectNoteModel
    {
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string DptName { get; set; }
    }
    #endregion

    #region 通知公告阅读记录
    /// <summary>
    /// 通知公告阅读记录
    /// </summary>
    public class NoteRead : IModel<int>
    {
        public int Id { get; set; }
        public int NoteId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string CompanyName { get; set; }
        public string DptName { get; set; }
        public DateTime ReadDate { get; set; }
    }
    #endregion
}
