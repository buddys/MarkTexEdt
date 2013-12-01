using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarkTexEdt.util
{
    /// <summary>
    /// 实现NotifyPropertyChanged 接口，属性的改动将自动更新界面
    /// </summary>
    [Serializable]
    public class ObservableClass : INotifyPropertyChanged
    {
        /// <summary>
        /// 系统会自动为该事件生成处理函数
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
