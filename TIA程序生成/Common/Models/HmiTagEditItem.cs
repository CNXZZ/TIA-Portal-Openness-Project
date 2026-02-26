using MyLibrary.Dtos;

namespace TIA程序生成.Common.Models
{
    public class HmiTagEditItem : BaseDto
    {
        private string tagName;
        public string TagName
        {
            get { return tagName; }
            set { tagName = value; OnPropertyChanged(); }
        }

        private string dataType = "Bool";
        public string DataType
        {
            get { return dataType; }
            set { dataType = value; OnPropertyChanged(); }
        }

        private string address;
        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged(); }
        }

        private string acquisitionCycle = "100 ms";
        public string AcquisitionCycle
        {
            get { return acquisitionCycle; }
            set { acquisitionCycle = value; OnPropertyChanged(); }
        }
    }
}
