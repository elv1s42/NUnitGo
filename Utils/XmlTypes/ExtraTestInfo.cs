using System;

namespace Utils.XmlTypes
{
    public class ExtraTestInfo
    {
        public ExtraTestInfo()
        {
            Guid = new Guid();
            TestId = "";
            RunnerId = "";
            TestName = "";
            FullTestName = "";
            UniqueTestName = "";
            StartDate = new DateTime();
            FinishDate = new DateTime();
            AssertCount = 0;
            LogPath = "";
            OutPath = "";
            TracePath = "";
            ErrorPath = "";
        }

        public Guid Guid { get; set; }
        public string TestId { get; set; }
        public string RunnerId { get; set; }
        public string TestName { get; set; }
        public string FullTestName { get; set; }
        public string UniqueTestName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime FinishDate { get; set; }
        public int AssertCount { get; set; }
        public string LogPath { get; set; }
        public string OutPath { get; set; }
        public string TracePath { get; set; }
        public string ErrorPath { get; set; }
    }
}