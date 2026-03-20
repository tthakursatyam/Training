namespace UltraEnterpriseSDLC
{
    enum RiskLevel
    {
        Low,Medium,High,Critical

    }
    enum SDLCStage
    {
        Backlog,Requirement,Design,Development,CodeReview,Testing,UAT,Deployment,Maintenance

    }
    sealed class Requirement
    {
        public int Id {get;}
        public string Title {get;}
        public RiskLevel Risk{get;}

        public Requirement(int id,string title,RiskLevel risk)
        {
            Id=id;
            Title=title;
            Risk=risk;
        }
    }
    sealed class WorkItem
    {
        public int Id {get;}
        public string Name {get;}
        public SDLCStage Stage{get;}
        public HashSet<int> DependencyIds {get;}
        public WorkItem(int id,string name,SDLCStage stage)
        {
            DependencyIds = new HashSet<int>();
            Name=name;
            Id=id;
            Stage=stage;
        }
    }
    sealed class BuildSnapshot
    {
        public string Version{get;}
        public DateTime TimeStamp{get;}
        public BuildSnapshot(string version)
        {
            Version=version;
            TimeStamp=DateTime.Now;
        } 
    }
    sealed class AuditLog
    {
        public DateTime Time{get;}
        public string Action{get;}
        public AuditLog(string action)
        {
            Time=DateTime.Now;
            Action=action;
        }
    }
    sealed class QualityMetric
    {
        public string Name{get;}
        public double Score{get;}
        
        public QualityMetric(string name,double score)
        {
            Name=name;
            Score=score;
        }
    } 
    sealed class EnterpriseSDLCEngine 
    {
        private List<Requirement> _requirements;
        private Dictionary<int,WorkItem > _workItemRegistry;
        private SortedDictionary<SDLCStage,List<WorkItem>> _stageBoard;
        private Queue<WorkItem> _executionQueue;
        private Stack<BuildSnapshot> _rollbackStack;
        private HashSet<string> _uniqueTestSuites;
        private LinkedList<AuditLog> _auditLedger;
        private SortedList<int,QualityMetric> _releaseScoreboard;
        private int _requirementCounter;
        private int _workItemCounter;

        public EnterpriseSDLCEngine()
        {
            _requirements       = new List<Requirement>();
            _workItemRegistry   = new Dictionary<int, WorkItem>();
            _stageBoard         = new SortedDictionary<SDLCStage, List<WorkItem>>();
            foreach (SDLCStage stage in Enum.GetValues(typeof(SDLCStage)))
            {
                _stageBoard[stage] = new List<WorkItem>();
            }
            _executionQueue = new Queue<WorkItem>();
            _rollbackStack  = new Stack<BuildSnapshot>();
            _uniqueTestSuites  = new HashSet<string>();
            _auditLedger  = new LinkedList<AuditLog>();
            _releaseScoreboard  = new SortedList<int, QualityMetric>();
        }

        public void AddRequirement(int id,string Title,RiskLevel Risk)
        {
            Requirement require = new Requirement(_requirementCounter,Title,Risk);
            _requirementCounter++;
            _requirements.Add(require);
            AuditLog au = new AuditLog("Addition");
            _auditLedger.Append(au);
        }
        public WorkItem CreateWorkItem(string name,SDLCStage stage)
        {
            WorkItem workItem = new WorkItem(_workItemCounter,name,stage);
            _workItemCounter++;
            _workItemRegistry.Add(workItem.Id,workItem);
            _stageBoard[stage].Add(workItem);
            AuditLog au = new AuditLog($"WorkItem:{workItem} with Stage:{stage} was created.");
            _auditLedger.Append(au);
            return workItem;
        }
        public void AddDependency(int workItemId,int dependsOnId)
        {
            if(!_workItemRegistry.ContainsKey(workItemId)) return;
            if(!_workItemRegistry.ContainsKey(dependsOnId)) return;
            WorkItem workItem = _workItemRegistry[workItemId];
            workItem.DependencyIds.Add(dependsOnId);
            AuditLog au = new AuditLog($"Dependency relationship\nWorkItemId:{workItemId},DependsOnId:{dependsOnId}");
            _auditLedger.Append(au);

        }
        public void PlanStage(SDLCStage stage)
        {
            var eligibleItems = _stageBoard[stage].Where(x=> x.DependencyIds.All(id=> _workItemRegistry[id].Stage > stage)).ToList();
            foreach(var i in eligibleItems)
            {
                _executionQueue.Enqueue(i);
            }
            AuditLog au = new AuditLog($"Stage planned: {stage}");
            _auditLedger.Append(au); 
        }
        public void ExecuteNext()
        {
            if(_executionQueue.Count()==0) return;
            var next = _executionQueue.Dequeue();
            SDLCStage stge = next.Stage;
            stge++;
        }
    }
}