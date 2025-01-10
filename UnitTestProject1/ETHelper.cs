using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using BusinessWork.Works;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NFine.Code;
using UnitTestProject1.ResultModel;

namespace UnitTestProject1
{
    [TestClass]
    public class ETHelper
    {
        [TestMethod]
        public void TestMethod1()
        {

            Dictionary<int, int> dic = null;

            foreach(var item in dic)
            {

            }

            int maxCount = 10;
            string msg = "sdfsadfasdfsadfsdfdfsdfsdf";
            int totalNum = msg.Length % maxCount > 0 ? msg.Length / maxCount + 1 : msg.Length / maxCount;


            var result = SplitLength(msg, maxCount);
            for (int i = 0; i < totalNum; i++)
            {
                //var str = msg.Split(100).Substring(i * maxCount, (i + 1) * maxCount);
            }
            try
            {
                ETHandleJob job = new ETHandleJob();
                job.Login("", "");
                job.GetData();
            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// 读取excel信息
        /// </summary>
        [TestMethod]
        public void ReadExcelTemp()
        {
            string dicMeta = "津贴管理,TenantBase.AllowanceInfo;自动考核设置,TenantBase.AutoEvaluateSeting;自动考核设置详情,TenantBase.AutoEvaluateSetingDetail;自动编码,TenantBase.AutoGenCode;自动编码规则,TenantBase.AutomaticCodingRule;自动编码规则明细,TenantBase.AutomaticCodingRuleDetail;表彰与奖励,TenantBase.Awards;待办事项,TenantBase.BacklogTemplate;工牌模板,TenantBase.BadgeTemplate;银行账号（废弃）,TenantBase.BankAccount;批量调编申请,TenantBase.BatchChangeEstablishment;批量调动,TenantBase.BatchTransfer;黑名单,TenantBase.BlackList;通用规则设置,TenantBase.BusinessRules;证书执照,TenantBase.Certificate;公共数据,TenantBase.CommonData;通用数据源设置,TenantBase.CommonDataSource;通用字段,TenantBase.CommonFieldsEntity;公共存储对象,TenantBase.CommonStorage;公共存储对象（员工级）,TenantBase.CommonUserStorage;联系方式（废弃）,TenantBase.ContactInformation;合同变动记录,TenantBase.ContractChangeRecord;法人公司,TenantBase.Corporation;离职退休交接,TenantBase.DimissionHandover;离职去向,TenantBase.DimissionWhereabouts;DTC监控数据,TenantBase.DTCData;职责转交,TenantBase.DutyTransfer;审批流程转交（作废）,TenantBase.DutyTransferApproval;通用职责转交,TenantBase.DutyTransferCommon;下属员工转交,TenantBase.DutyTransferEmployee;组织角色转交,TenantBase.DutyTransferOrgRole;入离职事项转交,TenantBase.DutyTransferTodoRule;教育经历,TenantBase.Education;紧急联系人[废弃],TenantBase.EmergencyContact;变动原因,TenantBase.EmpChangeReason;变动类型,TenantBase.EmpChangeType;员工详情页信息集及其摘要,TenantBase.EmployeeDetailPageInfoSet;员工详情页页签信息,TenantBase.EmployeeDetailPageTabInfo;员工详情页配置,TenantBase.EmployeeDetailsPageConfig;员工引导记录,TenantBase.EmployeeGuideRecord;员工信息,TenantBase.EmployeeInformation;字段继承设置,TenantBase.EmployeeRecordFieldSetting;任职变动记录,TenantBase.EmploymentChange;合同协议,TenantBase.EmploymentContract;用工形式,TenantBase.EmploymentForm;任职记录,TenantBase.EmploymentRecord;人员来源,TenantBase.EmploymentSource;人员类别,TenantBase.EmploymentType;员工准备事项,TenantBase.EmpTodos;异动类型,TenantBase.EmpTransitionType;入职办理记录,TenantBase.EntryAffairsRecord;材料设置,TenantBase.EntryMaterial;材料管理,TenantBase.EntryMaterialRec;入职(暂时不用，留作以后再用，别乱动),TenantBase.EntryRecord;入职欢迎信,TenantBase.EntryWelcomeLetter;组织编制审批,TenantBase.EstablishmentApproval;编制方案,TenantBase.EstestablishmentScheme;占编人员范围,TenantBase.EstestablishmentScope;编制设置,TenantBase.EstestablishmentSetting;考核结果,TenantBase.EstimationResult;执行结果记录,TenantBase.ExecutionResultRecord;家庭成员,TenantBase.Family;文件,TenantBase.Files;身份证读卡器,TenantBase.IDCardReader;信息变更记录,TenantBase.InformationChangeRecords;数据初始化记录,TenantBase.InitializeDataRecord;在职信息采集,TenantBase.InServiceCollectInfo;职等,TenantBase.JobGrade;工作履历,TenantBase.jobhistory;职层,TenantBase.JobLayer;职级,TenantBase.JobLevel;职级类别,TenantBase.JobLevelType;职务,TenantBase.JobPost;职务职级图谱,TenantBase.JobPostAndJobLevelGraph;职务序列,TenantBase.JobSequence;新员工融入办理记录,TenantBase.JoinInAffairsRecord;新员工融入关联任务,TenantBase.JoinInAssociatedTask;新员工融入方案,TenantBase.JoinInScheme;新员工融入阶段设置,TenantBase.JoinInStages;新员工融入任务子集,TenantBase.JoinInTask;新员工融入任务设置,TenantBase.JoinInTaskTemplate;新员工融入任务规则（废弃）,TenantBase.JoinInTaskTemplateRule;语言能力,TenantBase.Languageability;尚未启动的对象,TenantBase.Location;日志,TenantBase.LogMessage;字段映射,TenantBase.MetaMapping;新员工自我介绍,TenantBase.NewEmployee;Offer,TenantBase.Offer;Offer批量审批,TenantBase.OfferBatchApproval;入职材料生成规则,TenantBase.OnboardingMaterialRules;试岗期考核,TenantBase.OnTrialEvaluation;试岗期信息,TenantBase.OnTrialInformation;排序属性(暂不用),TenantBase.OrderProperty;排序规则,TenantBase.OrderRule;排序规则项目,TenantBase.OrderRuleItem;组织架构调整,TenantBase.OrgAdjustment;组织单元,TenantBase.Organization;组织调整申请,TenantBase.OrganizationApproval;组织调整申请明细,TenantBase.OrganizationApprovalDetail;组织变更记录,TenantBase.OrganizationChange;编制,TenantBase.OrganizationEstablishment;组织层级,TenantBase.OrganizationLevel;组织类型,TenantBase.OrganizationType;兼职审批,TenantBase.ParttimeJobApproval;绩效改进结果（废弃）,TenantBase.PerformanceImprovedResult;绩效改进结果,TenantBase.PerformanceImproveResult;图片库,TenantBase.PictureLibrary;职位,TenantBase.Position;职位调整申请,TenantBase.PositionApproval;职位调整申请明细,TenantBase.PositionApprovalDetail;转正（暂时不用，留作以后再用，别乱动）,TenantBase.PositiveRecord;预置子集1,TenantBase.PresetSubset1;预置子集10,TenantBase.PresetSubset10;预置子集11,TenantBase.PresetSubset11;预置子集12,TenantBase.PresetSubset12;预置子集13,TenantBase.PresetSubset13;预置子集14,TenantBase.PresetSubset14;预置子集15,TenantBase.PresetSubset15;预置子集16,TenantBase.PresetSubset16;预置子集17,TenantBase.PresetSubset17;预置子集18,TenantBase.PresetSubset18;预置子集19,TenantBase.PresetSubset19;预置子集2,TenantBase.PresetSubset2;预置子集20,TenantBase.PresetSubset20;预置子集21,TenantBase.PresetSubset21;预置子集22,TenantBase.PresetSubset22;预置子集23,TenantBase.PresetSubset23;预置子集24,TenantBase.PresetSubset24;预置子集25,TenantBase.PresetSubset25;预置子集26,TenantBase.PresetSubset26;预置子集27,TenantBase.PresetSubset27;预置子集28,TenantBase.PresetSubset28;预置子集29,TenantBase.PresetSubset29;预置子集3,TenantBase.PresetSubset3;预置子集30,TenantBase.PresetSubset30;预置子集4,TenantBase.PresetSubset4;预置子集5,TenantBase.PresetSubset5;预置子集6,TenantBase.PresetSubset6;预置子集7,TenantBase.PresetSubset7;预置子集8,TenantBase.PresetSubset8;预置子集9,TenantBase.PresetSubset9;试用期考核,TenantBase.ProbationEvaluation;专业条线,TenantBase.ProfessionalLine;职业技能信息,TenantBase.ProfessionalSkillsInfo;专业技术职务,TenantBase.ProfessionalTechnicalPostInfo;项目经历,TenantBase.ProjectExperience;惩罚情况,TenantBase.Punish;执业（职业）资格名称设置,TenantBase.QualificationSettings;极速入职模板设置[废弃],TenantBase.QuickEntryTemplate;拒绝Offer/取消入职原因,TenantBase.ReasonsOfRefuseOffer;合同续签规则详情,TenantBase.RenewContractRuleDetail;资源集合,TenantBase.ResourceSet;退休返聘记录,TenantBase.RetireAfterReEmployed;任职类型,TenantBase.ServiceType;专业技能,TenantBase.Skill;外部ID映射,TenantBase.SourceIdMapping;步骤化调整,TenantBase.StepAdjustment;细分编制审批,TenantBase.SubdivideEstablishmentApproval;开关参数,TenantBase.Switch;同步人员结果记录,TenantBase.SyncEmployeeRecord;同步职务类别结果记录,TenantBase.SyncJobCategoryRecord;同步职务结果记录,TenantBase.SyncJobPostRecord;同步组织结果记录,TenantBase.SyncOrganizationRecord;下载日志,TenantBase.SysAttachExportMetaObj;消息模板,TenantBase.SYSMessageTemplate;导出实体,TenantBase.SystemMetaObjExportMetaObject;导入历史,TenantBase.SystemMetaObjImportMetaObject;作业监控,TenantBase.SystemMetaObjMonitorableMetaObject;操作日志,TenantBase.SystemMetaObjOperateLog;暂存数据（平台）,TenantBase.SystemMetaObjTemporaryData;高层次人才信息,TenantBase.TalentsInfo;专业技术职务名称设置,TenantBase.TechnicalPostQualifications;暂存数据(废弃),TenantBase.temporaryData;暂存数据,TenantBase.TemporaryObject;事项办理规则,TenantBase.TodoInchangerCalRule;培训经历,TenantBase.Training;转移明细（用于分批转移）,TenantBase.TransferDetailsForStopOrg;调动交接（废弃）,TenantBase.TransferHandover;调动交接,TenantBase.TransferHandoverV2;获取其他应用数据通用对象,TenantBase.TransferStation;试工管理,TenantBase.TrialWork;执业（职业）资格信息,TenantBase.VocationalQualificationInfo;Word模板,TenantBase.WordTemplate;自定义对象1,TenantBase.zidingyiduixiang1;自定义对象10,TenantBase.zidingyiduixiang10;自定义对象2,TenantBase.zidingyiduixiang2;自定义对象3,TenantBase.zidingyiduixiang3;自定义对象4,TenantBase.zidingyiduixiang4;自定义对象5,TenantBase.zidingyiduixiang5;自定义对象6,TenantBase.zidingyiduixiang6;自定义对象7,TenantBase.zidingyiduixiang7;自定义对象8,TenantBase.zidingyiduixiang8;自定义对象9,TenantBase.zidingyiduixiang9";
            var dicCountAll = dicMeta.Split(';');
            Dictionary<string, string> dicCountInfo = new Dictionary<string, string>();
            foreach (var item in dicCountAll)
            {

                var itemInner = item.Split(',');
                if (dicCountInfo.ContainsKey(itemInner[0]) == false)
                {
                    dicCountInfo.Add(itemInner[0], itemInner[1]);
                }
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("TenantBase.EmploymentRecord", "StdBusinessUnitID,OIdOrganization,OIdDepartment,OIdJobPosition,OIdJobPost,OIdJobLevel,OidJobGrade,Place,EntryDate,StartDate,StopDate,JobNumber,EmployeeStatus,POIdEmpAdmin,POIdEmpReserve2,WorkYearBefore,WorkYearCompanyBefore,WorkYearTotal,WorkYearCompanyTotal,ProbationStartDate,ProbationStopDate,ProbationActualStopDate,Probation,RegularizationDate,ProbationResult,EmploymentType,EmploymentSource,EmploymentForm,IsCharge,BusinessTypeOID,ChangeTypeOID,ChangeReason,ChangeDesc,ChangedStatus,TransitionTypeOID,EmployType,ServiceType,ServiceStatus,OIdJobSequence,OIdProfessionalLine,Whereabouts,LastWorkDate,POIdEmpReserve3,POIdEmpReserve4,POIdEmpReserve5,TraineeStartDate,Remarks,EntryDateYear,EntryStatus,EntryDateMonth,EntryDateDay,TransferSyncToJobHistory,AddOrNotBlackList,BlackStaffDesc,BlackListAddReason,ReasonForCancellation,Dimension1,Dimension2,Dimension3,Dimension4,Dimension5,EntryType,ExpectedLastWorkDate");
            dic.Add("TenantBase.EmploymentContract", "ContactLength,EffectiveDate,TerminateDate,SigningDate,ActualTerminateDate,FirstParty,ContractType,ContractDueTimeType,Code,status,FirstPartyCode,SignedNum,BusinessType,OriginalContractID");
            dic.Add("TenantBase.EmpTodos", "Order,Type,Content,InCharge,Telephone,Mobile,Email,HasFinish,Remark,EmailSelf,JobNumber,TodoCategory,BeiSenUserID,tutorNew");
            dic.Add("TenantBase.EntryMaterialRec", "12");
            dic.Add("TenantBase.EstimationResult", "Year,Period,ActivityName,Score,Grade,DeptName,RateStatement,DevelopmentPlan,ScoreGrade,ValuesScore,ValuesRuleId,AbilityScore,AbilityRuleId,StartDate,StopDate,CoefficientOfPerformance,PerformanceCloudPeriod");
            dic.Add("TenantBase.ProbationEvaluation", "StdOrganization,EvaluateDate,Summary,EvaluationName,Assessment,PositiveOpinion,Description,RegularizationDate,Remarks,EvaluationType,FinalScore,ValuesScore,AbilityScore,TotalScoreRuleId,ValuesRuleId,AbilityRuleId,OStatus,Period");
            dic.Add("TenantBase.jobhistory", "Company,Address,Hangye,DimissionDate,Begindate,LastJog,FirstJob,Department,JobLevel,UnderlingNum,ReportTo,Achievement,CompanySize,Introduction,JobType,WorkKind,Salary,LeaveReason,ProverName,ProverJobPost,ProverRelation,CompanyType,Responsibility,StartDate,StopDate,ProverContactInfo,WhetherTheWorkExperienceOfTheUnit,POIdOrgAdminNameTreePath,ServiceType");
            dic.Add("TenantBase.Education", "School,BeginDate,EnddatE,Major,Specializedfield,EducationLevel,Degree,MainCourse,Comments,UserID,Location,SchoolSystme,MajorDescription,Performance,ClassRanking,MajorRanking,Prover,IsFirstMajor,DegreeCountry,GrantingInstitution,IsHighestEducation,IsHighestDegree,GraduationType,StudyMode,TrainingMode,SchoolCode,IsFirstEducationLevel");
            dic.Add("TenantBase.ProjectExperience", "Name,StartDate,EndDate,Description,UserID,Responsibility,JobPost,PeopleNumber,Software,Hardware,DevTool,Achievement,JobPosition");
            dic.Add("TenantBase.Training", "Evaluation,Course,TrainType,TimeToTle,Result,Discription,TrainingAgency,Certificate,StartDate,StopDate,TrainingCategory,GetCredit,CompletionStatus,PassStatus,IsHaveMedal,TrainingActivityNum,MentorUserId");
            dic.Add("TenantBase.Family", "Name,Relation,Gender,Mobile,Telephone,Email,Birthday,Company,JobPost,PoliticalStatus,RelationType");
            dic.Add("TenantBase.Certificate", "IssauthorIty,Gettime,Validuntil,Discription,Name,EffectiveDate,CertificateNo");
            dic.Add("TenantBase.Languageability", "Languageskill,Language,Level,IsNative,WritingAbility,ReadingAbility,SpeakingAbility");
            dic.Add("TenantBase.Skill", "Name,Level,Category,UseLength");
            dic.Add("TenantBase.Awards", "Date,Discribtion,Type,AwardName,Level");
            dic.Add("TenantBase.VocationalQualificationInfo", "QualificationNameID,QualificationTypeID,QualificationLevelID,TermTypeID,IssueUnit,DateOfAcquisition,ValidUntil,ApplicationTypeID,Professional,AccessWayID,SupremeGrade,CertificateNumber,Accessory,SourceLicensesID,CertificateNumberUpper");
            dic.Add("TenantBase.ProfessionalTechnicalPostInfo", "TechnicalPostQualificationsID,TechnicalLevelID,AccessWayID,QualificationEvaluationUnit,AssessmentDate,AppointStartDate,AppointEndDate,AppointCompany,AppointTechnicalLevelID,ProfessionalSpeciality,IsTopLevel,AppointTechnicalPostQualificationsID,Remarks");
            dic.Add("TenantBase.ProfessionalSkillsInfo", "OccupationCategoryID,OccupationalSkillLevelID,AcquireTime,CertificateNumber,CertifyingAuthority");
            dic.Add("TenantBase.Punish", "PunishDate,PunishDescribe");
            dic.Add("TenantBase.AllowanceInfo", "DateOfIssue,ValidUntil,AllowanceLevelID,AllowanceTypeID");
            dic.Add("TenantBase.TalentsInfo", "DateOfAcquisition,GrantNo,TalentsLevelID,TalentsCategoryID,HonoraryCategoryID,ApprovalAuthority,IsBringInTalents");
            dic.Add("TenantBase.SubsetCommon", "DefaultField,StartDate,StopDate");

            var readData = OfficeHelper.ReadAllSheetToDataTable("D:\\TempExcel\\配置标题摘要时可选字段.xlsx", false, firstRowIndex: 3);

            Dictionary<string, string> fieldCountDic = new Dictionary<string, string>();


            //result = XmlHelper.GetXmlModel<FormView>(result, xmlStr);
            StringBuilder sb = new StringBuilder();
            foreach (var data in readData)
            {
                int i = 0;
                if (dicCountInfo.ContainsKey(data.Key) == false)
                {

                }
                List<string> dicINfo = dic[dicCountInfo[data.Key]].Split(',').ToList();
                List<string> codes = new List<string>();
                foreach (var datarow in data.Value.Rows)
                {
                    if (data.Value != null && data.Value.Rows != null && data.Value.Rows[i] != null)
                    {
                        try
                        {

                            string codeValue = data.Value.Rows[i]["编码"]?.ToString();
                            if (string.IsNullOrEmpty(codeValue) == false && dicINfo.Contains(codeValue) == false)
                                codes.Add(data.Value.Rows[i]["编码"].ToString());
                        }
                        catch (Exception ex)
                        {

                        }
                    }

                    i++;
                }
                var codeStr = string.Join(",", codes.Distinct());

                sb.Append($"<item Key=\"{dicCountInfo[data.Key]}\" Name=\"{data.Key}\" BlackFields=\"{codeStr}\"/>");
                //if (fieldCountDic.ContainsKey(data.Key) == false)
                //    fieldCountDic.Add(data.Key, codeStr);
            }


        }
        /// <summary>
        /// 读取excel信息
        /// </summary>
        [TestMethod]
        public void ReadExcelToDataTableTestMethod1()
        {
            var readData = OfficeHelper.ReadExcelToDataTable("D:\\AllDatas.xlsx");

            Dictionary<int, int> fieldCountDic = new Dictionary<int, int>();

            //result = XmlHelper.GetXmlModel<FormView>(result, xmlStr);
            int i = 0;
            foreach (var datarow in readData.Rows)
            {
                if (i > 0)
                {
                    try
                    {

                        FormView result = new FormView();
                        string xmlStr = readData.Rows[i]["FormXml"].ToString();
                        result = XmlHelper.DESerializer<FormView>(xmlStr);
                        if (result != null && result.FormParts != null && result.FormParts[1] != null)
                        {
                            int fiecount = result.FormParts[1].Fields.Count();
                            if (fieldCountDic.ContainsKey(fiecount))
                            {
                                fieldCountDic[fiecount] = fieldCountDic[fiecount] + 1;
                            }
                            else
                            {
                                fieldCountDic.Add(fiecount, 1);
                            }
                        }

                    }
                    catch (Exception)
                    {

                    }
                }

                i++;
            }
        }

        /// <summary>
        /// 按长度分割字符串，汉字按一个字符算
        /// </summary>
        /// <param name="SourceString"></param>
        /// <param name="Length"></param>
        /// <returns></returns>
        public static ArrayList SplitLength(string SourceString, int Length)
        {
            //List<string> DestString = new List<string>();
            ArrayList list = new ArrayList();
            for (int i = 0; i < SourceString.Trim().Length; i += Length)
            {
                if ((SourceString.Trim().Length - i) >= Length)
                    list.Add(SourceString.Trim().Substring(i, Length));
                else
                    list.Add(SourceString.Trim().Substring(i, SourceString.Trim().Length - i));
            }
            return list;
        }
        [TestMethod]
        public void TestMethod12222()
        {
            AllData all = new AllData();
            //面试测评、考勤信息、薪酬信息
            Dictionary<string, bool> MoreMessage = new Dictionary<string, bool>();
            MoreMessage.Add("Assess", true);
            MoreMessage.Add("Recruitment", false);
            MoreMessage.Add("Compensation", true);
            MoreMessage.Add("Attendance", true);
            all.Authoritys = MoreMessage;
            all.Identity = "HR";

            var tfd22 = all.ToJson();
            var tfd = MoreMessage.ToJson();
        }
        private int DateDiff(DateTime dateStart, DateTime dateEnd)

        {

            DateTime start = Convert.ToDateTime(dateStart.ToShortDateString());

            DateTime end = Convert.ToDateTime(dateEnd.ToShortDateString());

            TimeSpan sp = end.Subtract(start);

            return sp.Days;

        }
        #region Public Import Method
        /// <summary>
        /// 获取组织人事超过5000的terms
        /// </summary>
        [TestMethod]
        public void TestHttpPost()
        {



            //var outData = (float)((Math.Floor((days / 365) * Math.Pow(10, precision) + workYearCompanyBefore * 10* precision)) / Math.Pow(10, precision));



            //try
            //{
            //    var searchDate = DateTime.Now;
            //    string path = "D:\\myTools\\KibanaTerms\\" + searchDate.ToString("yyyyMMdd") + ".text";
            //    FileHelper.ExistsFileV2(path);
            //    StringBuilder sb = new StringBuilder();
            //    sb.AppendLine($"数据所在日期：{searchDate}；所有的trace===>");
            //    sb.AppendLine($"执行结束时间====》{DateTime.Now.ToString("yyyyMMddTHH:mm:ss")}");
            //    FileHelper.AppendText(path, sb.ToString());
            //}
            //catch (Exception ex)
            //{


            //} 
            try
            {
                List<DateTime> times = new List<DateTime>() { new DateTime(2023, 5, 20), new DateTime(2023, 5, 21) };
                times.ForEach(time =>
                {
                    DateTime dateTime = time;
                    int totalCount = 0;
                    var allTrace = GetTraceList(dateTime, out totalCount);
                    var allPath = GetViewPathList(allTrace.ToArray(), dateTime);
                    Thread.Sleep(20000);
                });

            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// 获取组织人事(GetOrganizationInfoByOIds) 接口参数
        /// </summary>
        [TestMethod]
        public void TestAPiColumsPost()
        {



            try
            {
                StringBuilder sb = new StringBuilder();
                AppendSummary(sb, "请填写方案名称");
                sb.AppendLine($"public static readonly string JoinInSchemeNameCantNull = \"JoinInSchemeNameCantNull\";");
                AppendSummary(sb, "请填写优先级");
                sb.AppendLine($"public static readonly string JoinInSchemePriorityCantNull = \"JoinInSchemePriorityCantNull\";");
                AppendSummary(sb, "请选择适用组织范围");
                sb.AppendLine($"public static readonly string JoinInSchemeOrgCantNull = \"JoinInSchemeOrgCantNull\";");
                AppendSummary(sb, "名称长度不能超过50");
                sb.AppendLine($"public static readonly string JoinInSchemeNameCantThan50 = \"JoinInSchemeNameCantThan50\";");
                AppendSummary(sb, "说明长度不能超过500");
                sb.AppendLine($"public static readonly string JoinInSchemeExplainCantThan50 = \"JoinInSchemeExplainCantThan50\";");
                AppendSummary(sb, "请填写到期提醒天数为0至60的整数");

                sb.AppendLine($"public static readonly string JoinInSchemeRemindDayRange = \"JoinInSchemeRemindDayRange\";");
                AppendSummary(sb, "请填写提醒天数");

                sb.AppendLine($"public static readonly string JoinInSchemeRemindDayCantNull = \"JoinInSchemeRemindDayCantNull\";");
                AppendSummary(sb, "不能与其他方案的名称重复");

                sb.AppendLine($"public static readonly string JoinInSchemeNameCantRepeat = \"JoinInSchemeNameCantRepeat\";");
                AppendSummary(sb, "不能与其他方案的优先级重复");

                sb.AppendLine($"public static readonly string JoinInSchemePriorityCantRepeat = \"JoinInSchemePriorityCantRepeat\";");
                AppendSummary(sb, "不能与其他方案的范围重复");

                sb.AppendLine($"public static readonly string JoinInSchemeRangeCantRepeat = \"JoinInSchemeRangeCantRepeat\";");
                AppendSummary(sb, "请选择任务");

                sb.AppendLine($"public static readonly string JoinInAssTasksCantNull = \"JoinInAssTasksCantNull\";");
                AppendSummary(sb, "请填写开始日期");

                sb.AppendLine($"public static readonly string JoinInAssTasksStartDateCantNull = \"JoinInAssTasksStartDateCantNull\";");
                AppendSummary(sb, "请填写截止日期");

                sb.AppendLine($"public static readonly string JoinInAssTasksStopDateCantNull = \"JoinInAssTasksStopDateCantNull\";");
                AppendSummary(sb, "请填写办理人类型");

                sb.AppendLine($"public static readonly string JoinInAssTasksPersonInChargeTypeCantNull = \"JoinInAssTasksPersonInChargeTypeCantNull\";");
                AppendSummary(sb, "开始日期不能晚于截止日期");

                sb.AppendLine($"public static readonly string JoinInAssTasksStartCantGreaterStopDate = \"JoinInAssTasksStartCantGreaterStopDate\";");
                AppendSummary(sb, "办理人{0}已离职或已退休");

                sb.AppendLine($"public static readonly string JoinInAssTasksPersonInChargeHaveDismiss = \"JoinInAssTasksPersonInChargeHaveDismiss\";");
                AppendSummary(sb, "所选任务已停用，请重新选择");

                sb.AppendLine($"public static readonly string JoinInAssTasksHaveStoped = \"JoinInAssTasksHaveStoped\";");
                AppendSummary(sb, "当前阶段下任务名称不允许重复");

                sb.AppendLine($"public static readonly string JoinInAssTasksNameCantRepeat = \"JoinInAssTasksNameCantRepeat\";");
                AppendSummary(sb, "新员工祝福还未开启，任务类别不能选择新员工祝福");

                sb.AppendLine($"public static readonly string JoinInTaskCantBeBlessing = \"JoinInTaskCantBeBlessing\";");
                AppendSummary(sb, "删除方案将同时删除方案下的{0}个阶段，总共{1}个任务，是否要执行此操作");

                sb.AppendLine($"public static readonly string JoinInSchemeAssociatedPhaseDelete = \"JoinInSchemeAssociatedPhaseDelete\";");

                AppendSummary(sb, "删除阶段将同时删除阶段下的{0}个任务，是否要执行此操作");

                sb.AppendLine($"public static readonly string JoinInSchemeAssociatedTaskDelete = \"JoinInSchemeAssociatedTaskDelete\";");
                AppendSummary(sb, "不允许更换任务");


                sb.AppendLine($"public static readonly string JoinInTaskEditCantChange = \"JoinInTaskEditCantChange\";");
                AppendSummary(sb, "删除失败，任务【{0}】已关联方案");

                sb.AppendLine($"public static readonly string JoinInTaskAssociatedSchemeCantDelete = \"JoinInTaskAssociatedSchemeCantDelete\";");
                AppendSummary(sb, "删除失败，任务【{0}】已被员工引用为新员工融入任务");

                sb.AppendLine($"public static readonly string JoinInTaskReferencedCantDelete = \"JoinInTaskReferencedCantDelete\";");

                AppendSummary(sb, "不能与当前方案下其他阶段的名称重复");
                sb.AppendLine($"public static readonly string JoinInPhaseNameCantRepeat = \"JoinInPhaseNameCantRepeat\";");

            }
            catch (System.Exception)
            {

            }

            try
            {

                DateTime dateTime = new DateTime(2022, 7, 18);
                int totalCount = 0;
                var allTrace = GetAllDebugList(dateTime, out totalCount);


            }
            catch (Exception ex)
            {

            }

        }
        /// <summary>
        /// 增加Summary
        /// </summary>
        /// <param name="sb"></param>
        /// <param name="FieldText"></param>
        private static void AppendSummary(StringBuilder sb, string FieldText)
        {
            sb.AppendLine("    /// <summary>");
            sb.AppendLine($"     /// {FieldText}");
            sb.AppendLine("     /// </summary>");
        }

        /// <summary>
        /// 读取exce帮助类
        /// </summary>
        [TestMethod]
        public void TestReadExcel()
        {
            try
            {

                string filePath = "D:\\融入任务规则.xls";
                var dataTable = OfficeHelper.ReadExcelToDataTable(filePath);
                if (dataTable != null)
                {
                    var listData = OfficeHelper.DataTableToList<TenantBaseRuls>(dataTable);
                    //var groupData = listData.GroupBy(x => x.TenantId, y => y);
                    int count = 0;
                    var groupData = listData.GroupBy(item => item.TenantId)
                    .ToDictionary(gourp => gourp.Key,
                    group => group.ToList());
                    Dictionary<string, HashSet<string>> mulData = new Dictionary<string, HashSet<string>>();
                    foreach (var item in groupData)
                    {
                        string defalutSept = "900" + item.Key;
                        //.Where(q => q.ApplyDept != defalutSept)
                        var distinctDept = item.Value.Select(t => t.ApplyDept).Distinct().ToList();
                        HashSet<string> set = new HashSet<string>();
                        if (distinctDept.Count() != 1)
                        {
                            if (mulData.ContainsKey(item.Key) == false)
                            {
                                distinctDept.ForEach(f =>
                                {
                                    var depList = f.Split(',').ToList();
                                    depList.ForEach(w =>
                                    {
                                        var sigleSept = w.Split('|').FirstOrDefault();
                                        if (sigleSept != defalutSept && sigleSept != "0")
                                            set.Add(sigleSept);
                                    });

                                });

                            }
                            mulData.Add(item.Key, set);
                            count++;

                        }
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }

        /// <summary>
        /// 根据trace获取所有数据信息
        /// </summary>
        [TestMethod]
        public void TestGetByTrace()
        {
            DateTime date = DateTime.Now;
            string innerTraceJsonArgs = File.ReadAllText(@"..\..\ResultModel\Files\queryByTrace.text", System.Text.Encoding.UTF8);
            var listStr = GetQueryAllStrByTrace(date, new DateTime(date.Year, date.Month, date.Day, 20, 50, 15), new DateTime(date.Year, date.Month, date.Day, 21, 03, 55), innerTraceJsonArgs, "1f6e8abc-fea2-4249-854b-bc96faaa33bd", SplitMin: 5000);

            HashSet<_SourceV2> allTrace = new HashSet<_SourceV2>();
            Dictionary<string, long> allTraceCount = new Dictionary<string, long>();
            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic.Add("kbn-version", "5.2.2");

            foreach (var queryStr in listStr)
            {
                try
                {
                    //http://et.beisen-inc.com/elasticsearch/_msearch
                    var result = HttpMethods.HttpPost("http://pt.beisen-inc.com/elasticsearch/_msearch", queryStr, headerDic);

                    if (string.IsNullOrEmpty(result) == false)
                    {
                        var resultModel = result.ToObject<BeisenKakfaLogResult>();
                        if (resultModel != null && resultModel.responses != null && resultModel.responses[0].hits.hits != null)
                        {
                            foreach (var hit in resultModel.responses[0].hits.hits)
                            {
                                if (allTraceCount.ContainsKey(hit._source.VirtualPath)==false)
                                {
                                    allTraceCount.Add(hit._source.VirtualPath, hit._source.CostInMillisecond);
                                }
                                else
                                {
                                    allTraceCount[hit._source.VirtualPath] = allTraceCount[hit._source.VirtualPath] + hit._source.CostInMillisecond;
                                }
                                if (hit._source.CostInMillisecond > 2000)
                                    allTrace.Add(hit._source);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {

                }

            }
            //long data= allTrace.Sum(t=>t.CostInMillisecond)
        }
        #endregion

        #region private searchTrace
        /// <summary>
        /// 获取所有的查询时间段
        /// </summary>
        /// <param name="date"></param>
        /// <param name="SplitMin">时间分隔，默认30分钟</param>
        /// <returns></returns>
        private static List<string> GetQueryAllStrByTrace(DateTime date, DateTime startTime, DateTime endTime, string innerTraceJsonArgs = "", string queryStrInfo = "", int SplitMin = 30)
        {
            if (string.IsNullOrEmpty(innerTraceJsonArgs))
            {
                innerTraceJsonArgs = innerTraceJson;
            }
            List<string> queryAllStr = new List<string>();
            //DateTime startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 1);

            //DateTime endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            DateTime tempTime = startTime;

            string innerTraceJsonNew = innerTraceJsonArgs.Replace("DayStr", date.ToString("yyyyMMdd"));
            innerTraceJsonNew = innerTraceJsonNew.Replace("CurrentTimeStr", ConvertDateTimeToInt(DateTime.Now).ToString());
            if (string.IsNullOrEmpty(queryStrInfo) == false)
            {
                innerTraceJsonNew = innerTraceJsonNew.Replace("queryStrInfo", queryStrInfo);
            }

            while (tempTime < endTime)
            {
                string queryStr = innerTraceJsonNew.Replace("date_start", ConvertDateTimeToInt(tempTime).ToString());
                queryStr = queryStr.Replace("date_end", ConvertDateTimeToInt(tempTime.AddSeconds(SplitMin)).ToString());
                queryAllStr.Add(queryStr);
                tempTime = tempTime.AddSeconds(SplitMin);
                //string queryStr = innerTraceJsonNew.Replace("date_start", ConvertDateTimeToInt(tempTime).ToString());
                //queryStr = queryStr.Replace("date_end", ConvertDateTimeToInt(tempTime.AddMinutes(SplitMin)).ToString());
                //queryAllStr.Add(queryStr);
                //tempTime = tempTime.AddMinutes(SplitMin);
            }

            return queryAllStr;
        }
        #endregion

        #region private
        private static Dictionary<string, string> GetViewPathList(string[] traceStrs, DateTime searchDate)
        {

            string traceId = "";
            string viewPath = "";
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"执行开始时间====》" + DateTime.Now.ToString("yyyyMMddTHH:mm:ss"));
            List<string> viewPaths = new List<string>();
            Dictionary<string, string> viewPathDic = new Dictionary<string, string>();
            Dictionary<string, int> viewPathDicCount = new Dictionary<string, int>();
            if (traceStrs.Length > 0)
            {
                foreach (var str in traceStrs)
                {
                    traceId = str.TrimEnd();
                    if (string.IsNullOrEmpty(traceId) == false)
                    {
                        try
                        {
                            viewPath = GetViewPath(traceId, searchDate);
                            if (string.IsNullOrEmpty(viewPath) == false)
                                if (viewPathDic.ContainsKey(viewPath) == false)
                                {
                                    viewPathDic.Add(viewPath, traceId);
                                    viewPathDicCount.Add(viewPath, 1);
                                }
                                else
                                    viewPathDicCount[viewPath] = viewPathDicCount[viewPath] + 1;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }

                    }

                }
            }
            viewPaths = viewPaths.Distinct().ToList();
            if (viewPathDicCount != null && viewPathDicCount.Count > 0)
            {
                string path = "D:\\myTools\\KibanaTerms\\" + searchDate.ToString("yyyyMMdd") + ".text";
                FileHelper.ExistsFileV2(path);
                sb.AppendLine($"数据所在日期：{searchDate}；所有的trace===>{traceStrs.Count()}");
                sb.AppendLine($"接口和trace信息====》{viewPathDic.ToJson()}");
                sb.AppendLine($"接口和数量信息====》{viewPathDicCount.ToJson()}");
                sb.AppendLine($"执行结束时间====》{DateTime.Now.ToString("yyyyMMddTHH:mm:ss")}");
                FileHelper.AppendText(path, sb.ToString());
            }

            return viewPathDic;
        }

        public static string innerJson = File.ReadAllText(@"..\..\ResultModel\Files\queryStrs.text", System.Text.Encoding.UTF8);
        public static string innerTraceJson = File.ReadAllText(@"..\..\ResultModel\Files\queryTraceStrs.text", System.Text.Encoding.UTF8);
        //"{\"index\":[\"invocationtracelog_20220604\"],\"ignore_unavailable\":true,\"preference\":1654307700619}\n{\"size\":2000,\"sort\":[{\"CurrentDateTime\":{\"order\":\"asc\",\"unmapped_type\":\"boolean\"}}],\"query\":{\"bool\":{\"must\":[{\"query_string\":{\"analyze_wildcard\":true,\"query\":\"*\"}},{\"match\":{\"TraceID\":{\"query\":\"traceIdStr\",\"type\":\"phrase\"}}},{\"range\":{\"CurrentDateTime\":{\"gte\":1654221508517,\"lte\":1654307908517,\"format\":\"epoch_millis\"}}}],\"must_not\":[]}},\"highlight\":{\"pre_tags\":[\"@kibana-highlighted-field@\"],\"post_tags\":[\"@/kibana-highlighted-field@\"],\"fields\":{\"*\":{}},\"require_field_match\":false,\"fragment_size\":2147483647},\"_source\":{\"excludes\":[]},\"aggs\":{\"2\":{\"date_histogram\":{\"field\":\"CurrentDateTime\",\"interval\":\"30m\",\"time_zone\":\"Asia/Shanghai\",\"min_doc_count\":1}}},\"stored_fields\":[\"*\"],\"script_fields\":{},\"docvalue_fields\":[\"CurrentDateTime\"]}\n";
        private static string GetViewPath(string traceId, DateTime searchTime)
        {
            string queryStr = innerJson.Replace("traceIdStr", traceId);
            queryStr = queryStr.Replace("DayStr", searchTime.ToString("yyyyMMdd"));
            queryStr = queryStr.Replace("CurrentTimeStr", ConvertDateTimeToInt(DateTime.Now).ToString());
            queryStr = queryStr.Replace("st_timeStr", ConvertDateTimeToInt(searchTime.Date).ToString());
            queryStr = queryStr.Replace("end_TimeStr", ConvertDateTimeToInt(DateTime.Now).ToString());

            var viewPathSplit = "";
            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic.Add("kbn-version", "5.2.2");
            var result = HttpMethods.HttpPost("http://pt.beisen-inc.com/elasticsearch/_msearch", queryStr, headerDic);

            if (string.IsNullOrEmpty(result) == false)
            {
                var resultModel = result.ToObject<BeisenKakfaLogResult>();
                if (resultModel != null && resultModel.responses != null && resultModel.responses[0].hits.hits != null)
                {
                    foreach (var hit in resultModel.responses[0].hits.hits)
                    {
                        //if (hit._source.VirtualPath.Contains("TenantBase"))//TenantBaseMRest
                        //{
                        //    return hit._source.VirtualPath;//.Split('/').LastOrDefault()
                        //}
                    }
                }
            }
            return viewPathSplit;
        }

        [TestMethod]
        public void TestHttpPostRRR()
        {
            try
            {
                var strs = UppIdDetailEnum.Hr.ToString();
                int totalCount = 0;
                GetTraceList(new DateTime(2022, 6, 7), out totalCount);
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// <summary>
        ///  根据日期获取所有的trace
        /// </summary>
        /// <param name="date">查询日期</param>
        /// <returns></returns>
        private static HashSet<string> GetTraceList(DateTime date, out int totalCount)
        {
            totalCount = 0;
            HashSet<string> allTrace = new HashSet<string>();
            var allList = GetAueryAllStr(date);


            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic.Add("kbn-version", "5.2.2");

            foreach (var queryStr in allList)
            {
                var result = HttpMethods.HttpPost("http://pt.beisen-inc.com/elasticsearch/_msearch", queryStr, headerDic);

                if (string.IsNullOrEmpty(result) == false)
                {
                    var resultModel = result.ToObject<BeisenKakfaLogResult>();
                    if (resultModel != null && resultModel.responses != null && resultModel.responses[0].hits.hits != null)
                    {
                        foreach (var hit in resultModel.responses[0].hits.hits)
                        {
                            allTrace.Add(hit._source.EagleEyeTraceID);
                            totalCount++;
                        }
                    }
                }
            }
            return allTrace;
        }
        /// <summary>
        /// 获取所有的查询时间段
        /// </summary>
        /// <param name="date"></param>
        /// <param name="SplitMin">时间分隔，默认30分钟</param>
        /// <returns></returns>
        private static List<string> GetAueryAllStr(DateTime date, string innerTraceJsonArgs = "", string queryStrInfo = "", int SplitMin = 30)
        {
            if (string.IsNullOrEmpty(innerTraceJsonArgs))
            {
                innerTraceJsonArgs = innerTraceJson;
            }
            List<string> queryAllStr = new List<string>();
            DateTime startTime = new DateTime(date.Year, date.Month, date.Day, 0, 0, 1);

            DateTime endTime = new DateTime(date.Year, date.Month, date.Day, 23, 59, 59);
            DateTime tempTime = startTime;

            string innerTraceJsonNew = innerTraceJsonArgs.Replace("DayStr", date.ToString("yyyyMMdd"));
            innerTraceJsonNew = innerTraceJsonNew.Replace("CurrentTimeStr", ConvertDateTimeToInt(DateTime.Now).ToString());
            if (string.IsNullOrEmpty(queryStrInfo) == false)
            {
                innerTraceJsonNew = innerTraceJsonNew.Replace("queryStrInfo", queryStrInfo);
            }

            while (tempTime < endTime)
            {
                string queryStr = innerTraceJsonNew.Replace("date_start", ConvertDateTimeToInt(tempTime).ToString());
                queryStr = queryStr.Replace("date_end", ConvertDateTimeToInt(tempTime.AddMinutes(30)).ToString());
                queryAllStr.Add(queryStr);
                tempTime = tempTime.AddMinutes(30);
            }

            return queryAllStr;
        }

        /// <summary>
        ///  根据日期获取所有的trace
        /// </summary>
        /// <param name="date">查询日期</param>
        /// <returns></returns>
        private static HashSet<string> GetAllDebugList(DateTime date, out int totalCount)
        {
            totalCount = 0;
            string innerTraceJsonArgs = File.ReadAllText(@"..\..\ResultModel\Files\queryDebug.text", System.Text.Encoding.UTF8);
            HashSet<string> allTrace = new HashSet<string>();
            var allList = GetAueryAllStr(date, innerTraceJsonArgs, "Beisen.CoreHr.ESB.ServiceImp.OrganizationV5ServiceProvider.GetOrganizationInfoByOIds,参数内容", 10);

            //queryDebug


            Dictionary<string, string> headerDic = new Dictionary<string, string>();
            headerDic.Add("kbn-version", "5.2.2");

            foreach (var queryStr in allList)
            {
                //http://et.beisen-inc.com/elasticsearch/_msearch
                var result = HttpMethods.HttpPost("http://pt.beisen-inc.com/elasticsearch/_msearch", queryStr, headerDic);

                if (string.IsNullOrEmpty(result) == false)
                {
                    var resultModel = result.ToObject<BeisenKakfaLogResult>();
                    if (resultModel != null && resultModel.responses != null && resultModel.responses[0].hits.hits != null)
                    {
                        foreach (var hit in resultModel.responses[0].hits.hits)
                        {
                            if (hit._source.Message.Contains("Columns\":null") == false)
                                allTrace.Add(hit._source.EagleEyeTraceID);
                        }
                    }
                }
            }
            return allTrace;
        }

        /// <summary>
        /// 将c# DateTime时间格式转换为Unix时间戳格式
        /// </summary>
        /// <param name="time">时间</param>
        /// <returns>long</returns>
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZone.CurrentTimeZone.ToLocalTime(new System.DateTime(1970, 1, 1, 0, 0, 0, 0));
            long t = (time.Ticks - startTime.Ticks) / 10000;  //除10000调整为13位  
            return t;
        }
        #endregion


    }
    /// <summary>
    /// 新版员工详情页用到的身份信息
    /// </summary>
    public enum UppIdDetailEnum
    {
        /// <summary>
        /// 默认都在hr身份
        /// </summary>
        Hr = 0,
        /// <summary>
        /// 团队成员下的走经理身份
        /// </summary>
        Manager = 2,
        /// <summary>
        /// 我的档案下的走员工身份
        /// </summary>
        Staff = 1,

    }
    [DataContract]
    public class AllData
    {
        /// <summary>
        /// 身份信息
        /// </summary>
        [DataMember(Name = "identity")]
        public string Identity { get; set; }
        /// <summary>
        /// 应用开通信息
        /// </summary>
        [DataMember(Name = "authoritys")]
        public Dictionary<string, bool> Authoritys { get; set; }
    }

    public class Authoritys
    {
        public bool Assess { get; set; }
        public bool Recruitment { get; set; }
        public bool Compensation { get; set; }
        public bool Attendance { get; set; }
    }
    /// <summary>
    /// 组织人事规则拆分
    /// </summary>
    public class TenantBaseRuls
    {
        public string TenantId { get; set; }
        public string ApplyDept { get; set; }
        public string TenantName { get; set; }
        public string MoreCondition { get; set; }
        public string Priority { get; set; }


    }
}
