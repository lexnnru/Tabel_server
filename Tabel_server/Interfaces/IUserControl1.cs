﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tabel_server.Model.Data;

namespace Tabel_server.Interfaces
{
    public interface IUserControl1
    {
        void SetSource(MonthEmployeesDatas emp);
        UserControl uc1 { get; }
    }
}
